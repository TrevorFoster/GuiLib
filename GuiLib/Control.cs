using System;
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace GuiLib {

    class Control {
        private Vector2 Location;
        public Vector2 location {
            get { return Location; }
            set {
                Location = value;
                eventTrigger(moved);
            }
        }

        private string Text;
        public string text {
            get { return Text; }
            set {
                Text = value;
                if (Text != null && FontManager.fonts[Font.Verdana] != null) {
                    Vector2 measure = FontManager.fonts[Font.Verdana].MeasureString(Text);
                    textSize = new Size((int)measure.X, (int)measure.Y);
                }
            }
        }

        private Size Size;
        public Size size {
            get { return Size; }
            protected set {
                Size = value;
            }
        }
        protected Size textSize;
        protected Size controlSize;

        public event EventHandler selectedChange = null;

        private bool mouseOnLast;

        // Mutual event triggers
        public event EventHandler mouseOver = null;
        public event EventHandler mouseOff = null;

        protected event EventHandler moved = null;
        public event EventHandler resized = null;

        public virtual void initialize() { }
        protected virtual void subUpdate(Vector2 menuLocation) { }
        public virtual void draw(Vector2 menuLocation) { }
        public virtual void deselect() { }

        public Control() {
            text = "";
            size = new Size();
            controlSize = new Size();
            textSize = new Size();
            location = new Vector2();
        }

        public void resize(int Width, int Height) {
            setSize(Width, Height);
            eventTrigger(resized);
        }

        protected virtual void setSize(int Width, int Height) {
            size = new Size(Width, Height);
        }

        public void update(Vector2 menuLocation) {
            if (mouseOver != null) {
                checkMouseOver(menuLocation);
                checkMouseOff(menuLocation);
            }
            subUpdate(menuLocation);
        }

        protected void selectedHasChanged() {
            eventTrigger(selectedChange);
        }

        private void checkMouseOver(Vector2 menuLocation) {
            if (mouseOnLast) return;

            if (new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height).Contains(InputHandler.mouseRect)) {
                eventTrigger(mouseOver);
                mouseOnLast = true;
            }
        }

        private void checkMouseOff(Vector2 menuLocation) {
            if (!mouseOnLast) return;

            if (!(new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height).Contains(InputHandler.mouseRect))) {
                eventTrigger(mouseOff);
                mouseOnLast = false;
            }
        }

        protected void eventTrigger(EventHandler handler) {
            // make sure a callback function has been assigned
            if (handler == null) return;

            // call the callback function
            handler(this, EventArgs.Empty);
        }
    }
}
