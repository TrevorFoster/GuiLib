using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class TestMenu : Menu {
        private ControlList list;


        public TestMenu(string name, Vector2 location, int width, int height)
            : base(name, location, width, height) {
        }

        public override void setLayout() {
            Label temp;

            list = new ControlList(Orientation.Vertical);
            temp = new Label { text = "Unchecked" };
            temp.mouseOver += changeState;
            temp.mouseOff += changeState;
            list.addControl(temp);
            temp = new Label { text = "Unchecked" };
            temp.mouseOver += changeState;
            temp.mouseOff += changeState;
            list.addControl(temp);
            temp = new Label { text = "Unchecked" };
            temp.mouseOver += changeState;
            temp.mouseOff += changeState;
            list.addControl(temp);
            temp = new Label { text = "Unchecked" };
            temp.mouseOver += changeState;
            temp.mouseOff += changeState;
            list.addControl(temp);
            controls.Add(list);
        }
        private void changeState(object sender, EventArgs e) {
            ((Label)sender).text = ((Label)sender).hovering ? "Checked" : "Unchecked";
        }

        private void checkChange(object sender, EventArgs e) {
            ((CheckBox)sender).text = ((CheckBox)sender).isSelected ? "Checked" : "Unchecked";
        }
    }
}
