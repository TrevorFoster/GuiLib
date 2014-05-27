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
                    updateSize();
                }
            }
        }
        protected Size realSize;

        public Size size {
            get { return realSize; }
            set {
                resize(value.Width, value.Height);
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
        protected bool initialized = false;

        public virtual void initialize() { initialized = true; }
        protected virtual void subUpdate(Vector2 menuLocation) { }
        public virtual void draw(Vector2 menuLocation) { }
        public virtual void deselect() { }

        // Base constructor
        public Control()
            : base() {
            text = "";
            location = new Vector2();
            realSize = new Size();
            controlSize = new Size();
            textSize = new Size();

            size.fieldChanged += resize;
        }

        public Control(string text, Vector2 location, Size size) {
            this.text = text;
            this.location = new Vector2(location.X, location.Y);
            this.realSize = size.Clone();
        }

        protected void resize(object sender, PropertyChangedEventArgs e) {
            setSize(size.Width, size.Height);
        }

        public void resize(int Width, int Height) {
            setSize(Width, Height);
        }

        protected virtual void setSize(int Width, int Height) {
            realSize = new Size(Width, Height);
        }

        protected virtual void updateSize() {

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

            if (new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), realSize.Width, realSize.Height).Contains(InputHandler.mouseRect)) {
                eventTrigger(mouseOver);
                mouseOnLast = true;
            }
        }

        private void checkMouseOff(Vector2 menuLocation) {
            if (!mouseOnLast) return;

            if (!(new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), realSize.Width, realSize.Height).Contains(InputHandler.mouseRect))) {
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
