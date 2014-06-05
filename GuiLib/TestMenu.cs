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
                //backgroundColour = Color.Transparent;
        }

        public override void setLayout() {
            Table table = new Table();
            table.size = new Size(600, 400);

            for (int i = 0; i < 800 / 10; i++) {
                for (int j = 0; j < 600 / 10; j++) {
                    table.setColumnData(j, i, new RadioButton());
                }
            }

            this.addToBack(table);
        }
    }
}
