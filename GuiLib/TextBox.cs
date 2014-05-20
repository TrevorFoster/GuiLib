using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GuiLib {
    class TextBox : Control {
        private Animation boxStates;
        private int cursor;
        public bool isSelected;

        public TextBox() {
            text = "";
            cursor = 0;
            size = new Size(150, 30);
        }

        protected override void subUpdate(Vector2 menuLocation) {
            Rectangle boxRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), size.Width, size.Height);

            if (InputHandler.leftClickRelease()) {
                if (boxRect.Contains(InputHandler.mouseRect)) {
                    isSelected = true;
                } else {
                    isSelected = false;
                }
            }

            if (isSelected) {
                if (InputHandler.keyTyped(Keys.Left)) {
                    if (cursor > 0) {
                        cursor--;
                    }
                } else if (InputHandler.keyTyped(Keys.Right)) {
                    if (cursor < text.Length) {
                        cursor++;
                    }
                } else {
                    foreach (Keys key in InputHandler.previousKeys) {
                        if (InputHandler.keyTyped(key)) {
                            if ((int)key >= 32 && (int)key <= 128) {
                                char character = (char)(int)key;
                                if (text == "" || cursor == text.Length) {
                                    text += character;
                                } else {
                                    text = text.slice(0, cursor) + character + text.slice(cursor, text.Length);
                                }
                                cursor++;
                            }
                        }
                    }
                }
            }
        }

        public override void draw(Vector2 menuLocation) {

            Shapes.DrawRectangle(size.Width, size.Height, new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y), Color.White, 0);
            if (text != null) {
                GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text, new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y), Color.Black);
            }
        }
    }

    static class General {
        public static string slice(this string source, int start, int end) {
            if (end < 0) {
                end = source.Length + end;
            }
            int len = end - start;
            return source.Substring(start, len);
        }
    }
}
