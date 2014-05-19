using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class Animation {
        public int frameWidth, frameHeight;
        private readonly Size dimensions;

        public List<Texture2D> frames;
        public int frame = 0;
        private int lastFrame = -1;

        public Vector2 offset;
        private Vector2 scale;

        public bool automated = false;
        private float deltaTime;
        public float interval;

        public Animation(int dimW, int dimY) {
            frames = new List<Texture2D>();
            dimensions = new Size(dimW, dimY);

            offset = Vector2.Zero;
            scale = Vector2.One;
        }

        public Animation(int dimW, int dimY, Vector2 offset) {
            frames = new List<Texture2D>();
            dimensions = new Size(dimW, dimY);

            this.offset = offset;
            scale = Vector2.One;
        }

        public void updateScale(Vector2 newSize) {
            scale = new Vector2(newSize.X / ((frameWidth == 0) ? 1 : frameWidth), newSize.Y / ((frameHeight == 0) ? 1 : frameHeight));
        }

        public Texture2D currentFrame() {
            return frames[frame];
        }

        public Texture2D getFrame(int frameIndex) {
            if (frameIndex < frames.Count - 1 && frameIndex >= 0) {
                return frames[frameIndex];
            }
            return null;
        }

        public bool needsRender() {
            return frame != lastFrame;
        }

        public void update() {
            lastFrame = frame;
            if (!automated) return;

            if (deltaTime < interval) {
                deltaTime += Game1.time.ElapsedGameTime.Milliseconds / 1000f;
            } else {
                nextFrame();
                deltaTime = 0;
            }
        }

        private void nextFrame() {
            if (frame < frames.Count) {
                frame++;
            } else {
                frame = 0;
            }
        }

        public void draw(Vector2 origin) {
            GUIRoot.spriteBatch.Draw(currentFrame(), origin + offset, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        public void loadSheet(Texture2D texture, Rectangle source) {
            Color[] pixels = new Color[texture.Width * texture.Height];
            texture.GetData(pixels);

            frameWidth = source.Width / dimensions.Width;
            frameHeight = source.Height / dimensions.Height;

            for (int y = source.Top; y < source.Bottom; y += frameHeight) {
                for (int x = source.Left; x < source.Right; x += frameWidth) {
                    Texture2D frameTexture = new Texture2D(GUIRoot.graphicsDevice, frameWidth, frameHeight);
                    Color[] framePixels = new Color[frameWidth * frameHeight];
                    for (int j = 0; j < frameWidth; j++) {
                        for (int k = 0; k < frameHeight; k++) {
                            framePixels[j + k * frameWidth] = pixels[(x + j) + (y + k) * texture.Width];
                        }
                    }
                    frameTexture.SetData(framePixels);
                    frames.Add(frameTexture);
                }
            }
        }
    }
}
