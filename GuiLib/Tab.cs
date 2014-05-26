using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GuiLib {
    class Tab : Control {
        public event EventHandler onChange;

        private Menu tabContents;
        private Rectangle buttonRect;
        public bool isSelected = false;
        public Orientation orientation = Orientation.Horizontal;

        private AnimationSet frameSet;
        // middle
        private Animation middle;
        // corners
        private Animation topLeft, topRight;
        // edges
        private Animation left, right, top;

        public Tab() {
            orientation = Orientation.Horizontal;
            controlSize = new Size(60, 30);

            frameSet = new AnimationSet();
            middle = new Animation(2, 1);

            topLeft = new Animation(2, 1);
            topRight = new Animation(2, 1);

            left = new Animation(2, 1);
            right = new Animation(2, 1);
            top = new Animation(2, 1);

            frameSet.animations.AddRange(new List<Animation> { middle, topLeft, topRight, left, right, top });
        }

        public override void initialize() {
            //loading the middle portion sprites
            middle.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(156, 181, 96, 48));

            // loading corner sprites
            topLeft.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(156, 102, 16, 8));
            topRight.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(177, 102, 16, 8));

            // loading edge sprites
            left.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(156, 132, 16, 48));
            right.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(173, 132, 16, 48));
            top.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(156, 112, 96, 8));
            frameSet.setFrames(1);

            if (tabContents != null) {
                tabContents.intialize();
            }
            sizeStuff();
        }

        protected override void setSize(int Width, int Height) {
            size = new Size(Width, Height);
            controlSize = new Size(size.Width, size.Height);
            sizeStuff();
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
            if (isSelected)
                newMenu.show();
            else
                newMenu.hide();

            tabContents = newMenu;
        }

        protected override void subUpdate(Vector2 menuLocation) {
            buttonRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height);
            frameSet.update();
            if (InputHandler.leftPressed() && buttonRect.Contains(InputHandler.initialClick)) {
                isSelected = true;
                if (tabContents != null) {
                    tabContents.show();
                }

                selectedHasChanged();
                frameSet.setFrames(0);
                eventTrigger(onChange);
            } else if (InputHandler.leftPressed() && buttonRect.Contains(InputHandler.mouseRect)) {
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

            frameSet.setFrames(1);
            //frameSet.render(true);
            eventTrigger(onChange);
        }

        public override void draw(Vector2 menuLocation) {
            Vector2 drawLoc = location + menuLocation;


            //if(frameSet.rendered != null) GUIRoot.spriteBatch.Draw(frameSet.rendered, drawLoc, Color.White);
            frameSet.draw(drawLoc);

            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text,
                new Vector2(size.Width / 2 - FontManager.fonts[Font.Verdana].MeasureString(text).X / 2, size.Height / 2 - FontManager.fonts[Font.Verdana].MeasureString(text).Y / 2) + drawLoc, Color.Black);

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
