using System;
using System.Collections.Generic;

namespace GuiLib {
    class ControlGroup {
        private Control selected = null;

        public void addControl(Control toAdd) {
            if (toAdd == null) return;

            toAdd.selectedChange += changeSelected;
        }

        private void changeSelected(object newSelected, EventArgs e) {

            if (newSelected == null || !(newSelected is Control)) return;

            Control control = (Control)newSelected;
            if (selected == null) {
                selected = control;
            } else if (!selected.Equals(control)) {
                selected.deselect();
                selected = control;
            }
        }
    }
}
