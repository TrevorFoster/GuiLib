﻿using System;
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

        private Animation left;
        private Animation right;
        private Animation middle;

        private Animation buttonStates;

        public List<string> items;
        public bool isDropOpen = false;
        public int selectedIndex = -1;

        public DropDown() {
            text = "";
            items = new List<string>();

            itemBoxSize = new Size(200, 24);
            buttonSize = new Size(24, 24);
            size = itemBoxSize + buttonSize;

            left = new Animation(new int[2] { 1, 1 });
            right = new Animation(new int[2] { 1, 1 });
            middle = new Animation(new int[2] { 1, 1 });
            buttonStates = new Animation(new int[2] { 1, 1 });
        }

        public override void initialize() {
            left.loadSheet(GUIResources.sheets["selection001"], new Rectangle(152, 2, 8, 48));
            right.loadSheet(GUIResources.sheets["selection001"], new Rectangle(194, 2, 8, 48));
            middle.loadSheet(GUIResources.sheets["selection001"], new Rectangle(161, 2, 32, 48));

            buttonStates.loadSheet(GUIResources.sheets["selection001"], new Rectangle(103, 2, 48, 48));
        }

        protected override void subUpdate(Point menuLocation)
        {
            if (!InputHandler.leftClickRelease()) return;
            if (!isDropOpen) {
                Rectangle buttonRect = new Rectangle(location.X + menuLocation.X + itemBoxSize.Width, location.Y + menuLocation.Y, buttonSize.Width, buttonSize.Height);
                if (buttonRect.Contains(InputHandler.initialClick) && buttonRect.Contains(InputHandler.releaseClick)) {
                    isDropOpen = true;
                    eventTrigger(dropOpen);
                }
            } else {
                for (int i = 0; i < items.Count; i++) {
                    Rectangle temp = new Rectangle(location.X + menuLocation.X, location.Y + menuLocation.Y + (i + 1) * itemBoxSize.Height, itemBoxSize.Width, itemBoxSize.Height);
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
            Vector2 drawloc = new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y);

            GUIRoot.spriteBatch.Draw(left.currentFrame(), drawloc, null, Color.White, 0f, Vector2.Zero,
                new Vector2(1f, (float)itemBoxSize.Height / (float)left.frameHeight), SpriteEffects.None, 0);
            GUIRoot.spriteBatch.Draw(middle.currentFrame(), drawloc + new Vector2(left.frameWidth, 0), null, Color.White, 0f, Vector2.Zero,
                new Vector2((float)(itemBoxSize.Width - right.frameWidth) / (float)(middle.frameWidth), (float)itemBoxSize.Height / (float)middle.frameHeight), SpriteEffects.None, 0);
            GUIRoot.spriteBatch.Draw(right.currentFrame(), drawloc + new Vector2(itemBoxSize.Width - right.frameWidth, 0), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1f, (float)itemBoxSize.Height / (float)right.frameHeight), SpriteEffects.None, 0);
            
            GUIRoot.spriteBatch.DrawString(Game1.font, text, drawloc + new Vector2(left.frameWidth, 0), Color.Black);
            GUIRoot.spriteBatch.Draw(buttonStates.currentFrame(), drawloc + new Vector2(itemBoxSize.Width, 0), null, Color.White, 0f, Vector2.Zero,
                new Vector2((float)buttonSize.Width / (float)buttonStates.frameWidth, (float)buttonSize.Height / (float)buttonStates.frameHeight), SpriteEffects.None, 0);

            if (isDropOpen) {
                for (int i = 0; i < items.Count; i++) {
                    GUIRoot.spriteBatch.Draw(left.currentFrame(), drawloc + new Vector2(0, (i + 1) * itemBoxSize.Height), null, Color.White, 0f, Vector2.Zero,
                        new Vector2(1f, (float)itemBoxSize.Height / (float)left.frameHeight), SpriteEffects.None, 0);
                    GUIRoot.spriteBatch.Draw(middle.currentFrame(), drawloc + new Vector2(left.frameWidth, (i + 1) * itemBoxSize.Height), null, Color.White, 0f, Vector2.Zero,
                        new Vector2((float)(itemBoxSize.Width - right.frameWidth) / (float)(middle.frameWidth), (float)itemBoxSize.Height / (float)middle.frameHeight), SpriteEffects.None, 0);
                    GUIRoot.spriteBatch.Draw(right.currentFrame(), drawloc + new Vector2(itemBoxSize.Width - right.frameWidth, (i + 1) * itemBoxSize.Height), null, Color.White, 0f, Vector2.Zero,
                        new Vector2(1f, (float)itemBoxSize.Height / (float)right.frameHeight), SpriteEffects.None, 0);
                    GUIRoot.spriteBatch.DrawString(Game1.font, items[i], drawloc + new Vector2(left.frameWidth, (i + 1) * itemBoxSize.Height), Color.Black);
                }
            }
        }
    }
}
