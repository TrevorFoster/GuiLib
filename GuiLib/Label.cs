using Microsoft.Xna.Framework;

namespace GuiLib {
    class Label : Control {

        protected override void subUpdate(Point menuLocation) {
            Vector2 textSize = GUIResources.fonts["font"].MeasureString(text);
            size = new Size((int)textSize.X, (int)textSize.Y);
        }

        public override void draw(Point menuLocation) {
            GUIRoot.spriteBatch.DrawString(GUIResources.fonts["font"], text, new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y), Color.Black);
        }
    }
}
