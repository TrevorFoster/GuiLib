﻿using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class TestMenu : Menu {
        private Button button1;
        private CheckBox check1;
        private CheckBox check2;
        private RadioButton rbutton;
        private RadioButton rbutton2;
        private ControlList tabList;
        private DropDown list;


        public TestMenu(string name, Vector2 location, int width, int height)
            : base(name, location, width, height) {
        }

        public override void setLayout() {
            Tab tab;
            Menu test = new Menu("tab 1", Vector2.Zero, 360, 200);

            button1 = new Button();
            button1.onClick += button1_clicked;

            button1.location = new Vector2(10, 10);
            button1.size = new Size(80, 40);

            button1.text = "Testing";
            test.controls.Add(button1);

            ControlList buttonList = new ControlList(Orientation.Horizontal);

            buttonList.location = new Vector2(300, 200);
            
            buttonList.addControl(new Button("Test", Vector2.Zero, new Size(50, 30)));
            buttonList.addControl(new Button());
            buttonList.addControl(new Button());
            buttonList.addControl(new Button());

            controls.Add(buttonList);

            tabList = new ControlList(Orientation.Horizontal);

            tab = new Tab();
            tab.size = new Size(120, 30);
            tab.text = "Tab1";
            tab.setMenu(new Menu("tab 1", Vector2.Zero, 360, 200));
            tabList.addControl(tab);

            tab = new Tab();
            tab.size = new Size(120, 30);
            tab.text = "Tab2";
            tab.setMenu(test);
            tabList.addControl(tab);

            tab = new Tab();
            tab.size = new Size(120, 30);
            tab.text = "Tab3";
            tabList.addControl(tab);

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

            button1 = new Button { location = new Vector2(200, 0), text = "Testing" };
            button1.resize(90, 40);
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
            rbutton2.location = new Vector2(200, 200);
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
