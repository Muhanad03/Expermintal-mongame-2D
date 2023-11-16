using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using rpg.input;
using rpg.models;
using rpg.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace rpg.Entities
{
    public class Player : Entity
    {

        float speed = 500f;
        private Animation Animation;
        Texture2D walking, Striking, lifting, holdingRun;
        float scale = 0.4f;
        public static BulletsHandler bulletHandler = new BulletsHandler();
        SoundEffect shooting;
        public Player(types type, Vector2 position) : base(type, position)
        {
            loadContent();
            this.position = position;
            
        }

        public override void loadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }
        SoundEffectInstance soundInstance;
        public void loadContent()
        {
         
            shooting = Globals.content.Load<SoundEffect>("laser2");
            soundInstance = shooting.CreateInstance();
            Animation = new Animation(walking, 260, 216, 1f, 20, 0);
            temp = bounds;
        }


        public override void render(SpriteBatch spriteBatch)
        {
           
            //Animation.Draw(spriteBatch, new Vector2(bounds.X+50,bounds.Y+40), scale,spriteRotation);
            spriteBatch.DrawRectangle(bounds, Color.Red);
            spriteBatch.DrawRectangle(temp, Color.Black);
            spriteBatch.FillRectangle(bounds, Color.Black);

            bulletHandler.render(spriteBatch);

            Debug.WriteLine("ani" + Animation.currentFrame);
     
            
            
        }
        private float timeSinceLastEKeyPress = 0f;
        private const float eKeyPressDelay = 0.3f;
        private float spriteRotation = 0f;
        Vector2 direction;
        public override void update(GameTime gameTime)
        {
            timeSinceLastEKeyPress += Globals.deltaTime;
            Vector2 newPosition = position; // Create a new variable to store the updated position
           
            direction = (Globals.mousePos + Globals.camera.Position) - position;
           

            // Calculate the rotation angle based on the direction
            spriteRotation = (float)Math.Atan2(direction.Y, direction.X);



            if (!keyListener.W && !keyListener.A && !keyListener.S && !keyListener.D)
            {
                Animation.isActive = false;
            }
            else { Animation.isActive = true; }

            speed = (keyListener.LSHIFT) ? 800f : 500f;
            Animation.frameTime = (keyListener.LSHIFT) ? 0.07f : 0.1f;

            if (keyListener.D) { newPosition.X += Globals.deltaTime * speed; }
            else if (keyListener.A) { newPosition.X -= Globals.deltaTime * speed; }
            else if (keyListener.S) { newPosition.Y += Globals.deltaTime * speed; }
            else if (keyListener.W) { newPosition.Y -= Globals.deltaTime * speed; }

            if (keyListener.F) { Animation.spriteSheet = holdingRun; }
            else { Animation.spriteSheet = walking; }

           


            Animation.Update(gameTime);

            // Update the position with the new value
            position = newPosition;
            // Update the bounds rectangle based on the new position
            bounds = new Rectangle((int)position.X, (int)position.Y, (int)100, (int)90);


            //bounds = new Rectangle((int)position.X - 40, (int)position.Y - 25, (int)80, (int)64);

            temp = bounds;
            temp.Width = bounds.Width * 2;
            temp.Height = bounds.Height * 2;
            temp.X = bounds.X - (temp.Width - bounds.Width) / 2;
            temp.Y = bounds.Y - (temp.Height - bounds.Height) / 2;
            shoot();
            bulletHandler.update(gameTime);



        }
        Rectangle temp;
        public void shoot()
        {
            if (keyListener.MouseLeftClick)
            {
                Vector2 gunPosition = position + new Vector2(50, 40); // Adjust the offset as needed

                if (!temp.Contains(Globals.mouseWorldPos))
                {
                    Animation = new Animation(Striking, 260, 216, 1f, 3, 0);

                    soundInstance.Stop();
                    bulletHandler.addBullet(new Bullet(gunPosition, direction));
                    soundInstance.Volume = 0.01f;
                    soundInstance.Play();
                }
                else
                {
                    Animation = new Animation(walking, 260, 216, 1f, 20, 0);
                }
            }

            if(keyListener.MouseRightClick)
            {

               
                ExplosionBullet.CreateCrossExplosion(position, 20, 30f, bulletHandler);
                
                Debug.WriteLine("bomb");
            }
        }










    }
}
