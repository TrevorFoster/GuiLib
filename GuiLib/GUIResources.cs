using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    public enum Sheet {
        MainSheet
    }
    class GUIResources {
        public static Dictionary<Sheet, Texture2D> sheets = new Dictionary<Sheet, Texture2D>();

        public static void loadContent() {
            sheets[Sheet.MainSheet] = GUIRoot.content.Load<Texture2D>("selection002");
        }
    }
}
