using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpg.input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg.UI
{
    public class pauseMenu
    {
        Button[] buttons = new Button[2];
        public bool visible = false;
        InventoryMenu inventory;
        public pauseMenu(InventoryMenu inventory)
        {

            this.inventory = inventory;
            // Define the dimensions of the buttons.
            int buttonWidth = 400;
            int buttonHeight = 100;


            int centerX = 1280 / 2;
            int centerY = 720 / 2;

            // Calculate the positions of the buttons to center them on the screen.
            Vector2 playButtonPos = new Vector2(centerX - buttonWidth / 2, centerY - 150);
            Vector2 quitButtonPos = new Vector2(centerX - buttonWidth / 2, centerY);

            // Create the "Play" button.
            buttons[0] = new Button(playButtonPos, "Continue", buttonWidth, buttonHeight, Color.GhostWhite);
            buttons[0].OnClick += Continue_Clicked; // Attach the click event handler.

            // Create the "Quit" button.
            buttons[1] = new Button(quitButtonPos, "Quit", buttonWidth, buttonHeight, Color.GhostWhite);
            buttons[1].OnClick += QuitButton_Clicked; // Attach the click event handler.
        }
        private float timeSinceLastEKeyPress = 0f;
        private const float eKeyPressDelay = 0.3f;
        public void Update(float mouseX, float mouseY)
        {
            // Update the buttons (handle input, check for clicks, etc.).
            timeSinceLastEKeyPress += Globals.deltaTime;
            if (visible)
            {
                foreach (Button button in buttons)
                {
                    button.Update(mouseX, mouseY);
                }
            }

            if (keyListener.ESC && timeSinceLastEKeyPress >= eKeyPressDelay&&inventory.Visible == false&&Globals.CurrnetState is not Game1.State.MainMenu)
            {
                makeVisible();
                timeSinceLastEKeyPress = 0f; // Reset the time since last 'E' key press
            }

        }

        private void makeVisible()
        {
            if (visible == true)
            {
                visible = false;
                Globals.CurrnetState = Game1.State.GamePlay;

            }
            else
            {
                visible = true;
                Globals.CurrnetState = Game1.State.GamePause;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the buttons.

            int centerX = 1280 / 2;
            int centerY = 720 / 2;
            int backgroundWidth = 400*2;
            int backgroundHeight = 250*2;
            Vector2 backgroundPosition = new Vector2(centerX - backgroundWidth / 2, centerY - backgroundHeight / 2);
            DrawBackground(spriteBatch, backgroundPosition, backgroundWidth, backgroundHeight, 0.7f);
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }
        private void DrawBackground(SpriteBatch spriteBatch, Vector2 position, int width, int height, float alpha)
        {
            // Create a texture with a single white pixel.
            Texture2D pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            // Set the rectangle size and position based on the width, height, and position passed to the method.
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, width, height);

            // Draw the background rectangle with the desired color and opacity (alpha).
            spriteBatch.Draw(pixel, rect, Color.Black * alpha);
        }



        // Event handler for the "Play" button click.
        private void Continue_Clicked()
        {


            makeVisible();

        }

        // Event handler for the "Quit" button click.
        private void QuitButton_Clicked()
        {
            visible = false;
            Globals.CurrnetState = Game1.State.MainMenu;

        }
    }
}
