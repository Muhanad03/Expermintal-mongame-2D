using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace rpg.AI
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public class Pathfinding
    {
        public int[,] grid;
        private int width;
        private int height;

        public Pathfinding(int[,] grid)
        {
            this.grid = grid;
            this.width = grid.GetLength(0);
            this.height = grid.GetLength(1);
        }

        public List<Vector2> FindPath(Vector2 start, Vector2 end)
        {
            Node startNode = new Node((int)start.X, (int)start.Y);
            Node endNode = new Node((int)end.X, (int)end.Y);

            List<Node> openSet = new List<Node>();
            openSet.Add(startNode);

            Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
            Dictionary<Node, float> gScore = new Dictionary<Node, float>();
            gScore[startNode] = 0;

            Dictionary<Node, float> fScore = new Dictionary<Node, float>();
            fScore[startNode] = HeuristicCost(startNode, endNode);

            while (openSet.Count > 0)
            {
                openSet.Sort((a, b) => fScore[a].CompareTo(fScore[b]));
                Node current = openSet[0];

                if (current.Equals(endNode))
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);

                foreach (Node neighbor in GetNeighbors(current))
                {
                    float tentativeGScore = gScore[current] + 1; // Assuming uniform cost
                    if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentativeGScore;
                        fScore[neighbor] = gScore[neighbor] + HeuristicCost(neighbor, endNode);

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }

            return null; // No path found
        }

        private float HeuristicCost(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            for (int i = 0; i < 4; i++)
            {
                int newX = node.X + dx[i];
                int newY = node.Y + dy[i];

                if (newX >= 0 && newX < width && newY >= 0 && newY < height && grid[newX, newY] == 0)
                {
                    neighbors.Add(new Node(newX, newY));
                }
            }

            return neighbors;
        }

        private List<Vector2> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
        {
            List<Vector2> path = new List<Vector2>();
            while (cameFrom.ContainsKey(current))
            {
                path.Add(new Vector2(current.X, current.Y));
                current = cameFrom[current];
            }
            path.Reverse();
            return path;
        }

        private class Node
        {
            public int X { get; }
            public int Y { get; }

            public Node(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override int GetHashCode()
            {
                return X.GetHashCode() ^ Y.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is Node other)
                {
                    return X == other.X && Y == other.Y;
                }
                return false;
            }
        }
    }

}
