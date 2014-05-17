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
        
        public Size size;
        public Size textSize;

        private Size ControlSize;
        public Size controlSize {
            get { return ControlSize; }
            set {
                ControlSize = value;
                eventTrigger(resized);
            }
        }

        private string Text;
        public string text {
            get { return Text; }
            set { 
                Text = value;
                if (Text != null && Game1.font != null) {
                    textSize = new Size((int)Game1.font.MeasureString(Text).X, (int)Game1.font.MeasureString(Text).Y);
                }
            }
        }
        
        public int groupIndex = -1;

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
            controlSize.sizeChanged += resizeDispatcher;
        }

        private void resizeDispatcher(object sender, PropertyChangedEventArgs e) {
            eventTrigger(resized);
        }

        public void update(Vector2 menuLocation) {
            if (mouseOver != null) {
                checkMouseOver(menuLocation);
                checkMouseOff(menuLocation);
            }
            subUpdate(menuLocation);
        }

        private void checkMouseOver(Vector2 menuLocation) {
            if (mouseOnLast) return;

            if (new Rectangle((int)(location.X + menuLocation.X),(int)(location.Y + menuLocation.Y), size.Width, size.Height).Contains(InputHandler.mouseRect)) {
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

        protected virtual void eventTrigger(EventHandler handler) {
            // make sure a callback function has been assigned
            if (handler == null) return;

            // call the callback function
            handler(this, EventArgs.Empty);
        }
    }
}
