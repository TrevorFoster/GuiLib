using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace GuiLib {
    static class InputHandler {
        public static Rectangle initialClick = new Rectangle(-1, -1, 1, 1);
        public static Rectangle releaseClick = new Rectangle(-1, -1, 1, 1);
        public static Rectangle mouseRect = new Rectangle(-1, -1, 1, 1);
        public static MouseState mouseState;
        public static MouseState mouseLastFrame;
        public static Keys[] pressedKeys;
        public static Keys[] keysLastFrame;

        public static void update() {
            mouseLastFrame = mouseState;
            mouseState = Mouse.GetState();

            mouseRect.X = mouseState.X;
            mouseRect.Y = mouseState.Y;

            if (leftPressed()) {
                initialClick.X = mouseState.X;
                initialClick.Y = mouseState.Y;
            } else if (leftClickRelease()) {
                releaseClick.X = mouseState.X;
                releaseClick.Y = mouseState.Y;
            }
            keysLastFrame = null;
            keysLastFrame = pressedKeys;
            pressedKeys = null;
            pressedKeys = Keyboard.GetState().GetPressedKeys();
        }

        public static bool keyTyped(Keys key) {
            if (pressedKeys == null || keysLastFrame == null) return false;
            return !pressedKeys.Contains(key) && keysLastFrame.Contains(key);
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
                mouseLastFrame.LeftButton == ButtonState.Pressed;
        }
    }
}
