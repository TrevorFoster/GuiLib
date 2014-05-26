﻿using System;
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

        }

        public Button() {
            this.resize(80, 50);
            
        }

        public override void initialize() {
            frameSet = new AnimationSet();

            middle = new Animation(2, 1);

            topLeft = new Animation(2, 1);
            topRight = new Animation(2, 1);
            bottomLeft = new Animation(2, 1);
            bottomRight = new Animation(2, 1);

            left = new Animation(2, 1);
            right = new Animation(2, 1);
            bottom = new Animation(2, 1);
            top = new Animation(2, 1);

            frameSet.animations.AddRange(new List<Animation> { middle, left, right, top, bottom, topLeft, topRight, bottomLeft, bottomRight });

            //loading the middle portion sprites
            middle.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(3, 181, 96, 48));

            // loading corner sprites
            topLeft.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(3, 102, 16, 8));
            topRight.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(25, 102, 16, 8));
            bottomLeft.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(45, 102, 16, 8));
            bottomRight.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(67, 102, 16, 8));

            // loading edge sprites
            left.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(3, 132, 16, 48));
            right.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(23, 132, 16, 48));
            top.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(3, 112, 96, 8));
            bottom.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(3, 121, 96, 8));
            sizeStuff();
            base.initialize();
        }

        protected override void setSize(int Width, int Height) {
            size = new Size(Width, Height);
            controlSize = new Size(size.Width, size.Height);
            if (initialized)
                sizeStuff();
        }

        private void sizeStuff() {
            middle.updateScale(new Vector2(size.Width, size.Height));

            left.updateScale(new Vector2(left.frameWidth, size.Height));
            right.updateScale(new Vector2(right.frameWidth, size.Height));
            top.updateScale(new Vector2(size.Width, top.frameHeight));
            bottom.updateScale(new Vector2(size.Width, bottom.frameHeight));

            topRight.offset = new Vector2(size.Width - topRight.frameWidth, 0);
            bottomLeft.offset = new Vector2(0, size.Height - bottomLeft.frameHeight);
            bottomRight.offset = new Vector2(size.Width - bottomRight.frameWidth, size.Height - bottomRight.frameHeight);

            right.offset = new Vector2(size.Width - right.frameWidth, 0);
            bottom.offset = new Vector2(0, size.Height - bottom.frameHeight);
        }

        protected override void subUpdate(Vector2 menuLocation) {
            buttonRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height);

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
                new Vector2(size.Width / 2 - textSize.Width / 2, size.Height / 2 - textSize.Height / 2) + drawLoc,
                Color.Black);
        }
    }
}
