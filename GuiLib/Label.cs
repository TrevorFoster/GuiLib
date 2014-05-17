﻿using Microsoft.Xna.Framework;

namespace GuiLib {
    class Label : Control {

        protected override void subUpdate(Vector2 menuLocation) {
            Vector2 textSize = Game1.font.MeasureString(text);
            size = new Size((int)textSize.X, (int)textSize.Y);
        }

        public override void draw(Vector2 menuLocation) {
            GUIRoot.spriteBatch.DrawString(Game1.font, text, new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y), Color.Black);
        }
    }
}
