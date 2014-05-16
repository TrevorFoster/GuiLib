using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    class GUIResources {
        public static Dictionary<string, Texture2D> sheets = new Dictionary<string, Texture2D>();
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void loadContent() {
            sheets["selection001"] = GUIRoot.content.Load<Texture2D>("selection002");
        }

        public static void loadFont(string font) {
            SpriteFont loaded;

            try {
                loaded = GUIRoot.content.Load<SpriteFont>(font);
            } catch {
                throw new Exception("Cannot load font: " + font);
            }
            fonts[font] = loaded;
        }
    }
}
