using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    class GUIResources {
        public static Dictionary<string, Texture2D> sheets = new Dictionary<string, Texture2D>();

        public static void loadContent() {
            sheets["selection001"] = GUIRoot.content.Load<Texture2D>("selection002");
        }
    }
}
