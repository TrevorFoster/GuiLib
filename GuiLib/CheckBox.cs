using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {
    class CheckBox : Control {
        public event EventHandler onChange;
        private Animation boxStates;

        public CheckBox() {

            controlSize = new Size(24, 24);
            boxStates = new Animation(2, 1, Sheet.MainSheet);
        }

        public override void initialize() {
            boxStates.loadSheet(new Rectangle(2, 2, 98, 48));
            realSize = new Size(controlSize.Width + textSize.Width, controlSize.Height);
            boxStates.updateScale(new Vector2(controlSize.Width, controlSize.Height));

            base.initialize();
        }

        public override void update(Vector2 offset) {
            Rectangle buttonRect = new Rectangle((int)(location.X + offset.X), (int)(location.Y + offset.Y), realSize.Width, realSize.Height);

            if (InputHandler.leftClickRelease() && hovering) {
                isSelected = !isSelected;
                boxStates.frame = isSelected ? 1 : 0;
                renderer.render(realSize);
                eventTrigger(onChange);
            }
            base.update(offset);
        }

        protected override void setSize(int Width, int Height) {
            realSize = new Size(Width + textSize.Width, (int)Math.Max(Height, textSize.Height));
            controlSize = new Size(realSize.Width, realSize.Height);
            boxStates.updateScale(new Vector2(controlSize.Width, controlSize.Height));
        }

        public override void deselect() {
            // if already deselected return
            if (!isSelected) return;

            // change check box state to unchecked
            isSelected = false;
            boxStates.frame = 0;
            eventTrigger(onChange);
        }

        public override void render() {
            // draw the check box
            boxStates.draw();
            // draw the text for the check box
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text,
                new Vector2(controlSize.Width + 2, controlSize.Height / 2 - textSize.Height / 2), Color.Black);
        }
    }
}
