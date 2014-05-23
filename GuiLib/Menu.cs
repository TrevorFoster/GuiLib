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
        private bool hidden = false;
        public bool bordered = false;
        public bool initialized;

        public Menu() : this("", Vector2.Zero, 10, 10) {

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

        public void intialize() {
            setLayout();
            foreach (Control item in controls) {
                item.initialize();
            }
            initialized = true;
        }

        public void update() {
            if (hidden) return;

            foreach (Control item in controls) {
                item.update(location);
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
            foreach (Control item in controls) {
                item.draw(temp);
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
            foreach (Control item in controls) {
                item.draw(location);
            }
        }
    }
}
