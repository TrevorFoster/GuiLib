using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    public enum Font {
        Verdana
    }

    public static class FontManager {
        public static Dictionary<Font, SpriteFont> fonts = new Dictionary<Font,SpriteFont>();

        public static void loadFonts() {
            fonts[Font.Verdana] = GUIRoot.content.Load<SpriteFont>("Verdana_12");
        }
    }
}
