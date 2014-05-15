using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GuiLib {
    class Menu {
        public List<Control> controls = new List<Control>();

        public Point location;
        public Size size;

        public string title;
        private bool hidden;
        public bool initialized;

        public Menu(string title, Point location, int [] size, bool hidden) {
            this.title = title;

            this.location = location;
            this.size = new Size(size[0], size[1]);

            this.hidden = hidden;
        }

        public void show() {
            hidden = false;
        }

        public void hide() {
            hidden = true;
        }

        public void intialize() {
            foreach (Control item in controls) {
                item.initialize();
            }
            initialized = true;
        }

        public void update() {
            foreach (Control item in controls) {

                item.update(location);
            }
        }

        public void update(Point offset) {
            Point temp = new Point(location.X + offset.X, location.Y + offset.Y);

            foreach (Control item in controls) {
                item.update(temp);
            }
        }

        public void draw(Point offset) {
            if (hidden) return;
            Shapes.DrawRectangle(size.Width, size.Height, new Vector2(location.X + offset.X, location.Y + offset.Y), new Color(120, 120, 120, 100), 0);
            Point temp = new Point(location.X + offset.X, location.Y + offset.Y);
            foreach (Control item in controls) {
                item.draw(temp);
            }
        }

        public void draw() {
            if (hidden) return;
            Shapes.DrawRectangle(size.Width, size.Height, new Vector2(location.X, location.Y), new Color(120, 120, 120, 100), 0);

            foreach (Control item in controls) {
                item.draw(location);
            }
        }
    }
}
