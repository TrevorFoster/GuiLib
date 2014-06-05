using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GuiLib {
    class Menu {
        public List<Control> controls = new List<Control>();

        public Vector2 location;
        public Size size;
        public Color backgroundColour;

        public string title;
        public bool initialized, hidden, bordered;
        private int layers;

        public Menu()
            : this("", Vector2.Zero, 10, 10) {

        }

        public Menu(string title, Vector2 location, int width, int height) {
            this.title = title;

            this.location = location;
            this.size = new Size(width, height);

            backgroundColour = new Color(120, 120, 120, 100);
        }

        public void show() {
            hidden = false;
        }

        public void hide() {
            hidden = true;
        }

        public virtual void setLayout() {

        }

        public void addToFront(Control control) {
            if (!control.initialized) {
                control.initialize();
            }
            controls.Insert(0, control);
            layers++;
        }

        public void addToBack(Control control) {
            if (!control.initialized) {
                control.initialize();
            }
            controls.Add(control);
            layers++;
        }

        public void insertToLayer(int layer, Control control) {
            if (!control.initialized) {
                control.initialize();
            }
            if (layer < 0 || layer > layers - 1) {
                layer = layers - 1;
            }
            controls.Insert(layer, control);
            layers++;
        }

        public void intialize() {
            layers = controls.Count;
            setLayout();
            foreach (Control item in controls) {
                if (!item.initialized) {
                    item.initialize();
                }
            }
            initialized = true;
        }

        public void update() {
            if (hidden) return;
            for (int i = 0; i < layers; i++) {
                controls[i].update(location);
            }
        }

        public void update(Vector2 offset) {
            if (hidden) return;

            Vector2 totOffs = location + offset;

            foreach (Control item in controls) {
                item.update(totOffs);
            }
        }

        public void draw(Vector2 offset) {
            if (hidden) return;
            Vector2 drawLoc = offset + location;
            Shapes.DrawRectangle(size.Width, size.Height, drawLoc, backgroundColour, 0);
            if (bordered) {
                Shapes.DrawRectangle(size.Width + 20, 10, drawLoc + new Vector2(-10, -10), Color.White, 0);
                Shapes.DrawRectangle(size.Width + 20, 10, drawLoc + new Vector2(-10, size.Height), Color.White, 0);
                Shapes.DrawRectangle(10, size.Height, drawLoc + new Vector2(-10, 0), Color.White, 0);
                Shapes.DrawRectangle(10, size.Height, drawLoc + new Vector2(size.Width, 0), Color.White, 0);
            }
            Vector2 temp = new Vector2(location.X + offset.X, location.Y + offset.Y);
            for (int i = layers - 1; i >= 0; i--) {
                controls[i].draw(temp);
            }
        }

        public void draw() {
            if (hidden) return;
            Shapes.DrawRectangle(size.Width, size.Height, new Vector2(location.X, location.Y), backgroundColour, 0);
            if (bordered) {
                Shapes.DrawRectangle(size.Width + 20, 10, new Vector2(location.X - 10, location.Y - 10), Color.White, 0);
                Shapes.DrawRectangle(size.Width + 20, 10, new Vector2(location.X - 10, location.Y + size.Height), Color.White, 0);
                Shapes.DrawRectangle(10, size.Height, new Vector2(location.X - 10, location.Y), Color.White, 0);
                Shapes.DrawRectangle(10, size.Height, new Vector2(location.X + size.Width, location.Y), Color.White, 0);
            }
            for (int i = layers - 1; i >= 0; i--) {
                controls[i].draw(location);
            }
        }
    }
}
