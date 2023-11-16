using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using rpg.AI;
using rpg.Entities;
using rpg.scenes;
using rpg.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static rpg.Game1;

namespace rpg
{
    public class Globals
    {
        //Game related properties 
        public static SpriteFont spriteFont;
        public static ContentManager content;
        public static State CurrnetState = State.MainMenu;
        public static float deltaTime;
        public static DialogueManager dialogueUi;
        public static Camera camera;
        public static Pathfinding pathfinding;
        public static scene currentScene;
        public static Vector2 mousePos;
        public static Vector2 mouseWorldPos;
        public static Vector2 adjustedMousePosRelativeToRes;
        public static EntityManager entityManager;
        //Settings
        public static bool isFullScreen = false;
        public static bool mouseVisible = false;
        public static bool mutliSampling = true;
        public static bool IsBorderless = false;
        public static int heightRes = (isFullScreen) ? 1440 : 720;
        public static int widthRes = (isFullScreen) ? 2560 : 1280;
        public static float framesLimit = 240f;
    }
}
