using System;
using Microsoft.Xna.Framework;

namespace GameGUI {

    class Control {
        public Point location { 
            get { 
                return Location; 
            }
            set { 
                Location = value;
                eventTrigger(moved); 
            } 
        }
        protected Point Location;
        public Size size;
        protected Size textSize;
        protected Size controlSize;
        public string text;
        
        public int groupIndex = -1;

        private bool mouseOnLast;

        // Mutual event triggers
        public event EventHandler mouseOver = null;
        public event EventHandler mouseOff = null;

        protected event EventHandler moved = null;

        public virtual void initialize() { }
        protected virtual void subUpdate(Point menuLocation) { }
        public virtual void draw(Point menuLocation) { }
        public virtual void deselect() { }

        public Control() {
            size = new Size();
            location = new Point();
        }

        public void update(Point menuLocation) {
            if (mouseOver != null) {
                checkMouseOver(menuLocation);
                checkMouseOff(menuLocation);
            }
            subUpdate(menuLocation);
        }

        private void checkMouseOver(Point menuLocation) {
            if (mouseOnLast) return;

            if (new Rectangle(location.X + menuLocation.X, location.Y + menuLocation.Y, size.Width, size.Height).Contains(InputHandler.mouseRect)) {
                eventTrigger(mouseOver);
                mouseOnLast = true;
            }
        }

        private void checkMouseOff(Point menuLocation) {
            if (!mouseOnLast) return;

            if (!(new Rectangle(location.X + menuLocation.X, location.Y + menuLocation.Y, size.Width, size.Height).Contains(InputHandler.mouseRect))) {
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
