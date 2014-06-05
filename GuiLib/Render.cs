using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    
    class Renderer {
        public delegate void RenderStrategy();
        private RenderStrategy renderStrategy;
        public RenderTarget2D image;
        private Size imageSize;
        private bool initialized;

        public Renderer() {

        }

        public void initialize() {
            image = new RenderTarget2D(GUIRoot.graphicsDevice, 1, 1);
            initialized = true;
        }

        public void render(Size size) {
            if (!initialized) return;

            if (imageSize != size) {
                if(image != null) 
                    image.Dispose();
                image = new RenderTarget2D(GUIRoot.graphicsDevice, size.Width, size.Height);
                imageSize = size;
            }
            GUIRoot.graphicsDevice.SetRenderTarget(image);
            GUIRoot.graphicsDevice.Clear(Color.Transparent);
            GUIRoot.spriteBatch.Begin();
            renderStrategy();
            GUIRoot.spriteBatch.End();

            GUIRoot.graphicsDevice.SetRenderTarget(null);
        }

        public void setStrategy(Delegate strategy){
            if (strategy == null) return;
            renderStrategy = (RenderStrategy)strategy;
        }
    }
}
