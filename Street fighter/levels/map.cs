using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg.levels
{
    
        public class Map
        {
            #region Fields
            public int TileSize = 32;
            public int[,] solid;//1 when blocks & 0 when floor
            private Texture2D tileset;
            public int Size;//the size of the tilemap (in tiles)

            #endregion

            #region Constructor
            public Map(int Size)
            {
                this.Size = Size;
                solid = new int[Size, Size];
                PlaceFloorTile();
                setSolid();
            }
            #endregion



            /// <summary>
            /// Set everything to 0 so that nothing is solid at first
            /// </summary>
            private void PlaceFloorTile()
            {
                //Random floor tiles
                for (int x = 0; x < Size; x++)
                {
                    for (int y = 0; y < Size; y++)
                    {
                        solid[x, y] = 0;
                    }
                }
            }

            /// <summary>
            /// Places walls on the map
            /// </summary>
            /// 
            private void setSolid()
            {
                solid[10, 7] = 1;
                solid[11, 7] = 1;
                solid[12, 7] = 1;
                solid[13, 7] = 1;
                solid[14, 7] = 1;
                solid[14, 8] = 1;
                solid[14, 9] = 1;
            }


            public void Draw(SpriteBatch spriteBatch)
            {
                for (int x = 0; x < Size; x++)
                {
                    for (int y = 0; y < Size; y++)
                    {
                        //Draw the tile
                        spriteBatch.FillRectangle(new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize), Color.Green);//draw the floor tile
                        spriteBatch.DrawString(Globals.spriteFont, solid[x, y].ToString(), new Vector2(x * TileSize, y * TileSize), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
                }
            }
        }
    
}
