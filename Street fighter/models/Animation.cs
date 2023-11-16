using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg.models
{
    public class Animation
    {
        public Texture2D spriteSheet { get; set; }
        private int frameWidth;
        private int frameHeight;
        public float frameTime { get; set; }
        private float timer;
        private int totalFrames;
        public int currentRow { get; set; }
        public int currentFrame { get; set; }
        public bool isActive { get; set; } = true;
        public Animation(Texture2D spriteSheet, int frameWidth, int frameHeight, float frameTime, int totalFrames, int currentRow)
        {
            this.spriteSheet = spriteSheet;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.frameTime = frameTime;
            this.totalFrames = totalFrames;
            this.currentRow = currentRow;
            currentFrame = 0;
            timer = 0f;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (isActive == true)
            {
                if (timer >= frameTime)
                {
                    currentFrame++;
                    if (currentFrame >= totalFrames)
                    {
                        currentFrame = 0;
                    }
                    timer = 0f;
                }
            }
            else
            {
                currentFrame = 0;
            }
            
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position,float scale,float ?roatation)
        {   
           
            int row = currentRow;
            int column = currentFrame;

            int spriteSheetRows = spriteSheet.Height / frameHeight;
            int spriteSheetColumns = spriteSheet.Width / frameWidth;
            float Rotation;
            if(roatation is null)
            {
                Rotation = 0f;
            }
            else
            {
                Rotation = (float)roatation;
            }
            //new Vector2(281 / 2, 100)
            if (row >= 0 && row < spriteSheetRows && column >= 0 && column < spriteSheetColumns)
            {
                Rectangle sourceRect = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);
                spriteBatch.Draw(spriteSheet, position, sourceRect, Color.White, Rotation, new Vector2(281/2,100), scale, SpriteEffects.None, 0f);
            }
           
        }
    }
}
