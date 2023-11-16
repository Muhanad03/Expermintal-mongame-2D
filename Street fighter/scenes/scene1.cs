using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using rpg.Entities;
using rpg.levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace rpg.scenes
{
    public class scene1 : scene
    {
        public scene1 (Player player):base(player)
        {
            test = new testLevel(player);
            Globals.entityManager = entityManager;
            entityManager.addEntity(new zombie(types.Zombie, new Vector2(300, 300)));
           
        }

        public override void loadContent()
        {
            base.loadContent();
            

        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
            if (!inventory.Visible)
            {

                entityManager.update(gameTime);
                Globals.camera.Update(gameTime, player.position);
                player.update(gameTime);
                test.update(gameTime);

            }
            inventory.Update(new MouseState(),gameTime);
          
        }

        public override void render(SpriteBatch spritebatch)
        {

            spritebatch.Begin(transformMatrix: Globals.camera.GetViewMatrix(), samplerState: SamplerState.PointWrap);

            test.render(spritebatch);
            if (!inventory.Visible)
            {
                Globals.dialogueUi.draw(spritebatch);
                player.render(spritebatch);
                entityManager.render(spritebatch);
                spritebatch.FillRectangle(new Rectangle((int)Globals.mouseWorldPos.X, (int)Globals.mouseWorldPos.Y, 32, 32), Color.Yellow);
            }
           
           
         
           
            spritebatch.End();

            Vector2 temp = player.position;

            temp.Round();
            spritebatch.Begin();
            if (inventory.Visible)
            {
                inventory.Draw(spritebatch);    
            }
            spritebatch.DrawString(Globals.spriteFont, temp.ToString(), new Vector2(30, 30), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spritebatch.DrawString(Globals.spriteFont, "FPS: " + (Game1.fps).ToString(), new Vector2(30, 60), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spritebatch.DrawString(Globals.spriteFont, Globals.mouseWorldPos.ToString(), new Vector2(30, 90), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spritebatch.DrawString(Globals.spriteFont, "Kills: " + (Globals.entityManager.kills).ToString(), new Vector2(30, 120), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
            spritebatch.End();

        }

      
        public scene restart()
        {
            return new scene1(player);
        }
    }
}
