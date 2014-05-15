using System.Collections.Generic;


namespace GuiLib {
    class ControlGroup {
        public int selfIndex;
        private int selected;
        private List<Control> groupControls = new List<Control>();

        public void addControl(Control control) {
            if (control == null)
                return;
            control.groupIndex = selfIndex;
            groupControls.Add(control);
        }

        public void changeSelected(Control newSelected) {
            int lastSelected = selected;
            int i = 0;

            foreach (Control current in groupControls) {
                if (current.Equals(newSelected)) {
                    selected = i;
                    break;
                }
                i++;
            }
            if (lastSelected != -1 && lastSelected != selected) {
                groupControls[lastSelected].deselect();
            }
        }
    }
}
