﻿#region Using Statements
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
        TestMenu main;
        public static GameTime time;
        Render test;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            this.IsMouseVisible = true;
            
            Content.RootDirectory = "Content";
            main = new TestMenu("test", new Vector2(20, 20), 600, 500);
            test = new Render();
            test.setStrategy((Render.RenderStrategy)t);
        }

        private void t() {
            Console.WriteLine("hello");
        }

        protected override void Initialize() {
            base.Initialize();
            GUIRoot.initialize(GraphicsDevice, Content);
            GUIRoot.menuHandler.addMenu(main);
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
