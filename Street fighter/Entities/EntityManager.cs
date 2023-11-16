using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg.Entities
{
    public class EntityManager
    {
        public List<Entity> Entities = new List<Entity>();
        public int kills = 0;
        public EntityManager() 
        {
            
        }
        public void update(GameTime gameTime)
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                Entity entity = Entities[i];
                entity.update(gameTime);

                if (entity.health <= 0)
                {
                    removeEntity(entity);
                    kills++;
                }
                
               

            }

            if (Entities.Count == 0)
            {
                Random rnd = new Random();
                int Count = rnd.Next(1, 10);

                for (int i = 0; i < Count; i++)
                {
                    Entities.Add(new zombie(types.Zombie, new Vector2(rnd.Next(100,400), 1000)));
                }
            }
            
            Debug.WriteLine(Entities.Count);
        }
        public void render(SpriteBatch spriteBatch)
        {
            foreach (var entity in Entities)
            {
                entity.render(spriteBatch);

            }

        }
        public void loadContent(ContentManager content)
        {
            foreach (var entity in Entities)
            {
                entity.loadContent(content);

            }

        }
        
        public void addEntity(Entity entity)
        {
            Entities.Add(entity);

        }
        public void removeEntity(Entity entity)
        {
            Entities.Remove(entity);

        }

        public Entity returnEntity(types targetType)
        {
            foreach(var entity in Entities)
            {
                if(entity.type == targetType)
                {
                    return entity;
                }
               
            }

            return null;
        }
        public List<Entity> allEntities()
        {
            return Entities;
        }


    }
}
