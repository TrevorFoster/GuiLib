using Microsoft.Xna.Framework;
using System;

namespace GuiLib {
    class ToolTip : Control {
        private Vector2 tipLocation;
        private bool drawTip;

        public int hoverTime;
        private int timeHovered;

        public void setToolTip(Control control, string description) {
            text = description;
            control.mouseOver += mousedOver;
            control.mouseOff += mousedOff;
            timeHovered = 0;
            hoverTime = 500;
        }

        public override void update(Vector2 offset) {
            if (hovering && !drawTip) {
                if (timeHovered >= hoverTime) {
                    tipLocation = new Vector2(InputHandler.mouseState.X, InputHandler.mouseState.Y + 20);
                    drawTip = true;
                } else {
                    timeHovered += Game1.time.ElapsedGameTime.Milliseconds;
                }
            }
            base.update(offset);
        }

        // Callback function to turn drawing of the tool tip on
        private void mousedOver(object sender, EventArgs e) {
            hovering = true;
            drawTip = true;
        }

        // Callback function to turn the tool tip drawing off
        private void mousedOff(object sender, EventArgs e) {
            drawTip = false;
            timeHovered = 0;
        }

        public override void draw(Vector2 offset) {
            // Make sure the tooltip should be drawn
            if (!drawTip) return;

            Shapes.DrawRectangle(FontManager.fonts[Font.Verdana].MeasureString(text).X, FontManager.fonts[Font.Verdana].MeasureString(text).Y, tipLocation,
                new Color(100, 100, 100, 100), 0);
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text, tipLocation, Color.Black);
        }
    }
}
