using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class TestMenu : Menu {
        private Button button1;
        private CheckBox check1;
        private CheckBox check2;
        private RadioButton rbutton;
        private RadioButton rbutton2;
        private TabbedList tabList;
        private DropDown list;


        public TestMenu(string name, Vector2 location, int[] size, bool hidden)
            : base(name, location, size, hidden) {
        }

        public override void setLayout() {
            Tab tab;
            
            Menu test = new Menu("tab 1", Vector2.Zero, new int[] { 360, 200 }, false);

            button1 = new Button();
            button1.onClick += button1_clicked;

            button1.location = new Vector2(10, 10);
            button1.controlSize.Width = 80;
            button1.controlSize.Height = 40;

            button1.text = "Testing";
            test.controls.Add(button1);

            tabList = new TabbedList(Orientation.Horizontal);

            tab = new Tab { size = { Width = 120, Height = 28 } };
            tab.text = "Tab1";
            tab.setMenu(new Menu("tab 1", Vector2.Zero, new int[] { 360, 200 }, false));
            tabList.addTab(tab);

            tab = new Tab { size = { Width = 120, Height = 28 } };
            tab.text = "Tab2";
            tab.setMenu(test);
            tabList.addTab(tab);

            tab = new Tab { size = { Width = 120, Height = 28 } };
            tab.text = "Tab3";
            tabList.addTab(tab);

            tabList.location = new Vector2(100, 300);
            controls.Add(tabList);

            list = new DropDown { location = new Vector2(400, 30) };
            list.items.AddRange(new string[]{
                "Test 1",
                "Test 2",
                "Test 3"
            });
            controls.Add(list);

            TextBox textBox = new TextBox { location = new Vector2(450, 100) };
            //controls.Add(textBox);

            button1 = new Button { location = new Vector2(200, 0), controlSize = { Width = 100, Height = 60 }, text = "Testing" };
            button1.onClick += button1_clicked;

            controls.Add(button1);

            check1 = new CheckBox { location = new Vector2(50, 50), text = "Check me!" };
            check1.onChange += checkBox1_changed;

            check2 = new CheckBox { location = new Vector2(50, 100), text = "Check me too!" };
            check2.onChange += checkBox1_changed;

            controls.Add(check1);
            controls.Add(check2);
            ControlGroup radioGroup1 = new ControlGroup();
            ControlGroup radioGroup2 = new ControlGroup();

            rbutton = new RadioButton { location = new Vector2(50, 150), text = "1st group" };
            radioGroup1.addControl(rbutton);

            rbutton2 = new RadioButton { location = new Vector2(50, 200), text = "1st group" };
            radioGroup1.addControl(rbutton2);
            controls.Add(rbutton);
            controls.Add(rbutton2);

            Label label = new Label { location = new Vector2(400, 250), text = "Testing the label" };

            ToolTip tip = new ToolTip();
            tip.setToolTip(label, "I am a tool tip");

            rbutton = new RadioButton { location = new Vector2(200, 150), text = "2nd group" };
            radioGroup2.addControl(rbutton);

            rbutton2 = new RadioButton { location = new Vector2(200, 200), text = "2nd group" };
            radioGroup2.addControl(rbutton2);

            controls.Add(tip);
            controls.Add(rbutton);
            controls.Add(rbutton2);
            controls.Add(label);
        }

        private void button1_clicked(object sender, EventArgs e) {
            Console.WriteLine("I have been clicked");
        }

        private void checkBox1_changed(object sender, EventArgs e) {
            Console.WriteLine(check1.isSelected);
        }
    }
}
