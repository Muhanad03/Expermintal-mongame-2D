using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpg.Entities;
using rpg.levels;
using rpg.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg.scenes
{
    public class scene
    {
        protected Player player;
        protected EntityManager entityManager = new EntityManager();
        public InventoryMenu inventory = new InventoryMenu(new Vector2(720 / 2, 40));
        protected level test;
        public scene(Player player)
        {
            this.player = player;
        }
        public scene restart()
        {
            return new scene(player);
        }
        public virtual void update(GameTime gameTime)
        {
            Globals.mouseWorldPos = Globals.mousePos + Globals.camera.Position;
        }
        public virtual void render(SpriteBatch spritebatch) { }
        public virtual void loadContent() 
        {
            test.loadMap();
            Globals.pathfinding = new AI.Pathfinding(test.solid);
        }


    }
}
