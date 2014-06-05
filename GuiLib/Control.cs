using System;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;

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

        protected Size textSize, controlSize, realSize;
        public Size size {
            get { return realSize; }
            set {
                if (!(this is Label)) {
                    setSize(value.Width, value.Height);
                    eventTrigger(onSizeChanged);
                }
            }
        }

        public bool hovering, initialized, hoverable = true;
        public bool isSelected;

        public Color overlayColour = new Color(100, 100, 100, 100);

        // Mutual event triggers
        public event EventHandler mouseOver, mouseOff;
        protected event EventHandler onLocationChanged, onSizeChanged;

        // For control groups
        public event EventHandler selectedChange;

        private Renderer renderer;

        // Field change events
        

       
        protected virtual void setSize(int Width, int Height) { }
        public virtual void deselect() { }

        // Base constructor
        public Control()
            : base() {
            renderer = new Renderer();
            text = "";
            location = new Vector2();
            realSize = new Size();
            controlSize = new Size();
            textSize = new Size();

            onSizeChanged += sizeChanged;
            onLocationChanged += locationChanged;
            renderer.setStrategy((Renderer.RenderStrategy)render);
        }

        public Control(string text, Vector2 location, Size size) {
            renderer = new Renderer();
            this.text = text;
            this.location = new Vector2(location.X, location.Y);
            realSize = size;
            controlSize = new Size();
            textSize = new Size();
            measureText();
            setSize(realSize.Width, realSize.Height);

            onSizeChanged += sizeChanged;
            onLocationChanged += locationChanged;
            renderer.setStrategy((Renderer.RenderStrategy)render);
        } 
        
        public virtual void initialize() {
            initialized = true;
            renderer.initialize();
            renderer.render(realSize);
        }

        private void measureText() {
            if (Text != null && FontManager.fonts[Font.Verdana] != null) {
                Vector2 measure = FontManager.fonts[Font.Verdana].MeasureString(Text);
                textSize = new Size((int)measure.X, (int)measure.Y);
                eventTrigger(onSizeChanged);
            }
        }
        protected virtual void locationChanged(object sender, EventArgs e) {
        }

        protected virtual void sizeChanged(object sender, EventArgs e) {
            renderer.render(realSize);
            
        }

        public virtual void update(Vector2 offset) {
            if (!hoverable) return;

            if (!hovering) {
                checkMouseOver(offset);
            } else {
                checkMouseOff(offset);
            }
        }

        public virtual void render() {

        }

        public virtual void draw(Vector2 menuLocation) {
            GUIRoot.spriteBatch.Draw(renderer.image, location + menuLocation, Color.White);
            if (hovering) {
                Shapes.DrawRectangle(controlSize.Width, controlSize.Height, location + menuLocation, overlayColour, 0);
            }
        }

        public virtual void select() {
            isSelected = true;
            eventTrigger(selectedChange);
        }

        private void checkMouseOver(Vector2 offset) {
            if (new Rectangle((int)(location.X + offset.X), (int)(location.Y + offset.Y), realSize.Width, realSize.Height).Contains(InputHandler.mouseRect)) {
                eventTrigger(mouseOver);
                hovering = true;
            }
        }

        private void checkMouseOff(Vector2 offset) {
            if (!(new Rectangle((int)(location.X + offset.X), (int)(location.Y + offset.Y), realSize.Width, realSize.Height).Contains(InputHandler.mouseRect))) {
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
