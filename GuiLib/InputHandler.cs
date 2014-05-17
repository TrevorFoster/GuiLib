using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace GuiLib {
    static class InputHandler {
        public static Rectangle initialClick = new Rectangle(-1, -1, 1, 1);
        public static Rectangle releaseClick = new Rectangle(-1, -1, 1, 1);
        public static Rectangle mouseRect = new Rectangle(-1, -1, 1, 1);
        public static Vector2 mousePos = Vector2.Zero;
        public static MouseState mouseState;
        public static MouseState lastMouseState;
        public static Keys[] pressedKeys;
        public static Keys[] previousKeys;

        public static void update() {
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            mouseRect.X = mouseState.X;
            mouseRect.Y = mouseState.Y;

            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;

            if (leftPressed()) {
                initialClick.X = mouseState.X;
                initialClick.Y = mouseState.Y;
            } else if (leftClickRelease()) {
                releaseClick.X = mouseState.X;
                releaseClick.Y = mouseState.Y;
            }
            previousKeys = pressedKeys;
            pressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        public static bool keyTyped(Keys key) {
            if (pressedKeys == null || previousKeys == null) return false;
            return !pressedKeys.Contains(key) && previousKeys.Contains(key);
        }

        public static bool keyPressed(Keys key) {
            if (pressedKeys == null) return false;
            return pressedKeys.Contains(key);
        }

        public static bool leftPressed() {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool leftClickRelease(){
            return mouseState.LeftButton == ButtonState.Released &&
                lastMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
