using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace rpg.Entities
{
    public class ExplosionBullet : Bullet
    {
        private bool IsExplosion;
        
        public ExplosionBullet(Vector2 position, Vector2 velocity) : base(position, velocity)
        {
            IsExplosion = true;
        }
        public static void CreateCirclyExplosion(Vector2 explosionCenter, int numberOfBullets, float bulletSpeed, BulletsHandler bullets)
        {

            // Create explosion bullets in all directions
            float angleIncrement = MathHelper.TwoPi / numberOfBullets;
            for (int i = 0; i < numberOfBullets; i++)
            {
                Vector2 direction = new Vector2((float)Math.Cos(angleIncrement * i), (float)Math.Sin(angleIncrement * i));
                ExplosionBullet bullet = new ExplosionBullet(explosionCenter, direction* bulletSpeed);
                bullets.addBullet(bullet);
            }
        }

        

        

        public static void CreateCrossExplosion(Vector2 explosionCenter, int numberOfBullets, float bulletSpeed, BulletsHandler bulletHandler)
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = MathHelper.Lerp(0, MathHelper.TwoPi, (float)i / numberOfBullets);
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                ExplosionBullet bullet = new ExplosionBullet(explosionCenter + direction * 20f, direction * bulletSpeed);

                if (i % 2 == 0)
                {
                    bullet.Position = explosionCenter;
                }

                bulletHandler.addBullet(bullet);
            }
        }

        public static void CreateSineWaveExplosion(Vector2 explosionCenter, int numberOfBullets, float bulletSpeed, BulletsHandler bulletHandler)
        {
            float angleIncrement = MathHelper.TwoPi / numberOfBullets;

            for (int i = 0; i < numberOfBullets; i++)
            {
                float angle = angleIncrement * i;
                Vector2 direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));

                // Add a sine wave pattern to the bullet's movement
                float amplitude = 50f; // Adjust the amplitude of the sine wave
                float frequency = 2f; // Adjust the frequency of the sine wave
                float yOffset = amplitude * (float)Math.Sin(frequency * angle);

                ExplosionBullet bullet = new ExplosionBullet(explosionCenter, direction * bulletSpeed);
                bullet.Position += direction * yOffset;
                bulletHandler.addBullet(bullet);
            }
        }






    }
}
