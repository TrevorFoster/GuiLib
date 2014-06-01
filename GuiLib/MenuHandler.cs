using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    class MenuHandler {
        private List<Menu> menus;
        private bool initialized;

        public MenuHandler() {
            menus = new List<Menu>();
        }

        public void addMenu(Menu newMenu) {
            if (newMenu == null) return;
            if (this.initialized && !newMenu.initialized) {
                newMenu.intialize();
            }
            menus.Add(newMenu);
        }

        public Menu getMenu(int index) {
            if (index < 0 || index > menus.Count - 1) return null;
            return menus[index];
        }

        public void initialize() {
            foreach (Menu menu in menus) {
                if (!menu.initialized) {
                    menu.intialize();
                }
            }
            initialized = true;
        }

        public void update() {
            foreach (Menu menu in menus) {
                menu.update();
            }
        }

        public void draw() {
            foreach (Menu menu in menus) {
                menu.draw();
            }
        }
    }
}
