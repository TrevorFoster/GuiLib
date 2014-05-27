using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GuiLib {
    class Animation {
        public int frameWidth, frameHeight;
        private readonly Size dimensions;

        public List<Rectangle> frames;
        public int frame = 0;
        private int lastFrame = -1;

        public Vector2 offset;
        private Vector2 scale;

        public bool automated = false;
        private float deltaTime;
        public float interval = 1.0f;
        private Sheet sheet;

        
        public Animation(int dimW, int dimY, Sheet sheet) {
            frames = new List<Rectangle>();
            dimensions = new Size(dimW, dimY);

            offset = Vector2.Zero;
            scale = Vector2.One;
        }

        public Animation(int dimW, int dimY, Sheet sheet, Vector2 offset) {
            frames = new List<Rectangle>();
            dimensions = new Size(dimW, dimY);
            this.sheet = sheet;

            this.offset = offset;
            scale = Vector2.One;
        }

        /*public Animation Copy() {
            Animation copy = new Animation(this.dimensions.Width, this.dimensions.Height, sheet, this.offset);
            copy.frame = this.frame;
            copy.frameWidth = this.frameWidth;
            copy.frameHeight = this.frameHeight;
        }*/

        /// <summary>
        /// Changes the scale of this animation to the new
        /// scale passed.
        /// </summary>
        /// <param name="newSize">The new scale (width, height) for the animation part</param>
        public void updateScale(Vector2 newSize) {
            scale = new Vector2(newSize.X / ((frameWidth == 0) ? 1 : frameWidth), newSize.Y / ((frameHeight == 0) ? 1 : frameHeight));
        }

        /// <summary>
        /// Returns the animation's current frame.
        /// </summary>
        /// <returns>Current frame</returns>
        public Rectangle currentFrame() {
            return frames[frame];
        }

        public bool needsRender() {
            return frame != lastFrame;
        }

        /// <summary>
        /// Update the current frame for the animation.
        /// </summary>
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

        /// <summary>
        /// Changes to the animation's next frame
        /// </summary>
        private void nextFrame() {
            if (frame < frames.Count - 1) {
                frame++;
            } else {
                frame = 0;
            }
        }

        /// <summary>
        /// Draws the animation's current frame from the given orign.
        /// </summary>
        /// <param name="origin">The origin to draw the animation offset from.</param>
        public void draw(Vector2 origin) {
            GUIRoot.spriteBatch.Draw(GUIResources.sheets[sheet], origin + offset, currentFrame(), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Loads in a sprite from a given source rectangle.
        /// </summary>
        /// <param name="texture">The sheet to extract the sprite from</param>
        /// <param name="source">Where to extract the sprite from</param>
        public void loadSheet(Rectangle source) {

            frameWidth = source.Width / dimensions.Width;
            frameHeight = source.Height / dimensions.Height;
            for (int y = 0; y < dimensions.Height; y++) {
                for (int x = 0; x < dimensions.Width; x++) {
                    frames.Add(new Rectangle(source.Left + x * frameWidth, source.Top + y * frameHeight, frameWidth, frameHeight));
                }
            }
            /*
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
                }*/
        }
    }
}
