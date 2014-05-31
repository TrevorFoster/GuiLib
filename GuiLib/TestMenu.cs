using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class TestMenu : Menu {
        private ControlList list;
        private Label label;
        private DropDown dropdown;
        private int count = 0;


        public TestMenu(string name, Vector2 location, int width, int height)
            : base(name, location, width, height) {
        }

        public override void setLayout() {
            list = new ControlList(Orientation.Vertical);
            list.location = new Vector2(100, 100);
            list.addControl(new Label { text = "Please Select a Country", size = new Size(160, 200) });
            dropdown = new DropDown { items = new List<string> { "Canada", "US", "UK", "Russia" }, text = "Country"};
            list.addControl(dropdown);
            Button submit = new Button { text = "Ok" };
            submit.onClick += buttonClick;
            list.addControl(submit);
            label = new Label { text = "Clicks: " + count };
            list.addControl(label);

            this.addToBack(list);
        }
        private void buttonClick(object sender, EventArgs e) {
            Random r = new Random();
            count++;
            label.text = "Clicks: " + count;
            this.addToBack(new Button { location = new Vector2(r.Next(500), r.Next(500)) });
        }

        private void checkChange(object sender, EventArgs e) {
            ((CheckBox)sender).text = ((CheckBox)sender).isSelected ? "Checked" : "Unchecked";
        }
    }
}
