﻿using Microsoft.Xna.Framework;
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
            list = new ControlList(Orientation.Horizontal, 0);
            tab = new Tab();
            tab.text = "Tiles";
            tab.setMenu(new Menu("Tiles", Vector2.Zero, 260, 300));
            list.addControl(tab);

            tab = new Tab();
            tab.text = "Collision Masks";
            tab.size = new Size(150, 30);
            tab.setMenu(new Menu("Collision Masks", Vector2.Zero, 260, 300));
            list.addControl(tab);

            Table table = new Table();
            table.size = new Size(600, 400);

            table.setColumnData(0, 0, new Button { text = "Button1" });
            table.setColumnData(1, 1, new Button { text = "Button2" });
            table.setColumnData(2, 2, new Button { text = "Button3" });
            table.setColumnData(3, 3, new Button { text = "Button4" });
            table.setColumnData(4, 4, new Button { text = "Button5" });


            table.setColumnData(0, 4, new Button { text = "Button1" });
            table.setColumnData(1, 3, new Button { text = "Button2" });
            table.setColumnData(2, 2, new Button { text = "Button3" });
            table.setColumnData(3, 1, new Button { text = "Button4" });
            table.setColumnData(4, 0, new Button { text = "Button5" });


            controls.Add(table);

            

            this.addToBack(list);
        }
    }
}
