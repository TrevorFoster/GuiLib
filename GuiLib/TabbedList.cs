using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GuiLib{
    public enum Orientation {
        None,
        Horizontal,
        Vertical
    }

    class TabbedList : Control {
        private Orientation orientation = Orientation.Horizontal;
        private List<Tab> tabs;
        private int group;

        public TabbedList(Orientation orientation) : this() {
            this.orientation = orientation;
        }

        public TabbedList() {
            tabs = new List<Tab>();
            ControlGroups.addGroup(new ControlGroup());
            group = ControlGroups.groups.Count - 1;
            moved += listMoved;
        }

        public override void initialize() {
            foreach (Tab tab in tabs) {
                tab.initialize();
            }
        }

        public void addTab(Tab newTab) {
            ControlGroups.groups[group].addControl(newTab);
            tabs.Add(newTab);
            changeOrientation();
        }

        protected override void subUpdate(Vector2 menuLocation) {
            foreach (Tab tab in tabs) {
                tab.update(menuLocation);
            }
        }

        public override void draw(Vector2 menuLocation) {
            foreach (Tab tab in tabs) {
                tab.draw(menuLocation);
            }
        }

        public void changeOrientation(Orientation newOri = Orientation.None) {
            if (newOri != Orientation.None) {
                orientation = newOri;
            }

            Vector2 curLoc = new Vector2(location.X, location.Y);
            for (int i = 0; i < tabs.Count; i++) {
                tabs[i].location = new Vector2(curLoc.X, curLoc.Y);
                tabs[i].orientation = orientation;
                tabs[i].moveContents(location);


                switch (orientation) {
                    case Orientation.Horizontal:
                        curLoc.X += tabs[i].size.Width;
                        break;

                    case Orientation.Vertical:
                        curLoc.Y += tabs[i].size.Height;
                        break;
                }
            }
        }

        private void listMoved(object sender, EventArgs e) {
            changeOrientation();
        }
    }
}
