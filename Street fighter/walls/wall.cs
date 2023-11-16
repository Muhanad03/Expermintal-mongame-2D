using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using rpg.Entities;
using System;
using System.Diagnostics;

namespace rpg.walls
{
    public class Wall
    {
        public Vector2 position { get; set; }
        public Rectangle bounds { get; set; }

        public Wall(Vector2 position, int width, int height)
        {
            this.position = position;
            bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void Update(Entity player)
        {
           Rectangle temp = new Rectangle((int)position.X-20, (int)position.Y, bounds.Width, bounds.Height);
            if (bounds.Intersects(player.bounds))
            {
                Rectangle intersection = Rectangle.Intersect(player.bounds, bounds);
              
                if (intersection.Width > intersection.Height)
                {
                    // Adjust vertical position
                    if (player.position.Y < bounds.Y)
                        player.position = new Vector2(player.position.X, player.position.Y - intersection.Height);
                    else
                        player.position = new Vector2(player.position.X, player.position.Y + intersection.Height);
                }
                else
                {
                    // Adjust horizontal position
                    if (player.position.X < bounds.X)
                    {
                       
                        player.position = new Vector2(player.position.X - intersection.Width, player.position.Y);
                        

                    }
                        

                    else
                        player.position = new Vector2(player.position.X + intersection.Width, player.position.Y);
                }
            }

            //if(temp.Intersects(player.bounds))
            //{
            //    if (player.bounds.X <= temp.X)
            //    {
            //        Globals.leftCollide = true;
            //        player.position = new Vector2(player.position.X - 30, player.position.Y);
            //        Globals.leftCollide = false;

            //    }
            //}
        }
    }
}
