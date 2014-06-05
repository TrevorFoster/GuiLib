#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace GuiLib {
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        List<Menu> menus;
        public static GameTime time;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
            menus = new List<Menu>();
            Random r = new Random();
            for (int i = 0; i < 1; i++) {
                menus.Add(new TestMenu("test", new Vector2(0, 0), 800, 600));
            }
        }

        protected override void Initialize() {
            base.Initialize();
            GUIRoot.initialize(GraphicsDevice, Content);
            for (int i = 0; i < 1; i++) {
                GUIRoot.menuHandler.addMenu(menus[i]);
            }
        }

        protected override void LoadContent() {

        }

        protected override void UnloadContent() {

        }


        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            time = gameTime;
            InputHandler.update();
            GUIRoot.update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GUIRoot.draw();

            base.Draw(gameTime);
        }
    }
}
