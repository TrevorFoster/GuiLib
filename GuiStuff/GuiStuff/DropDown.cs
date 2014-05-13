using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameGUI {
    class DropDown : Control {
        public event EventHandler selectedIndexChanged;
        public event EventHandler dropOpen;
        public event EventHandler dropClose;

        private Size itemBoxSize;
        private Size buttonSize;

        private Animation itemBoxStates;
        private Animation buttonStates;

        public List<string> items = new List<string>();
        public bool isDropOpen = false;
        public int selectedIndex = -1;

        public DropDown() {
            text = "";
            itemBoxSize = new Size(107, 20);
            buttonSize = new Size(20, 20);
            size = itemBoxSize + buttonSize;

            itemBoxStates = new Animation(new int[2] { 1, 1 });
            buttonStates = new Animation(new int[2] { 1, 1 });
        }

        public override void initialize() {
            itemBoxStates.loadSheet(GUIResources.sheets["selection001"], new Rectangle(101, 10, 107, 20));
            buttonStates.loadSheet(GUIResources.sheets["selection001"], new Rectangle(101, 33, 20, 20));
        }

        protected override void subUpdate(Point menuLocation)
        {
            if (!InputHandler.leftClickRelease()) return;
            if (!isDropOpen) {
                Rectangle buttonRect = new Rectangle(location.X + itemBoxSize.Width, location.Y, buttonSize.Width, buttonSize.Height);
                if (buttonRect.Contains(InputHandler.initialClick) && buttonRect.Contains(InputHandler.releaseClick)) {
                    isDropOpen = true;
                    eventTrigger(dropOpen);
                }
            } else {
                for (int i = 0; i < items.Count; i++) {
                    Rectangle temp = new Rectangle(location.X, location.Y + (i + 1) * itemBoxSize.Height, itemBoxSize.Width, itemBoxSize.Height);
                    if (temp.Contains(InputHandler.initialClick) && temp.Contains(InputHandler.releaseClick)) {
                        selectedIndex = i;
                        text = items[i];
                        eventTrigger(selectedIndexChanged);
                        break;
                    }
                }
                isDropOpen = false;
                eventTrigger(dropClose);
            }
        }

        public override void draw(Point menuLocation) {

            GUIRoot.spriteBatch.Draw(buttonStates.currentFrame(), new Vector2(location.X + itemBoxSize.Width, location.Y), Color.White);
            GUIRoot.spriteBatch.Draw(itemBoxStates.currentFrame(), new Vector2(location.X, location.Y), Color.White);
            GUIRoot.spriteBatch.DrawString(Game1.font, text, new Vector2(location.X, location.Y), Color.Black);

            if (isDropOpen) {
                for (int i = 0; i < items.Count; i++) {
                    GUIRoot.spriteBatch.Draw(itemBoxStates.currentFrame(), new Vector2(location.X, location.Y + (i + 1) * itemBoxSize.Height), Color.White);
                    GUIRoot.spriteBatch.DrawString(Game1.font, items[i], new Vector2(location.X, location.Y + (i + 1) * itemBoxSize.Height), Color.Black);
                }
            }
        }
    }
}
