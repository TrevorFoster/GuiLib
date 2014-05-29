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
                eventTrigger(onLocationChanged);
            }
        }

        private string Text;
        public string text {
            get { return Text; }
            set {
                Text = value;
                measureText();
            }
        }

        protected Size realSize;
        public Size size {
            get { return realSize; }
            set {
                if (!(this is Label)) {
                    setSize(value.Width, value.Height);
                    eventTrigger(onSizeChanged);
                }
            }
        }

        protected Size textSize;
        protected Size controlSize;

        public event EventHandler selectedChange = null;

        public bool hovering;

        // Mutual event triggers
        public event EventHandler mouseOver = null;
        public event EventHandler mouseOff = null;
        protected event EventHandler onLocationChanged = null;
        protected event EventHandler onSizeChanged = null;

        protected bool initialized = false;

        public virtual void initialize() { initialized = true; }
        protected virtual void subUpdate(Vector2 menuLocation) { }
        public virtual void draw(Vector2 menuLocation) { }
        public virtual void deselect() { }

        //field changes
        protected virtual void sizeChanged(object sender, EventArgs e) { }
        protected virtual void locationChanged(object sender, EventArgs e) { }

        // Base constructor
        public Control()
            : base() {
            text = "";
            location = new Vector2();
            realSize = new Size();
            controlSize = new Size();
            textSize = new Size();

            onSizeChanged += sizeChanged;
            onLocationChanged += locationChanged;
        }

        public Control(string text, Vector2 location, Size size) {
            this.text = text;
            this.location = new Vector2(location.X, location.Y);
            realSize = size;
            controlSize = new Size();
            textSize = new Size();
            measureText();
            setSize(realSize.Width, realSize.Height);

            onSizeChanged += sizeChanged;
            onLocationChanged += locationChanged;
        }

        private void measureText() {
            if (Text != null && FontManager.fonts[Font.Verdana] != null) {
                Vector2 measure = FontManager.fonts[Font.Verdana].MeasureString(Text);
                textSize = new Size((int)measure.X, (int)measure.Y);
                eventTrigger(onSizeChanged);
            }
        }

        protected virtual void setSize(int Width, int Height) { }

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
            if (hovering) return;

            if (new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), realSize.Width, realSize.Height).Contains(InputHandler.mouseRect)) {
                eventTrigger(mouseOver);
                hovering = true;
            }
        }

        private void checkMouseOff(Vector2 menuLocation) {
            if (!hovering) return;

            if (!(new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), realSize.Width, realSize.Height).Contains(InputHandler.mouseRect))) {
                eventTrigger(mouseOff);
                hovering = false;
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
