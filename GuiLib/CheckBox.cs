using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {
    class CheckBox : Control {
        public event EventHandler onChange;

        private Animation boxStates;

        public bool isSelected = false;

        public CheckBox() {
            text = "";
            size = new Size();
            controlSize = new Size(24, 24);
            boxStates = new Animation(new [] { 2, 1 });
        }

        public override void initialize() {
            boxStates.loadSheet(GUIResources.sheets["selection001"], new Rectangle(2, 2, 98, 48));
        }

        protected override void subUpdate(Point menuLocation) {
            Rectangle buttonRect = new Rectangle(location.X + menuLocation.X, location.Y + menuLocation.Y, size.Width, size.Height);
            textSize = new Size((int)Game1.font.MeasureString(text).X, (int)Game1.font.MeasureString(text).Y);
            size = new Size(textSize.Width + controlSize.Width, controlSize.Height);

            if (InputHandler.leftClickRelease()
                && buttonRect.Contains(InputHandler.initialClick)
                && buttonRect.Contains(InputHandler.releaseClick)) {

                isSelected = !isSelected;
                boxStates.frame = isSelected ? 1 : 0;
                eventTrigger(onChange);
            }
        }

        public override void deselect() {
            // if already deselected return
            if (!isSelected) return;

            // change check box state to unchecked
            isSelected = false;
            boxStates.frame = 0;
            eventTrigger(onChange);
        }

        public override void draw(Point menuLocation) {
            // top left corner of check box
            Vector2 drawLoc = new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y);

            // draw the check box
            GUIRoot.spriteBatch.Draw(boxStates.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2(controlSize.Width / (float)boxStates.frameWidth, controlSize.Height / (float)boxStates.frameHeight), SpriteEffects.None, 0);
            // draw the text for the check box
            GUIRoot.spriteBatch.DrawString(Game1.font, text,
                new Vector2(controlSize.Width + 2, controlSize.Height / 2 - textSize.Height / 2) + drawLoc, Color.Black);
        }
    }
}
