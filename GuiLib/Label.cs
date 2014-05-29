using Microsoft.Xna.Framework;
using System;

namespace GuiLib {
    class Label : Control {

        public Label() {
            realSize = new Size(textSize.Width, textSize.Height);
        }

        protected override void sizeChanged(object sender, EventArgs e) {
            realSize = new Size(textSize.Width, textSize.Height);
        }

        public override void draw(Vector2 menuLocation) {
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text, new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y), Color.Black);
        }
    }
}
