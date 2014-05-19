using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GuiLib {
    class Tab : Control {
        public event EventHandler onChange;

        private Menu tabContents;
        private Rectangle buttonRect;

        private AnimationSet frameSet;
        // middle
        private Animation middle;
        // corners
        private Animation topLeft, topRight;
        // edges
        private Animation left, right, top;

        public Orientation orientation = Orientation.Horizontal;
        public bool isSelected = false;

        public Tab()
            : base() {
            resized += resize;
            text = "";
            controlSize = new Size(60, 30);

            frameSet = new AnimationSet();
            middle = new Animation(1, 1);

            topLeft = new Animation(1, 1);
            topRight = new Animation(1, 1);

            left = new Animation(1, 1);
            right = new Animation(1, 1);
            top = new Animation(1, 1);

            frameSet.animations.AddRange(new List<Animation> { middle, topLeft, topRight, left, right, top });
        }

        public override void initialize() {
            //loading the middle portion sprites
            middle.loadSheet(GUIResources.sheets["selection001"], new Rectangle(156, 181, 48, 48));

            // loading corner sprites
            topLeft.loadSheet(GUIResources.sheets["selection001"], new Rectangle(156, 102, 8, 8));
            topRight.loadSheet(GUIResources.sheets["selection001"], new Rectangle(165, 102, 8, 8));

            // loading edge sprites
            left.loadSheet(GUIResources.sheets["selection001"], new Rectangle(156, 132, 8, 48));
            right.loadSheet(GUIResources.sheets["selection001"], new Rectangle(165, 132, 16, 48));
            top.loadSheet(GUIResources.sheets["selection001"], new Rectangle(156, 112, 48, 8));

            if (tabContents != null) {
                tabContents.intialize();
            }
            sizeStuff();
        }

        private void resize(object sender, EventArgs e) {
            size = new Size(controlSize.Width, controlSize.Height);
        }

        private void sizeStuff() {
            middle.updateScale(new Vector2(size.Width - right.frameWidth, size.Height - top.frameHeight));

            left.updateScale(new Vector2(left.frameWidth, size.Height - top.frameHeight));
            right.updateScale(new Vector2(right.frameWidth, size.Height - top.frameHeight));
            top.updateScale(new Vector2(size.Width - right.frameWidth - topRight.frameWidth, top.frameHeight));

            top.offset = new Vector2(topLeft.frameWidth, 0);
            topRight.offset = new Vector2(size.Width - right.frameWidth, 0);
            left.offset = new Vector2(0, topLeft.frameHeight);
            right.offset = new Vector2(size.Width - right.frameWidth, topRight.frameHeight);
            middle.offset = new Vector2(left.frameWidth, top.frameHeight);
            
        }

        public void moveContents(Vector2 newPos) {
            if (tabContents == null) return;
            tabContents.location = new Vector2(newPos.X, newPos.Y);
        }

        public void setMenu(Menu newMenu) {
            if (isSelected) {
                newMenu.show();
            } else {
                newMenu.hide();
            }
            tabContents = newMenu;
        }

        protected override void subUpdate(Vector2 menuLocation) {
            buttonRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height);

            if (InputHandler.leftClickRelease()
                && buttonRect.Contains(InputHandler.initialClick)
                && buttonRect.Contains(InputHandler.releaseClick)) {

                isSelected = true;
                if (tabContents != null) {
                    tabContents.show();
                }

                selectedHasChanged();
                frameSet.setFrames(1);
                eventTrigger(onChange);

            } else if (InputHandler.leftPressed() && buttonRect.Contains(InputHandler.mouseRect)) {
                frameSet.setFrames(1);
            } else if (!isSelected) {
                frameSet.setFrames(0);
            }

            if (isSelected && tabContents != null) {
                Vector2 contentOffs = new Vector2(menuLocation.X, menuLocation.Y);

                switch (orientation) {
                    case Orientation.Horizontal:
                        contentOffs.Y += size.Height;
                        break;

                    case Orientation.Vertical:
                        contentOffs.X += size.Width;
                        break;
                }
                tabContents.update(contentOffs);
            }
        }

        public override void deselect() {
            if (!isSelected) return;

            isSelected = false;
            if (tabContents != null) tabContents.hide();

            frameSet.setFrames(0);
            eventTrigger(onChange);
        }

        public override void draw(Vector2 menuLocation) {
            Vector2 drawLoc = location + menuLocation;

            /*GUIRoot.spriteBatch.Draw(middle.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2((float)size.Width / (float)middle.frameWidth, (float)size.Height / (float)middle.frameHeight), SpriteEffects.None, 0);

            GUIRoot.spriteBatch.Draw(left.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2(1f, (float)size.Height / (float)left.frameHeight), SpriteEffects.None, 0);
            GUIRoot.spriteBatch.Draw(right.currentFrame(), drawLoc + new Vector2(size.Width - right.frameWidth, 0), null, Color.White, 0f, Vector2.Zero,
                new Vector2(1f, (float)size.Height / (float)right.frameHeight), SpriteEffects.None, 0);
            GUIRoot.spriteBatch.Draw(top.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2((float)size.Width / ((float)top.frameWidth), 1f), SpriteEffects.None, 0);
            //Game1.spriteBatch.Draw(bottom.currentFrame(), drawLoc + new Vector2(0, size.Height - 8), null, Color.White, 0f, Vector2.Zero,
            //  new Vector2((float)size.Width / ((float)bottom.frameWidth), 1f), SpriteEffects.None, 0);

            GUIRoot.spriteBatch.Draw(topLeft.currentFrame(), drawLoc, Color.White);
            GUIRoot.spriteBatch.Draw(topRight.currentFrame(), new Vector2(drawLoc.X + size.Width - topRight.frameWidth, drawLoc.Y), Color.White);
            //GUI.spriteBatch.Draw(bottomLeft.currentFrame(), new Vector2(buttonRect.Left, buttonRect.Bottom - bottomLeft.frameHeight), Color.White);
            //GUI.spriteBatch.Draw(bottomRight.currentFrame(), new Vector2(buttonRect.Right - bottomRight.frameWidth, buttonRect.Bottom - bottomRight.frameHeight), Color.White);
            */
            frameSet.draw(drawLoc);

            GUIRoot.spriteBatch.DrawString(Game1.font, text,
                new Vector2(size.Width / 2 - Game1.font.MeasureString(text).X / 2, size.Height / 2 - Game1.font.MeasureString(text).Y / 2) + drawLoc, Color.Black);

            if (tabContents != null) {
                Vector2 contentOffs = new Vector2(menuLocation.X, menuLocation.Y);

                switch (orientation) {
                    case Orientation.Horizontal:
                        contentOffs.Y += size.Height;
                        break;

                    case Orientation.Vertical:
                        contentOffs.X += size.Width;
                        break;
                }
                tabContents.draw(contentOffs);
            }
        }
    }
}
