using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace rpg.Entities
{
    public abstract class Entity
    {
        public Vector2 position { get; set; }
        public types type { get; set; }
        public Texture2D texture { get; set; }
        public Rectangle bounds { get; set; }
        public int health { get; set; } = 100;
        public Entity(types type,Vector2 position) 
        {
            this.type = type;
            this.position = position;
           
           
        }


        public virtual void update(GameTime gameTime)
        {
            foreach (var entity in Globals.entityManager.Entities)
            {
                if (entity.type == type && entity != this)
                {
                    if (entity.bounds.Intersects(bounds))
                    {
                        // Calculate the direction from 'this' entity to 'entity'
                        Vector2 direction = entity.position - position;
                        direction.Normalize();

                        // Calculate the distance between 'this' entity and 'entity'
                        float distance = Vector2.Distance(entity.position, position);

                        // Define a minimum distance to prevent merging
                        float minDistanceToPreventMerging = 20.0f; // Adjust this value as needed

                        if (distance < minDistanceToPreventMerging)
                        {
                            // Push 'this' entity away from 'entity'
                            position -= direction * (minDistanceToPreventMerging - distance) / 2.0f;
                        }
                    }
                }
            }
        }

        public abstract void render(SpriteBatch spriteBatch);
        public abstract void loadContent(ContentManager content);

    }
}
