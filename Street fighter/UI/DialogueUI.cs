using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace rpg.UI
{
    public class DialogueUI
    {
        public Dialogue dialogue;
        public int currentLetterIndex = 0;
        private float displayLetterTimer = 0f;
        private const float displayLetterSpeed = 0.03f; // Adjust the speed as needed
        public bool isLineDrawn = false;

        public DialogueUI(Dialogue d)
        {
            dialogue = d;
        }

        public void DrawDialogue(SpriteBatch _spriteBatch)
        {
            if (!isLineDrawn && dialogue != null)
            {
                string currentLine = dialogue.GetCurrentLine();
                if (currentLine != null)
                {
                    // Draw dialogue box and position
                    Rectangle dialogueBoxRect = new Rectangle(300, 500, 700, 100);
                    _spriteBatch.FillRectangle(dialogueBoxRect, Color.LightGray);

                    // Draw displayed text letter by letter
                    Vector2 textPosition = new Vector2(dialogueBoxRect.X + 10, dialogueBoxRect.Y + 10);
                    string displayedText = currentLine.Substring(0, currentLetterIndex);
                    _spriteBatch.DrawString(Globals.spriteFont, displayedText, textPosition, Color.Black);

                    // Update the display timer for letters
                    displayLetterTimer += Globals.deltaTime;
                    if (displayLetterTimer >= displayLetterSpeed)
                    {
                        // Move to the next letter and reset the timer
                        
                        displayLetterTimer = 0f;
                        if (currentLetterIndex < currentLine.Length)
                        {
                            currentLetterIndex++;
                        }

                        if (currentLetterIndex > currentLine.Length)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}
