using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Serialization;

namespace rpg.input
{
    public class keyListener
    {
        public static bool W, S, A, D, LSHIFT,F,E,ESC,ENTER,MouseLeftClick,MouseRightClick,P;
        private float timeSinceLastMouseClick = 0f;
        private const float mouseClickCooldown = 0.2f;
        private bool isGKeyPressedPreviously = false;

        public keyListener() { }


        public void update()
        {

            MouseState mouseState = Mouse.GetState();
            W = (Keyboard.GetState().IsKeyDown(Keys.W)) ? true : false;
            A = (Keyboard.GetState().IsKeyDown(Keys.A)) ? true : false;
            S = (Keyboard.GetState().IsKeyDown(Keys.S)) ? true : false;
            D = (Keyboard.GetState().IsKeyDown(Keys.D)) ? true : false;
            F = (Keyboard.GetState().IsKeyDown(Keys.F)) ? true : false;
            E = (Keyboard.GetState().IsKeyDown(Keys.E)) ? true : false;
            P = (Keyboard.GetState().IsKeyDown(Keys.P)) ? true : false;
            ENTER = (Keyboard.GetState().IsKeyDown(Keys.Enter)) ? true : false;
            ESC = (Keyboard.GetState().IsKeyDown(Keys.Escape)) ? true : false;
            LSHIFT = (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) ? true : false;
            if (Keyboard.GetState().IsKeyDown(Keys.B)) { Globals.camera.Shake(3, 100f); }

            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                // Check if 'G' key was not pressed in the previous frame
                if (!isGKeyPressedPreviously)
                {
                    Debug.WriteLine(Globals.camera.Zoom);

                    if (Globals.camera.Zoom != 0.5f)
                    {
                        Globals.camera.Zoom -= 0.1f;
                    }
                    else
                    {
                        Globals.camera.Zoom = 1f;
                    }
                }

                // Set the flag to true to indicate the 'G' key was pressed in this frame
                isGKeyPressedPreviously = true;
            }
            else
            {
                // Set the flag to false when the 'G' key is not pressed
                isGKeyPressedPreviously = false;
            }

            timeSinceLastMouseClick += Globals.deltaTime;

            bool canFire = mouseState.LeftButton == ButtonState.Pressed && timeSinceLastMouseClick >= mouseClickCooldown;
            MouseLeftClick = canFire;
            bool canFire2 = mouseState.RightButton == ButtonState.Pressed && timeSinceLastMouseClick >= mouseClickCooldown;
            MouseRightClick = canFire2;
            // Reset click timer if clicked
            if (canFire)
            {
                timeSinceLastMouseClick = 0f;
            }
            if (canFire2)
            {
                timeSinceLastMouseClick = 0f;
            }

            

        }


    }
}
