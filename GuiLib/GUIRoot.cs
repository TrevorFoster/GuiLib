using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {
    class GUIRoot {
        public static GraphicsDevice graphicsDevice;
        public static ContentManager content;
        public static SpriteBatch spriteBatch;
        public static MenuHandler menuHandler;

        public static void initialize(GraphicsDevice graphicsDevice, ContentManager content) {
            GUIRoot.graphicsDevice = graphicsDevice;
            GUIRoot.content = content;
            spriteBatch = new SpriteBatch(graphicsDevice);
            GUIResources.loadContent();
            Shapes.LoadContent();

            menuHandler = new MenuHandler();
            GUIResources.loadFont("font");
        }

        public static void update() {
            menuHandler.update();
        }

        public static void draw() {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            menuHandler.draw();
            spriteBatch.End();
        }
    }
}
