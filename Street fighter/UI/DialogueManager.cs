using Microsoft.Xna.Framework.Graphics;
using rpg.input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg.UI
{
    public class DialogueManager
    {
        public bool isActive = false;
     
        private float timeSinceLastEKeyPress = 0f;
        private const float eKeyPressDelay = 0.3f;
        DialogueUI ui;
        public DialogueManager()
        {

        }

        public void update()
        {
            timeSinceLastEKeyPress += Globals.deltaTime;
            if (isActive)
            {
                if (keyListener.ENTER && timeSinceLastEKeyPress >= eKeyPressDelay)
                {
                    ui.dialogue.NextLine();
                    ui.currentLetterIndex = 0;
                    timeSinceLastEKeyPress = 0f; // Reset the timer
                }
                if (ui.dialogue.IsDialogueComplete())
                {
                    stopDialogue();
                }
            }
            
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if(isActive)
            {
                ui.DrawDialogue(spriteBatch);
            }
        }
        public void startDialogue(Dialogue d)
        {
            ui = new DialogueUI(d);
            isActive = true;
            ui.currentLetterIndex = 0; // Reset the letter index
            ui.isLineDrawn = false;    // Reset the flag
        }

        public void stopDialogue()
        {
            isActive = false;
            ui.dialogue.restart();
            
         
        }
    }
}
