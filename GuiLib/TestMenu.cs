using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class TestMenu : Menu {
        private ControlList list;
        private Tab tab;

        public TestMenu(string name, Vector2 location, int width, int height)
            : base(name, location, width, height) {
                backgroundColour = Color.Transparent;
        }

        public override void setLayout() {
            list = new ControlList(Orientation.Horizontal);
            tab = new Tab();
            tab.text = "Menu 1";
            tab.setMenu(new Menu(this.title, this.location, this.size.Width, this.size.Height) { backgroundColour = Color.Blue });
            list.addControl(tab);
            tab = new Tab();
            tab.text = "Menu 2";
            tab.setMenu(new Menu(this.title, this.location, this.size.Width, this.size.Height) { backgroundColour = Color.Red });
            list.addControl(tab);
            tab = new Tab();
            tab.text = "Menu 3";
            tab.setMenu(new Menu(this.title, this.location, this.size.Width, this.size.Height));
            list.addControl(tab);
            //list.addControl(new Button { text = "Test Button", size = new Size(120,40)});

            this.addToBack(list);
        }
    }
}
