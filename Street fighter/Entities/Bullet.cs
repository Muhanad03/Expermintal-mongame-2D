using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
namespace rpg.Entities
{
    public class Bullet
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsActive { get; set; }
        public Rectangle bounds { get; set; }
        public Bullet(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
            //Texture = texture;
            IsActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds*5f;
                bounds = new Rectangle((int)Position.X, (int)Position.Y + 15, 16, 16);
            }

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.FillRectangle(bounds,Color.Red);
            }
        }
    }
}
