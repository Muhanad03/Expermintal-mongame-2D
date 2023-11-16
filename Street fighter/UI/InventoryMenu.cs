using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using rpg.input;
using System.Collections.Generic;
using System.Diagnostics;

namespace rpg.UI
{
    public class InventoryMenu
    {
        private List<Item> items;
        private int rows = 6;
        private int columns = 8;
        private int slotSize = 64;
        private int padding = 5;
        private Vector2 position;
        private int hoveredSlotX = -1;
        private int hoveredSlotY = -1;
        public bool Visible = false;
        public InventoryMenu(Vector2 position)
        {
            this.position = position;
            items = new List<Item>();
           
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }
        private float timeSinceLastEKeyPress = 0f;
        private const float eKeyPressDelay = 0.3f;
      
        public void Update(MouseState mouseState,GameTime gameTime)
        {
            Vector2 mousePosition = Globals.adjustedMousePosRelativeToRes;
     
            timeSinceLastEKeyPress += Globals.deltaTime;
            hoveredSlotX = -1;
            hoveredSlotY = -1;
            if (Visible)
            {
                // Check if the mouse is hovering over any slot
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        Rectangle slotBounds = new Rectangle(
                            (int)(position.X + x * (slotSize + padding)),
                            (int)(position.Y + y * (slotSize + padding)),
                            slotSize,
                            slotSize
                        );

                        if (slotBounds.Contains(mousePosition))
                        {
                            hoveredSlotX = x;
                            hoveredSlotY = y;
                        }
                    }
                }

                // Check if the mouse is clicking on any slot
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        for (int x = 0; x < columns; x++)
                        {
                            Rectangle slotBounds = new Rectangle(
                                (int)(position.X + x * (slotSize + padding)),
                                (int)(position.Y + y * (slotSize + padding)),
                                slotSize,
                                slotSize
                            );

                            if (slotBounds.Contains(mousePosition))
                            {
                                // Handle click behavior for the slot 

                               
                                
                            }
                        }
                    }
                }

            }
           


            if (keyListener.E && timeSinceLastEKeyPress >= eKeyPressDelay)
            {
                viewMenu();
                timeSinceLastEKeyPress = 0f; // Reset the time since last 'E' key press
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Rectangle(0, 0, 1280, 720), Color.Black * 0.8f, 0f);
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int index = y * columns + x;
                    Vector2 slotPosition = position + new Vector2(x * (slotSize + padding), y * (slotSize + padding));

                    Color slotColor = (x == hoveredSlotX && y == hoveredSlotY) ? Color.Yellow : Color.Gray;
                    spriteBatch.FillRectangle(new Rectangle((int)slotPosition.X, (int)slotPosition.Y, slotSize, slotSize), slotColor);

                    if (index < items.Count)
                    {
                        Item item = items[index];
                        Vector2 itemPosition = slotPosition + new Vector2(padding);
                        spriteBatch.Draw(item.Texture, itemPosition, Color.White);
                    }
                }
            }
        }


        private void viewMenu()
        {
            if (Visible == true)
            {
                Visible = false;

            }
            else
            {
                Visible = true;
            }
        }
    }

    public class Item
    {
        public Texture2D Texture { get; set; }

        public Item(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
