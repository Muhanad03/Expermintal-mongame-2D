using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace rpg.UI
{
    public class MainMenu
    {
        Button[] buttons = new Button[3];

        public MainMenu()
        {
            // Define the dimensions of the buttons.
            int buttonWidth = 400;
            int buttonHeight = 100;

  
            int centerX = 1280 / 2;
            int centerY = 720 / 2;

            // Calculate the positions of the buttons to center them on the screen.
            Vector2 playButtonPos = new Vector2(centerX - buttonWidth / 2, centerY - 200);
            Vector2 settingsButtonPos = new Vector2(centerX - buttonWidth / 2, centerY-50);
            Vector2 exitButtonPos = new Vector2(centerX - buttonWidth / 2, centerY+100);
           

            // Create the "Play" button.
            buttons[0] = new Button(playButtonPos, "Play", buttonWidth, buttonHeight, Color.LightGoldenrodYellow);
            buttons[0].OnClick += PlayButton_Clicked; // Attach the click event handler.

            buttons[1] = new Button(settingsButtonPos, "Settings", buttonWidth, buttonHeight, Color.LightGoldenrodYellow);
            buttons[1].OnClick += SettingsButton_Clicked; // Attach the click event handler.

            // Create the "Exit" button.
            buttons[2] = new Button(exitButtonPos, "Exit", buttonWidth, buttonHeight, Color.LightGoldenrodYellow);
            buttons[2].OnClick += ExitButton_Clicked; // Attach the click event handler.
        }

       

        public void Update (float mouseX, float mouseY)
        {
            // Update the buttons (handle input, check for clicks, etc.).
            foreach (Button button in buttons)
            {
                button.Update(mouseX, mouseY);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the buttons.
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }

        // Event handler for the "Play" button click.
        private void PlayButton_Clicked()
        {

            Globals.CurrnetState = Game1.State.GamePlay;
               
        }

        // Event handler for the "Quit" button click.
        private void ExitButton_Clicked()
        {
           System.Environment.Exit(0);
        }
        private void SettingsButton_Clicked()
        {
            
        }
    }

}
