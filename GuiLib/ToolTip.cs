using Microsoft.Xna.Framework;
using System;

namespace GuiLib {
    class ToolTip : Control {
        private Vector2 tipLocation;
        private bool drawTip;
        private bool hovering;

        public int hoverTime;
        private int timeHovered;

        public void setToolTip(Control control, string description) {
            text = description;
            control.mouseOver += mousedOver;
            control.mouseOff += mousedOff;
            timeHovered = 0;
            hoverTime = 500;
        }

        protected override void subUpdate(Point menuLocation) {
            if (hovering && !drawTip) {
                if (timeHovered >= hoverTime) {
                    tipLocation = new Vector2(InputHandler.mouseState.X, InputHandler.mouseState.Y + 20);
                    drawTip = true;
                } else {
                    timeHovered += Game1.time.ElapsedGameTime.Milliseconds;
                }
            }
        }

        // Callback function to turn drawing of the tool tip on
        private void mousedOver(object sender, EventArgs e) {
            hovering = true;
        }

        // Callback function to turn the tool tip drawing off
        private void mousedOff(object sender, EventArgs e) {
            drawTip = false;
            hovering = false;
            timeHovered = 0;
        }

        public override void draw(Point menuLocation) {
            // Make sure the tooltip should be drawn
            if (!drawTip) return;

            Shapes.DrawRectangle(GUIResources.fonts["font"].MeasureString(text).X, GUIResources.fonts["font"].MeasureString(text).Y, tipLocation,
                new Color(100, 100, 100, 100), 0);
            GUIRoot.spriteBatch.DrawString(GUIResources.fonts["font"], text, tipLocation, Color.Black);
        }
    }
}
