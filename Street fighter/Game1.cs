// Ignore Spelling: rpg Currnet

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpg.Entities;
using rpg.input;
using rpg.levels;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;
using TiledCS;
using System.Diagnostics;
using rpg.AI;
using rpg.UI;
using rpg.scenes;

namespace rpg
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public keyListener keyListener = new keyListener();
        public static Player player;
        public static GraphicsDevice device;
        RenderTarget2D renderTarget;
        float scale = 0.44444f;
        private int frameCounter;
        private float frameTimer;
        public static int fps;
        private int renderTargetOffsetX = 0;
        private int renderTargetOffsetY = 0;

        public static MainMenu menu = new MainMenu();
        public static pauseMenu pauseMenu;
       
        
        public enum State
        {
            MainMenu,
            GamePlay,
            GamePause,
            GameEnd,
        }
         


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = Globals.mouseVisible;
            Window.Title = "Prototype";
            
        }

        protected override void Initialize()
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / Globals.framesLimit);
            base.Initialize();
            _graphics.PreferredBackBufferWidth = Globals.widthRes;
            _graphics.PreferredBackBufferHeight = Globals.heightRes;
          

            _graphics.IsFullScreen = Globals.isFullScreen;
            Window.IsBorderless = Globals.IsBorderless;
            _graphics.ApplyChanges();
           
            Globals.camera = new Camera(new Vector2(-600, -300), 0.05f);



        }

        protected override void LoadContent()
        {
            device = this.GraphicsDevice;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            renderTarget = new RenderTarget2D(GraphicsDevice, 1280, 720);
            Globals.content = Content;
            Globals.dialogueUi = new DialogueManager();
            player = new Player(types.Player,new Vector2(1060,430));
            Globals.currentScene = new scene1(player);
            Globals.currentScene.loadContent();
            Globals.spriteFont = Content.Load<SpriteFont>("File");
            pauseMenu = new pauseMenu(Globals.currentScene.inventory);


        }

        protected override void Update(GameTime gameTime)
        {
            keyListener.update();
            float mouseX = (Mouse.GetState().X - renderTargetOffsetX) / scale;
            float mouseY = (Mouse.GetState().Y - renderTargetOffsetY) / scale;
            Globals.adjustedMousePosRelativeToRes = new Vector2(mouseX, mouseY);
            Globals.mousePos = new Vector2((int)mouseX, (int)mouseY);
            Globals.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter++;
            if (frameTimer >= 1f)
            {
                fps = frameCounter;
                frameCounter = 0;
                frameTimer = 0.0f;
                 
            }
           

           
            switch (Globals.CurrnetState)
            {

                case State.GamePlay:


                    
                    Globals.currentScene.update(gameTime);
                    Globals.dialogueUi.update();
              

                    break;

                case State.MainMenu:
                    menu.Update(mouseX, mouseY);



                    break;
                case State.GamePause:
                   

                    break;
                case State.GameEnd:
                    

                    break;

            }

            pauseMenu.Update(mouseX, mouseY);
           
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {

            scale = 1f / (720f / GraphicsDevice.Viewport.Height); 
            renderTargetOffsetX = GraphicsDevice.Viewport.X;
            renderTargetOffsetY = GraphicsDevice.Viewport.Y;
            GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicsDevice.Clear(Color.Black);


            if (Globals.CurrnetState == State.MainMenu)
            {
                Globals.mouseVisible = true;
                _spriteBatch.Begin();
                menu.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            else if(Globals.CurrnetState == State.GamePause)
            {
                Globals.mouseVisible = true;
                _spriteBatch.Begin();
                 pauseMenu.Draw(_spriteBatch);
                _spriteBatch.End();
            }
        
            else
            {
                //Globals.mouseVisible = false;
         
                Globals.currentScene.render(_spriteBatch);
            }



            IsMouseVisible = Globals.mouseVisible;
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
            Debug.WriteLine("lost focus");
        }

    }
}