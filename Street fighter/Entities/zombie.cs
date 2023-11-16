using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using rpg.AI;
using rpg.levels;
using rpg.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using rpg.models;
using Animation = rpg.models.Animation;

namespace rpg.Entities
{
    public class zombie : Entity
    {

        float speed = 200f;
        public bool grabbed = false;
        List<string>dialogueTexts = new List<string>();
        public Dialogue dialogue;
        public Vector2 destination;
        public List<Vector2> path = new List<Vector2>();
        private Color color;
        Animation animation;
        private Texture2D spriteSheet;
        private int row = 0;
        
        public zombie(types type, Vector2 position) : base(type, position)
        {
           
            dialogueTexts.Add("Hello there stranger!");
            dialogueTexts.Add("Are you from around here stranger?");
            dialogue = new Dialogue(dialogueTexts);
            Random rnd = new Random();
            spriteSheet = Globals.content.Load<Texture2D>($"Zombies/{rnd.Next(1,7)}ZombieSpriteSheet");
            animation = new Animation(spriteSheet, 41, 36, 0.5f, 3, row);
          
            color = new Color(rnd.Next(255), rnd.Next(255), rnd.Next(255));



        }
        public void SetDestination(Vector2 dest)
        {
          
            Vector2 Position = new Vector2(position.X/testLevel.scaledTileWidth, position.Y/testLevel.scaledTileHeight);
            Position.Round();
            destination = new Vector2(dest.X / testLevel.scaledTileWidth, dest.Y / testLevel.scaledTileHeight);
            destination.Round();
            path = Globals.pathfinding.FindPath(Position, destination);
        }
        private bool reachedDestination(Vector2 targetPosition)
        {
            bool hasArrived = false;
            float distanceToTarget = Vector2.Distance(position, new Vector2(targetPosition.X * testLevel.scaledTileWidth,
                    targetPosition.Y * testLevel.scaledTileHeight));
            if (distanceToTarget <= speed)
            {
                hasArrived = true;
            }
            return hasArrived;
        }

        private Vector2 previousTile = Vector2.Zero; // Initialize the previous tile

        private void FollowPath( )
        {
            var allZombies = Globals.entityManager.Entities;
            if (path != null && path.Count > 0)
            {
                Vector2 currentTile = new Vector2(path[0].X, path[0].Y);
                Vector2 targetPosition = new Vector2(currentTile.X * testLevel.scaledTileWidth, currentTile.Y * testLevel.scaledTileHeight);
                Vector2 direction = Vector2.Normalize(targetPosition - position);

                // Update the grid to mark the current tile as 1
                Globals.pathfinding.grid[(int)currentTile.X, (int)currentTile.Y] = 1;

                // If previousTile is not null, mark it as 0 to clear the previous tile
                if (previousTile != Vector2.Zero)
                {
                    Globals.pathfinding.grid[(int)previousTile.X, (int)previousTile.Y] = 0;
                }

                // Update previousTile
                previousTile = currentTile;

                // Rest of your movement logic...

                float distanceToWaypoint = Vector2.Distance(position, targetPosition);
                float moveDistance = speed * Globals.deltaTime;

                if (moveDistance > distanceToWaypoint)
                {
                    position = targetPosition; // Snap to the waypoint if close enough
                    path.RemoveAt(0); // Remove the waypoint
                }
                else
                {
                    position += direction * moveDistance; // Smoothly move towards the waypoint
                }


                Vector2 Direction = Vector2.Normalize(targetPosition - position);

                // Determine the animation row based on the direction
                if (Math.Abs(Direction.X) > Math.Abs(Direction.Y))
                {
                    // Horizontal movement is dominant
                    if (Direction.X > 0)
                    {
                        //right direction

                        row = 1;
                    }
                    else
                    {
                        //left direction
                        row = 3;
                    }
                }
                else if (Math.Abs(Direction.Y) > Math.Abs(Direction.X))
                {
                    // Vertical movement is dominant
                    if (Direction.Y > 0)
                    {
                        //down direction
                        row = 0;
                    }
                    else
                    {
                        //up direction
                        row = 2;
                    }
                }
            }


        }


        public override void render(SpriteBatch spriteBatch)
        {
            animation.currentRow = row;
            animation.Draw(spriteBatch, new Vector2(position.X+270,position.Y+210), 2f, null);
         
            
            if (path is not null)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    spriteBatch.FillRectangle(new Rectangle((int)path[i].X * testLevel.scaledTileWidth, (int)path[i].Y * testLevel.scaledTileHeight, 16, 16), color);
                }
            }
            int offsetX = 3;
            int offsetY = -20;
            float healthPercentage = (float)health / 100; // Calculate health percentage

            if (healthPercentage < 1f)
            {
                int healthBarWidth = (int)(60 * healthPercentage); // Adjust as needed
                int healthBarHeight = 8; // Adjust as needed

                Vector2 healthBarPosition = new Vector2(bounds.X + offsetX, bounds.Y + offsetY - 20); // Adjust offsets

                // Draw black background
                spriteBatch.FillRectangle(new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, 60, healthBarHeight), Color.Black);

                // Draw green health indicator
                spriteBatch.FillRectangle(new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, healthBarWidth, healthBarHeight), Color.Green);
            }





        }

        private float elapsedActionTime = 0f;
        private float actionInterval = 0.8f; // 1 seconds interval


        public override void update(GameTime gameTime)
        {

            base.update(gameTime);
            elapsedActionTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
           

            if (elapsedActionTime >= actionInterval)
            {
                SetDestination(Game1.player.position);
                elapsedActionTime = 0f;
            }

            FollowPath();

            bounds = new Rectangle((int)position.X, (int)position.Y + 15, 80, 80);
            shake();
            animation.Update(gameTime);

           


        }

        public void shake()
        {
            if (bounds.Intersects(Game1.player.bounds))
            {
                Globals.camera.Shake(3, 100f);
            }
        }
        public override void loadContent(ContentManager content)
        {

        }

       




    }
}
