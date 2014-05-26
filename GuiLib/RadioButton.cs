using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {

    class RadioButton : Control {
        public event EventHandler onChange;
        private Animation buttonStates;

        public bool isSelected = false;

        public RadioButton() {
            resized += resize;
            text = "";
            textSize = new Size();
            controlSize = new Size(24, 24);

            buttonStates = new Animation(2, 1);
        }

        public override void initialize() {
            buttonStates.loadSheet(GUIResources.sheets[Sheet.MainSheet], new Rectangle(2, 52, 98, 48));
        }

        protected override void subUpdate(Vector2 menuLocation) {
            Rectangle buttonRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), controlSize.Width, controlSize.Height);

            if (InputHandler.leftClickRelease() && buttonRect.Contains(InputHandler.initialClick)) {
                isSelected = true;
                buttonStates.frame = 1;
                selectedHasChanged();
                eventTrigger(onChange);
            }
        }

        private void resize(object sender, EventArgs e) {
            size = new Size(controlSize.Width + textSize.Width, (int)Math.Max(controlSize.Height, textSize.Height));
        }

        public override void deselect() {
            if (!isSelected) return;

            isSelected = false;
            buttonStates.frame = 0;
            eventTrigger(onChange);
        }

        public override void draw(Vector2 menuLocation) {
            Vector2 drawLoc = location + menuLocation;
            GUIRoot.spriteBatch.Draw(buttonStates.currentFrame(), drawLoc, null, Color.White, 0f, Vector2.Zero,
                new Vector2((float)controlSize.Width / (float)buttonStates.frameWidth, (float)controlSize.Height / (float)buttonStates.frameHeight), SpriteEffects.None, 0);
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text,
                new Vector2(controlSize.Width + 2, controlSize.Height / 2 - textSize.Height / 2) + drawLoc,
                Color.Black);
        }
    }
}
