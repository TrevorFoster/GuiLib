using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {

    class RadioButton : Control {
        public event EventHandler onChange;

        private Animation buttonStates;

        public bool isSelected;

        public RadioButton() {
            text = "";
            textSize = new Size();
            controlSize = new Size(24, 24);
            size = textSize + controlSize;

            buttonStates = new Animation(new int[2] { 2, 1 });
        }

        public override void initialize() {
            buttonStates.loadSheet(GUIResources.sheets["selection001"], new Rectangle(2, 52, 98, 48));
        }

        protected override void subUpdate(Point menuLocation) {
            Rectangle buttonRect = new Rectangle(location.X + menuLocation.X, location.Y + menuLocation.Y, controlSize.Width, controlSize.Height);
            textSize = new Size((int)Game1.font.MeasureString(text).X, (int)Game1.font.MeasureString(text).Y);
            size = new Size(textSize.Width + controlSize.Width, controlSize.Height);

            if (InputHandler.leftClickRelease()
                && buttonRect.Contains(InputHandler.initialClick)
                && buttonRect.Contains(InputHandler.releaseClick)) {

                isSelected = true;
                buttonStates.frame = 1;
                if (groupIndex != -1) {
                    ControlGroups.groups[groupIndex].changeSelected(this);
                }
                eventTrigger(onChange);
            }
        }

        public override void deselect() {
            if (!isSelected) return;

            isSelected = false;
            buttonStates.frame = 0;
            eventTrigger(onChange);
        }

        public override void draw(Point menuLocation) {
            Vector2 drawLoc = new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y);
            GUIRoot.spriteBatch.Draw(buttonStates.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2((float)controlSize.Width / (float)buttonStates.frameWidth, (float)controlSize.Height / (float)buttonStates.frameHeight), SpriteEffects.None, 0);
            GUIRoot.spriteBatch.DrawString(Game1.font, text,
                new Vector2(controlSize.Width + 2, controlSize.Height / 2 - textSize.Height / 2) + drawLoc,
                Color.Black);
        }
    }
}
