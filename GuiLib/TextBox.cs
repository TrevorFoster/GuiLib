using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GuiLib {
    class TextBox : Control {
        private Animation boxStates;
        private int cursor;

        public bool isSelected = false;

        public TextBox() {
            cursor = 0;
            realSize = new Size(150, 30);
        }

        public override void update(Vector2 menuLocation) {
            Rectangle boxRect = new Rectangle((int)(location.X + menuLocation.X), (int)(location.Y + menuLocation.Y), realSize.Width, realSize.Height);

            if (InputHandler.leftClickRelease()) {
                isSelected = hovering ? true : false;
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
                    foreach (Keys key in InputHandler.keysLastFrame) {
                        if (InputHandler.keyTyped(key)) {
                            int keyI = (int)key;
                            if (keyI >= 32 && keyI <= 128) {
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
            base.update(menuLocation);
        }

        public override void draw(Vector2 menuLocation) {

            Shapes.DrawRectangle(realSize.Width, realSize.Height, new Vector2(location.X + menuLocation.X, location.Y + menuLocation.Y), Color.White, 0);
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
