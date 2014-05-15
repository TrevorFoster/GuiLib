using System.Collections.Generic;

namespace GuiLib {
    static class ControlGroups {
        public static List<ControlGroup> groups = new List<ControlGroup>();

        public static void addGroup(ControlGroup newGroup) {
            if (newGroup == null) return;

            newGroup.selfIndex = groups.Count;
            groups.Add(newGroup);
        }
    }
}
