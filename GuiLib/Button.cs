using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {

    class Button : Control {
        public event EventHandler onClick;

        private AnimationSet frameSet;

        // middle
        private Animation middle;
        // corners
        private Animation topLeft, topRight, bottomLeft, bottomRight;
        // edges
        private Animation left, right, top, bottom;

        private Rectangle buttonRect;

        public Button(string text, Vector2 location, Size size)
            : base(text, location, size) {
            frameSet = new AnimationSet();

            middle = new Animation(2, 1, Sheet.MainSheet);

            topLeft = new Animation(2, 1, Sheet.MainSheet);
            topRight = new Animation(2, 1, Sheet.MainSheet);
            bottomLeft = new Animation(2, 1, Sheet.MainSheet);
            bottomRight = new Animation(2, 1, Sheet.MainSheet);

            left = new Animation(2, 1, Sheet.MainSheet);
            right = new Animation(2, 1, Sheet.MainSheet);
            bottom = new Animation(2, 1, Sheet.MainSheet);
            top = new Animation(2, 1, Sheet.MainSheet);

            frameSet.animations.AddRange(new List<Animation> { middle, left, right, top, bottom, topLeft, topRight, bottomLeft, bottomRight });
        }

        public Button() {
            frameSet = new AnimationSet();

            middle = new Animation(2, 1, Sheet.MainSheet);

            topLeft = new Animation(2, 1, Sheet.MainSheet);
            topRight = new Animation(2, 1, Sheet.MainSheet);
            bottomLeft = new Animation(2, 1, Sheet.MainSheet);
            bottomRight = new Animation(2, 1, Sheet.MainSheet);

            left = new Animation(2, 1, Sheet.MainSheet);
            right = new Animation(2, 1, Sheet.MainSheet);
            bottom = new Animation(2, 1, Sheet.MainSheet);
            top = new Animation(2, 1, Sheet.MainSheet);

            frameSet.animations.AddRange(new List<Animation> { middle, left, right, top, bottom, topLeft, topRight, bottomLeft, bottomRight });

            this.setSize(80, 30);
        }

        public override void initialize() {
            //loading the middle portion sprites
            middle.loadSheet(new Rectangle(3, 181, 96, 48));

            // loading corner sprites
            topLeft.loadSheet(new Rectangle(3, 102, 16, 8));
            topRight.loadSheet(new Rectangle(25, 102, 16, 8));
            bottomLeft.loadSheet(new Rectangle(45, 102, 16, 8));
            bottomRight.loadSheet(new Rectangle(67, 102, 16, 8));

            // loading edge sprites
            left.loadSheet(new Rectangle(3, 132, 16, 48));
            right.loadSheet(new Rectangle(23, 132, 16, 48));
            top.loadSheet(new Rectangle(3, 112, 96, 8));
            bottom.loadSheet(new Rectangle(3, 121, 96, 8));

            sizeStuff();

            base.initialize();
        }

        protected override void sizeChanged(object sender, EventArgs e) {
            sizeStuff();
        }

        protected override void setSize(int Width, int Height) {
            realSize = new Size(Width, Height);
            controlSize = realSize;
            
            if(initialized)
                sizeStuff();
        }

        private void sizeStuff() {
            middle.updateScale(new Vector2(realSize.Width, realSize.Height));

            left.updateScale(new Vector2(left.frameWidth, realSize.Height));
            right.updateScale(new Vector2(right.frameWidth, realSize.Height));
            top.updateScale(new Vector2(realSize.Width, top.frameHeight));
            bottom.updateScale(new Vector2(realSize.Width, bottom.frameHeight));

            topRight.offset = new Vector2(realSize.Width - topRight.frameWidth, 0);
            bottomLeft.offset = new Vector2(0, realSize.Height - bottomLeft.frameHeight);
            bottomRight.offset = new Vector2(realSize.Width - bottomRight.frameWidth, realSize.Height - bottomRight.frameHeight);

            right.offset = new Vector2(realSize.Width - right.frameWidth, 0);
            bottom.offset = new Vector2(0, realSize.Height - bottom.frameHeight);
        }

        protected override void subUpdate(Vector2 menuLocation) {
            buttonRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), realSize.Width, realSize.Height);

            if (InputHandler.leftClickRelease()
                && buttonRect.Contains(InputHandler.initialClick)
                && buttonRect.Contains(InputHandler.releaseClick)) {

                eventTrigger(onClick);
            } else if (InputHandler.leftPressed() && buttonRect.Contains(InputHandler.mouseRect)) {
                frameSet.setFrames(1);
                //frameSet.render();
            } else {
                frameSet.setFrames(0);

            }
        }

        public override void draw(Vector2 menuLocation) {
            Vector2 drawLoc = location + menuLocation;

            //if (frameSet.rendered != null) GUIRoot.spriteBatch.Draw(frameSet.rendered, drawLoc, Color.White);
            frameSet.draw(drawLoc);

            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text,
                new Vector2(realSize.Width / 2 - textSize.Width / 2, realSize.Height / 2 - textSize.Height / 2) + drawLoc,
                Color.Black);
        }
    }
}
