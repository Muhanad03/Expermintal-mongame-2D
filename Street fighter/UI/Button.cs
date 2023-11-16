using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace rpg.UI
{
    public class Button
    {
        public Vector2 pos;
        public string text;
        public int width, height;
        public Color color;
        private bool isMouseOver;
        private bool isMouseLeftButtonDown;

        public event Action OnClick; // Event for handling button clicks.

        public Button(Vector2 pos, string text, int width, int height, Color color)
        {
            this.pos = pos;
            this.text = text;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public void Update(float mouseX, float mouseY)
        {
            Rectangle buttonRect = new Rectangle((int)pos.X, (int)pos.Y, width, height);

            // Check if the mouse is over the button.
            isMouseOver = buttonRect.Contains((int)mouseX, (int)mouseY);

            // Check if the left mouse button is down.
            isMouseLeftButtonDown = Mouse.GetState().LeftButton == ButtonState.Pressed;

            // Check for a click event.
            if (isMouseOver && isMouseLeftButtonDown && !mouseStateOld.LeftButton.HasFlag(ButtonState.Pressed))
            {
                OnClick?.Invoke();
            }

            mouseStateOld = Mouse.GetState();
        }

        private MouseState mouseStateOld;

        public void Draw(SpriteBatch spriteBatch)
        {
            Color drawColor = isMouseOver ? color * 0.8f : color; // Dim the button color if the mouse is over it.

            spriteBatch.FillRectangle(new Rectangle((int)pos.X, (int)pos.Y, width, height), drawColor);

            // Calculate the center of the button.
            float centerX = pos.X + width / 2;
            float centerY = pos.Y + height / 2;

            // Calculate the size of the text.
            Vector2 textSize = Globals.spriteFont.MeasureString(text) * 2f; // Multiply by 2f since you used 2f as the scale in the DrawString method.

            // Calculate the position to draw the text at the center of the button.
            Vector2 textPosition = new Vector2(centerX - textSize.X / 2, centerY - textSize.Y / 2);

            spriteBatch.DrawString(Globals.spriteFont, text, textPosition, Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }
    }

}
