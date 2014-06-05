using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GuiLib {
    class AnimationSet {
        public List<Animation> animations;
        public Vector2 origin;
        public RenderTarget2D rendered = null;
        private int renderedWidth, renderedHeight;

        public AnimationSet() {
            animations = new List<Animation>();
            origin = Vector2.Zero;
            renderedWidth = 0;
            renderedHeight = 0;
        }

        public void update() {
            foreach (Animation animation in animations) {
                animation.update();
            }
        }

        public void render(bool startBatch) {
            int newWidth = 0, newHeight = 0;
            bool needsRender = false;

            foreach (Animation anim in animations) {
                if (anim.needsRender()) {
                    needsRender = true;
                }

                int right = (int)(anim.offset.X + anim.frameWidth);
                int bottom = (int)(anim.offset.Y + anim.frameHeight);

                if (right > newWidth) {
                    newWidth = right;
                }
                if (bottom > newHeight) {
                    newHeight = bottom;
                }
            }

            if (rendered == null || (newWidth != renderedWidth && newHeight != renderedHeight)) {
                rendered = new RenderTarget2D(GUIRoot.graphicsDevice, newWidth, newHeight);
                renderedWidth = newWidth;
                renderedHeight = newHeight;
                needsRender = true;
            }
            if (!needsRender) return;

            GUIRoot.graphicsDevice.SetRenderTarget(rendered);
            GUIRoot.graphicsDevice.Clear(Color.Transparent);
            if (startBatch)
                GUIRoot.spriteBatch.Begin();
            draw(Vector2.Zero);
            if (startBatch)
                GUIRoot.spriteBatch.End();

            GUIRoot.graphicsDevice.SetRenderTarget(null);
        }

        public void draw(Vector2 offset) {
            Vector2 totOffs = offset + origin;

            foreach (Animation animation in animations) {
                animation.draw(totOffs);
            }
        }

        public void draw() {
            foreach (Animation animation in animations) {
                animation.draw(origin);
            }
        }

        public void setFrames(int frame) {
            foreach (Animation animation in animations) {
                if (frame <= animation.frames.Count - 1 && frame >= 0) {
                    animation.frame = frame;
                }
            }
        }
    }
}
