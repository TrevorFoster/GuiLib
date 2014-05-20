using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GuiLib {
    public enum Orientation {
        None,
        Horizontal,
        Vertical
    }

    class TabbedList : Control {
        private Orientation orientation = Orientation.Horizontal;
        private List<Tab> tabs;
        private ControlGroup tabGroup;

        public TabbedList(Orientation orientation)
            : this() {
            this.orientation = orientation;
        }

        public TabbedList() {
            tabs = new List<Tab>();
            tabGroup = new ControlGroup();
            moved += listMoved;
        }

        public override void initialize() {
            foreach (Tab tab in tabs) {
                tab.initialize();
            }
        }

        public void addTab(Tab newTab) {
            tabGroup.addControl(newTab);
            tabs.Add(newTab);
            changeOrientation();
        }

        protected override void subUpdate(Vector2 menuLocation) {
            foreach (Tab tab in tabs) {
                tab.update(menuLocation);
            }
        }

        public override void draw(Vector2 menuLocation) {
            int selectedInd = 0;
            for (int i = tabs.Count - 1; i >= 0; i--) {
                if (!tabs[i].isSelected) tabs[i].draw(menuLocation);
                else selectedInd = i;
            }
            tabs[selectedInd].draw(menuLocation);
        }

        public void changeOrientation(Orientation newOri = Orientation.None) {
            if (newOri != Orientation.None) {
                orientation = newOri;
            }
            int maxHeight = 0;
            int maxWidth = 0;

            foreach (Tab tab in tabs) {
                switch (orientation) {
                    case Orientation.Horizontal:
                        if (tab.size.Height > maxHeight) 
                            maxHeight = tab.size.Height;
                        break;
                    case Orientation.Vertical:
                        if (tab.size.Width > maxWidth) 
                            maxWidth = tab.size.Width;
                        break;
                }
            }

            Vector2 curLoc = new Vector2(location.X, location.Y);
            for (int i = 0; i < tabs.Count; i++) {
                int offX = (maxWidth == 0) ? 0 : maxWidth - tabs[i].size.Width;
                int offY = (maxHeight == 0) ? 0 : maxHeight - tabs[i].size.Height;
                tabs[i].location = new Vector2(curLoc.X + offX, curLoc.Y + offY);
                tabs[i].orientation = orientation;
                tabs[i].moveContents(location + new Vector2(offX, offY));

                switch (orientation) {
                    case Orientation.Horizontal:
                        curLoc.X += tabs[i].size.Width - 10;
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
