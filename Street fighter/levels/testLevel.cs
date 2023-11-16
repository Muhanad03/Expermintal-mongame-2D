using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using rpg.walls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TiledCS;
using TiledSharp;
using System.Threading;
using System.Diagnostics;
using MonoGame.Extended;
using rpg.Entities;

namespace rpg.levels
{
    public class testLevel : level
    {

        Player player;
        public testLevel(Player player)
        {
       
            this.player = player;
        }
        float mapScale = 3f;
        int renderDistance = 32; // Adjust this value to control the number of tiles to render around the player
     
        public override void render(SpriteBatch spriteBatch)
        {
            // Calculate the range of tiles to render around the player
            int playerTileX = (int)(player.position.X / (tileWidth * mapScale));
            int playerTileY = (int)(player.position.Y / (tileHeight * mapScale));
           

            int startX = Math.Max(0, playerTileX - renderDistance);
            int startY = Math.Max(0, playerTileY - renderDistance);
            int endX = Math.Min(map.Width, playerTileX + renderDistance + 1);
            int endY = Math.Min(map.Height, playerTileY + renderDistance + 1);

            foreach (TmxLayer layer in map.Layers)
            {
                for (int y = startY; y < endY; y++)
                {
                    for (int x = startX; x < endX; x++)
                    {
                        int i = y * map.Width + x;
                        int gid = layer.Tiles[i].Gid;

                        // Empty tile, do nothing
                        if (gid == 0)
                        {
                            continue;
                        }

                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        // Calculate the scaled tile width and height
                        int scaledTileWidth = (int)(tileWidth * mapScale);
                        int scaledTileHeight = (int)(tileHeight * mapScale);

                        // Calculate the scaled position
                        float scaledX = x * scaledTileWidth;
                        float scaledY = y * scaledTileHeight;
                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);                     

                        // Draw the tile with the scaled size and position
                        spriteBatch.Draw(tileset, new Rectangle((int)scaledX, (int)scaledY, scaledTileWidth, scaledTileHeight), tilesetRec, Color.White);
                        //spriteBatch.DrawString(Globals.spriteFont, solid[x,y].ToString(), new Vector2(scaledX, scaledY), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }

            
            
        }


        public override void update(GameTime gameTime)
        {
            foreach(Wall wall in walls)
            {
                wall.Update(player);

                for(int i = 0; i< Player.bulletHandler.bullets.Count;i++)
                {
                    if (Player.bulletHandler.bullets[i].bounds.Intersects(wall.bounds))
                    {
                        Player.bulletHandler.removeBullet(Player.bulletHandler.bullets[i]);
                    }
                }
                
            }
        


        }
     
        public static int scaledTileHeight, scaledTileWidth;

        public override void loadMap()
        {
            map = new TmxMap("Content/exampleMap.tmx");
            tileset = Globals.content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
            solid = new int[map.Width, map.Height];
           
            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
            scaledTileWidth = (int)(tileWidth * mapScale);
            scaledTileHeight = (int)(tileHeight * mapScale);
            foreach (TmxLayer layer in map.Layers)
            {
                if(layer.Name == "walls")
                {
                    for(int  i = 0; i < layer.Tiles.Count; i++)
                    {
                        int gid = layer.Tiles[i].Gid;

                        // Empty tile, do nothing
                        if (gid == 0)
                        {
                            continue;
                        }

                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        // Calculate the scaled tile width and height
                        scaledTileWidth = (int)(tileWidth * mapScale);
                        scaledTileHeight = (int)(tileHeight * mapScale);

                        // Calculate the scaled position
                        int xIndex = i % map.Width;
                        int yIndex = i / map.Width;

                        float scaledX = xIndex * scaledTileWidth;
                        float scaledY = yIndex * scaledTileHeight;
                     
                        
                        walls.Add(new Wall(new Vector2(scaledX, scaledY), scaledTileWidth, scaledTileHeight));
                        
                    }

                }
            }


            // Calculate the range of tiles to render around the player
            int playerTileX = (int)(player.position.X / (tileWidth * mapScale));
            int playerTileY = (int)(player.position.Y / (tileHeight * mapScale));


          
           

            foreach (TmxLayer layer in map.Layers)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        int i = y * map.Width + x;
                        int gid = layer.Tiles[i].Gid;

                        // Empty tile, do nothing
                        if (gid == 0)
                        {
                            continue;
                        }

                        // Calculate the scaled tile width and height
                        int scaledTileWidth = (int)(tileWidth * mapScale);
                        int scaledTileHeight = (int)(tileHeight * mapScale);

                        // Calculate the scaled position
                        int scaledX = x * scaledTileWidth;
                        int scaledY = y * scaledTileHeight;
                        if(layer.Name == "walls" || layer.Name == "trees" || layer.Name == "houses" || layer.Name == "house2") { solid[x, y] = 1; }
                        else { solid[x, y] = 0; }
                        

                    }
                }
            }
      
            
          
        }
    }
}
