using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpg.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace rpg.Entities
{
    public class BulletsHandler
    {
        public List<Bullet> bullets;
      
        public BulletsHandler()
        {
            bullets = new List<Bullet>();
        
        }


        public void update(GameTime gameTime)
        {
            if (bullets != null)
            {
                var temp = Globals.entityManager.Entities;
                for(int i = 0;i< bullets.Count;i++)
                {
                    Bullet bullet = bullets[i];
                    bullet.Update(gameTime);
                    foreach (Entity e in temp)
                    {
                        if (bullet.bounds.Intersects(e.bounds))
                        {
                            e.health-=20;
                            removeBullet(bullet);
                            Debug.WriteLine(e.health);

                        }
                    }
                }
            }
        }
        public void render(SpriteBatch spriteBatch)
        {

            if (bullets != null)
            {
                foreach (Bullet bullet in bullets) 
                { 
                    bullet.Draw(spriteBatch);
                }
            }
        }
        public void addBullet(Bullet bullet)
        {
           bullets.Add(bullet);
            


        }

        public void removeBullet(Bullet bullet)
        {
            bullets.Remove(bullet);
        }
        

    }
}
