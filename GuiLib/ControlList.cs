using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GuiLib {
    public enum Orientation {
        None,
        Horizontal,
        Vertical
    }

    class ControlList : Control {
        private List<Control> controls;
        private ControlGroup tabGroup;

        public ControlList(Orientation orientation)
            : this() {
            this.orientation = orientation;
        }

        public ControlList() {
            controls = new List<Control>();
            tabGroup = new ControlGroup();
            moved += listMoved;
        }

        public override void initialize() {
            foreach (Tab tab in controls) {
                tab.initialize();
            }
        }

        public void addTab(Tab newTab) {
            tabGroup.addControl(newTab);
            controls.Add(newTab);
            changeOrientation();
        }

        protected override void subUpdate(Vector2 menuLocation) {
            foreach (Tab tab in controls) {
                tab.update(menuLocation);
            }
        }

        public override void draw(Vector2 menuLocation) {
            int selectedInd = 0;
            for (int i = controls.Count - 1; i >= 0; i--) {
                if (!controls[i].isSelected) 
                    controls[i].draw(menuLocation);
                else
                    selectedInd = i;
            }
            controls[selectedInd].draw(menuLocation);
        }

        public void changeOrientation(Orientation newOri = Orientation.None) {
            if (newOri != Orientation.None) {
                orientation = newOri;
            }
            int maxHeight = 0;
            int maxWidth = 0;

            foreach (Tab tab in controls) {
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
            for (int i = 0; i < controls.Count; i++) {
                int offX = (maxWidth == 0) ? 0 : maxWidth - controls[i].size.Width;
                int offY = (maxHeight == 0) ? 0 : maxHeight - controls[i].size.Height;
                controls[i].location = new Vector2(curLoc.X + offX, curLoc.Y + offY);
                controls[i].orientation = orientation;
                if(controls[i] is Tab)
                    ((Tab)controls[i]).moveContents(location + new Vector2(offX, offY));

                switch (orientation) {
                    case Orientation.Horizontal:
                        curLoc.X += controls[i].size.Width - 10;
                        break;

                    case Orientation.Vertical:
                        curLoc.Y += controls[i].size.Height;
                        break;
                }
            }
        }

        private void listMoved(object sender, EventArgs e) {
            changeOrientation();
        }
    }
}
