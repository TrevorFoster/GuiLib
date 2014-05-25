using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {
    class CheckBox : Control {
        public event EventHandler onChange;

        private Animation boxStates;

        public CheckBox() {

            controlSize = new Size(24, 24);
            boxStates = new Animation(2, 1);
        }

        public override void initialize() {
            boxStates.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(2, 2, 98, 48));
            size = new Size(controlSize.Width, controlSize.Height);
        }

        protected override void subUpdate(Vector2 menuLocation) {
            Rectangle buttonRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height);

            if (InputHandler.leftClickRelease()
                && buttonRect.Contains(InputHandler.initialClick)
                && buttonRect.Contains(InputHandler.releaseClick)) {

                isSelected = !isSelected;
                boxStates.frame = isSelected ? 1 : 0;
                eventTrigger(onChange);
            }
        }

        protected override void setSize(int Width, int Height) {
            size = new Size(Width + textSize.Width, (int)Math.Max(Height, textSize.Height));
            controlSize = new Size(size.Width, size.Height);
        }

        public override void deselect() {
            // if already deselected return
            if (!isSelected) return;

            // change check box state to unchecked
            isSelected = false;
            boxStates.frame = 0;
            eventTrigger(onChange);
        }

        public override void draw(Vector2 menuLocation) {
            // top left corner of check box
            Vector2 drawLoc = location + menuLocation;

            // draw the check box
            GUIRoot.spriteBatch.Draw(boxStates.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2(controlSize.Width / (float)boxStates.frameWidth, controlSize.Height / (float)boxStates.frameHeight), SpriteEffects.None, 0);
            // draw the text for the check box
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text,
                new Vector2(controlSize.Width + 2, controlSize.Height / 2 - textSize.Height / 2) + drawLoc, Color.Black);
        }
    }
}
