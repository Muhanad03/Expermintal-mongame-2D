using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using rpg.walls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace rpg.levels
{
    public abstract class level
    {
        protected TmxMap map { get; set; }
        protected Texture2D tileset;

        protected int tileWidth { get; set; }
        protected int tileHeight { get; set; }
        protected int tilesetTilesWide;
        protected int tilesetTilesHigh;
        protected List<Wall> walls = new List<Wall>();
        public int[,] solid;
        public level()
        {
         

        }

        public abstract void update(GameTime gameTime);
        public abstract void render(SpriteBatch spriteBatch);
        public abstract void loadMap();


    }
}
