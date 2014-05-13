using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameGUI {
    class Animation {
        public int frame = 0;
        private readonly int totalFrames;
        public int frameWidth, frameHeight;
        private readonly Size dimensions;
        public List<Texture2D> frames = new List<Texture2D>();

        public Animation(int[] size) {
            dimensions = new Size(size[0], size[1]);
            totalFrames = dimensions.Width * dimensions.Height;
        }

        public Texture2D currentFrame() {
            return frames[frame];
        }

        public Texture2D getFrame(int frameIndex) {
            if (frameIndex < totalFrames - 1 && frameIndex >= 0) {
                return frames[frameIndex];
            }
            return null;
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
