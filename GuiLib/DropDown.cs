using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {
    class DropDown : Control {
        public event EventHandler selectedIndexChanged;
        public event EventHandler dropOpen;
        public event EventHandler dropClose;

        private Size itemBoxSize;
        private Size buttonSize;

        private AnimationSet itemBoxAnimation;
        private Animation left, right, top, bottom, middle, topLeft, topRight, bottomLeft, bottomRight;
        private Animation buttonStates;

        public List<string> items;
        public bool isDropOpen = false;
        public int selectedIndex = -1;

        public DropDown() {
            items = new List<string>();

            itemBoxSize = new Size(200, 28);
            buttonSize = new Size(28, 28);
            realSize = new Size(itemBoxSize.Width + buttonSize.Width, Math.Max(itemBoxSize.Height, buttonSize.Height));

            itemBoxAnimation = new AnimationSet();
            left = new Animation(1, 1, Sheet.MainSheet);
            right = new Animation(1, 1, Sheet.MainSheet);
            top = new Animation(1, 1, Sheet.MainSheet);
            bottom = new Animation(1, 1, Sheet.MainSheet);

            middle = new Animation(1, 1, Sheet.MainSheet);

            topLeft = new Animation(1, 1, Sheet.MainSheet);
            topRight = new Animation(1, 1, Sheet.MainSheet);
            bottomLeft = new Animation(1, 1, Sheet.MainSheet);
            bottomRight = new Animation(1, 1, Sheet.MainSheet);
            itemBoxAnimation.animations.AddRange(new List<Animation> { left, right, top, bottom, middle, topLeft, topRight, bottomLeft, bottomRight });

            buttonStates = new Animation(1, 1, Sheet.MainSheet);
        }

        public override void initialize() {
            left.loadSheet(new Rectangle(152, 10, 8, 32));
            right.loadSheet(new Rectangle(194, 10, 8, 32));
            top.loadSheet(new Rectangle(161, 2, 32, 8));
            bottom.loadSheet(new Rectangle(161, 42, 32, 8));

            middle.loadSheet(new Rectangle(204, 10, 32, 32));

            topLeft.loadSheet(new Rectangle(152, 2, 8, 8));
            topRight.loadSheet(new Rectangle(194, 2, 8, 8));
            bottomLeft.loadSheet(new Rectangle(152, 42, 8, 8));
            bottomRight.loadSheet(new Rectangle(194, 42, 8, 8));

            buttonStates.loadSheet(new Rectangle(103, 2, 48, 48));
            sizeStuff();
        }

        protected override void subUpdate(Vector2 menuLocation) {
            if (!InputHandler.leftClickRelease()) return;

            if (!isDropOpen) {
                Rectangle buttonRect = new Rectangle((int)(location.X + menuLocation.X + itemBoxSize.Width), (int)(location.Y + menuLocation.Y), buttonSize.Width, buttonSize.Height);
                if (buttonRect.Contains(InputHandler.initialClick) && buttonRect.Contains(InputHandler.releaseClick)) {
                    isDropOpen = true;
                    eventTrigger(dropOpen);
                }
            } else {
                Vector2 startLoc = location + menuLocation;
                Rectangle itemBox = new Rectangle((int)(startLoc.X), (int)(startLoc.Y), itemBoxSize.Width, itemBoxSize.Height);

                for (int i = 0; i < items.Count; i++) {
                    itemBox.Y += itemBoxSize.Height;

                    if (itemBox.Contains(InputHandler.initialClick) && itemBox.Contains(InputHandler.releaseClick)) {
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

        private void sizeStuff() {
            middle.updateScale(new Vector2(itemBoxSize.Width - right.frameWidth * 2, itemBoxSize.Height - bottom.frameHeight * 2));

            left.updateScale(new Vector2(left.frameWidth, itemBoxSize.Height - topLeft.frameHeight * 2));
            right.updateScale(new Vector2(right.frameWidth, itemBoxSize.Height - topRight.frameHeight * 2));

            top.updateScale(new Vector2(itemBoxSize.Width - right.frameWidth * 2, top.frameHeight));
            bottom.updateScale(new Vector2(itemBoxSize.Width - right.frameWidth * 2, top.frameHeight));

            left.offset = new Vector2(0, topLeft.frameHeight);
            right.offset = new Vector2(itemBoxSize.Width - right.frameWidth, topRight.frameHeight);
            top.offset = new Vector2(topLeft.frameWidth, 0);
            bottom.offset = new Vector2(bottomLeft.frameWidth, itemBoxSize.Height - bottom.frameHeight);
            middle.offset = new Vector2(topLeft.frameWidth, topLeft.frameHeight);

            topRight.offset = new Vector2(itemBoxSize.Width - topRight.frameWidth, 0);
            bottomLeft.offset = new Vector2(0, itemBoxSize.Height - bottomLeft.frameHeight);
            bottomRight.offset = new Vector2(itemBoxSize.Width - bottomRight.frameWidth, itemBoxSize.Height - bottomRight.frameHeight);

            buttonStates.updateScale(new Vector2(buttonSize.Width, buttonSize.Height));
        }

        protected override void setSize(int Width, int Height) {
            realSize = new Size(Width + buttonSize.Width, Height);
            itemBoxSize = new Size(Width, realSize.Height);
            buttonSize = new Size(buttonSize.Width, realSize.Height);

            sizeStuff();
        }

        public override void draw(Vector2 menuLocation) {
            Vector2 drawloc = new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y);


            buttonStates.draw(drawloc + new Vector2(itemBoxSize.Width, 0));
            itemBoxAnimation.draw(drawloc);
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text, drawloc + new Vector2(left.frameWidth, 0), Color.Black);

            if (!isDropOpen) return;

            Vector2 boxLoc = new Vector2(0, 0);
            for (int i = 0; i < items.Count; i++) {
                boxLoc.Y += itemBoxSize.Height;

                itemBoxAnimation.draw(drawloc + boxLoc);
                GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], items[i], drawloc + boxLoc + new Vector2(left.frameWidth, 0), Color.Black);
            }
        }
    }
}