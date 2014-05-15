using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameGUI {
    class Tab : Control {
        public event EventHandler onChange;

        private Menu tabContents;
        private Rectangle buttonRect;

        private AnimationSet frameSet;
        // middle
        private Animation middle;
        // corners
        private Animation topLeft, topRight, bottomLeft, bottomRight;
        // edges
        private Animation left, right, top, bottom;

        public Orientation orientation = Orientation.Horizontal;
        public bool isSelected = false;

        public Tab() {
            text = "";
            size = new Size(60, 30);

            frameSet = new AnimationSet();
            middle = new Animation(new int[] { 2, 1 });

            topLeft = new Animation(new int[] { 2, 1 });
            topRight = new Animation(new int[] { 2, 1 });
            bottomLeft = new Animation(new int[] { 2, 1 });
            bottomRight = new Animation(new int[] { 2, 1 });

            left = new Animation(new int[] { 2, 1 });
            right = new Animation(new int[] { 2, 1 });
            bottom = new Animation(new int[] { 2, 1 });
            top = new Animation(new int[] { 2, 1 });

            frameSet.animations.AddRange(new List<Animation> { 
                middle, 
                topLeft, 
                topRight, 
                bottomLeft, 
                bottomRight, 
                left, 
                right, 
                bottom, 
                top });
        }

        public override void initialize() {
            //loading the middle portion sprites
            middle.loadSheet(GUIResources.sheets["selection001"], new Rectangle(3, 181, 96, 48));

            // loading corner sprites
            topLeft.loadSheet(GUIResources.sheets["selection001"], new Rectangle(3, 102, 16, 8));
            topRight.loadSheet(GUIResources.sheets["selection001"], new Rectangle(25, 102, 16, 8));
            bottomLeft.loadSheet(GUIResources.sheets["selection001"], new Rectangle(45, 102, 16, 8));
            bottomRight.loadSheet(GUIResources.sheets["selection001"], new Rectangle(67, 102, 16, 8));

            // loading edge sprites
            left.loadSheet(GUIResources.sheets["selection001"], new Rectangle(3, 132, 16, 48));
            right.loadSheet(GUIResources.sheets["selection001"], new Rectangle(23, 132, 16, 48));
            top.loadSheet(GUIResources.sheets["selection001"], new Rectangle(3, 112, 96, 8));
            bottom.loadSheet(GUIResources.sheets["selection001"], new Rectangle(3, 121, 96, 8));

            if (tabContents != null) {
                tabContents.intialize();
            }
        }

        public void moveContents(Point newPos) {
            if (tabContents == null) return;
            tabContents.location = new Point(newPos.X, newPos.Y);
        }

        public void setMenu(Menu newMenu) {
            if (isSelected) {
                newMenu.show();
            } else {
                newMenu.hide();
            }
            tabContents = newMenu;   
        }

        protected override void subUpdate(Point menuLocation) {
            buttonRect = new Rectangle(location.X + menuLocation.X, location.Y + menuLocation.Y, size.Width, size.Height);

            if (InputHandler.leftClickRelease()
                && buttonRect.Contains(InputHandler.initialClick)
                && buttonRect.Contains(InputHandler.releaseClick)) {

                isSelected = true;
                if (tabContents != null) {
                    tabContents.show();
                }

                if (groupIndex != -1) {
                    ControlGroups.groups[groupIndex].changeSelected(this);
                }
                frameSet.setFrame(1);


                eventTrigger(onChange);
            } else if (InputHandler.leftPressed() && buttonRect.Contains(InputHandler.mouseRect)) {
                frameSet.setFrame(1);
            } else if (!isSelected) {
                frameSet.setFrame(0);
            }

            if (isSelected && tabContents != null) {
                Point contentOffs = new Point(menuLocation.X, menuLocation.Y);

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

            frameSet.setFrame(0);
            eventTrigger(onChange);
        }

        public override void draw(Point menuLocation) {
            Vector2 drawLoc = new Vector2(location.X, location.Y) + new Vector2(menuLocation.X, menuLocation.Y);

            GUIRoot.spriteBatch.Draw(middle.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
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
            GUIRoot.spriteBatch.Draw(topRight.currentFrame(), new Vector2(buttonRect.Right - topRight.frameWidth, buttonRect.Top), Color.White);
            //GUI.spriteBatch.Draw(bottomLeft.currentFrame(), new Vector2(buttonRect.Left, buttonRect.Bottom - bottomLeft.frameHeight), Color.White);
            //GUI.spriteBatch.Draw(bottomRight.currentFrame(), new Vector2(buttonRect.Right - bottomRight.frameWidth, buttonRect.Bottom - bottomRight.frameHeight), Color.White);

            GUIRoot.spriteBatch.DrawString(Game1.font, text,
                new Vector2(size.Width / 2 - Game1.font.MeasureString(text).X / 2, size.Height / 2 - Game1.font.MeasureString(text).Y / 2) + drawLoc, Color.Black);

            if (tabContents != null) {
                Point contentOffs = new Point(menuLocation.X, menuLocation.Y);

                switch(orientation) {
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
