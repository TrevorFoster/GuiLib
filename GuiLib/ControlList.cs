using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GuiLib {
    public enum Orientation {
        None,
        Horizontal,
        Vertical
    }

    public enum Justification {
        Left,
        Right
    }

    class ControlList : Control {
        private List<Control> controls;
        private ControlGroup controlGroup;
        private Orientation orientation = Orientation.Horizontal;
        private Justification justification = Justification.Left;

        public ControlList(Orientation orientation)
            : this() {
            this.orientation = orientation;
            hoverable = false;
        }

        public ControlList() {
            controls = new List<Control>();
            controlGroup = new ControlGroup();
        }

        public override void initialize() {
            foreach (Control control in controls) {
                control.initialize();
            }
        }

        public void addControl(Control newcontrol) {
            if (!newcontrol.initialized && this.initialized) {
                newcontrol.initialize();
            }
            controlGroup.addControl(newcontrol);
            controls.Add(newcontrol);
        }

        public override void update(Vector2 menuLocation) {
            for (int i = 0; i < controls.Count; i++) {
                controls[i].update(menuLocation);
            }
        
            base.update(menuLocation);
        }

        public override void draw(Vector2 menuLocation) {
            
            int maxHeight = 0;
            int maxWidth = 0;

            foreach (Control control in controls) {
                switch (orientation) {
                    case Orientation.Horizontal:
                        if (control.size.Height > maxHeight)
                            maxHeight = control.size.Height;
                        break;
                    case Orientation.Vertical:
                        if (control.size.Width > maxWidth)
                            maxWidth = control.size.Width;
                        break;
                }
            }

            Vector2 curLoc = new Vector2(location.X, location.Y);
            for (int i = 0; i < controls.Count; i++) {
                int offX = (maxWidth <= 0) ? 0 : maxWidth - controls[i].size.Width;
                int offY = (maxHeight <= 0) ? 0 : maxHeight - controls[i].size.Height;
                int xOff = 0;
                if (orientation == Orientation.Vertical) {
                    switch (justification) {
                        case Justification.Left:
                            offX = 0;
                            offY = 0;
                            break;
                        
                        case Justification.Right:
                            break;
                    }
                }
                controls[i].location = new Vector2(curLoc.X + offX, curLoc.Y + offY);
                if (controls[i] is Tab) {
                    Tab tab = (Tab)controls[i];
                    tab.orientation = orientation;
                    tab.moveContents(location + new Vector2(offX, offY));
                    xOff = 10;
                }

                switch (orientation) {
                    case Orientation.Horizontal:
                        curLoc.X += controls[i].size.Width - xOff;
                        break;

                    case Orientation.Vertical:
                        curLoc.Y += controls[i].size.Height;
                        break;
                }
            }

            int selectedInd = -1;
            for (int i = controls.Count - 1; i >= 0; i--) {
                if (controls[i] is Tab) {
                    Tab tab = (Tab)controls[i];
                    if (!tab.isSelected)
                        tab.draw(menuLocation);
                    else
                        selectedInd = i;
                } else {
                    controls[i].draw(menuLocation);
                }
            }
            if (selectedInd != -1)
                controls[selectedInd].draw(menuLocation);
        }

        public void changeOrientation(Orientation newOri = Orientation.None) {
            if (newOri != Orientation.None) {
                orientation = newOri;
            }
        }
    }
}
