using Game1.Engine;
using Game1.Specifics;
using Game1.Specifics.lab7;
using Game1.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EngineGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Scene> scenes;

        public EngineGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            scenes = new List<Scene>();

            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            base.Initialize();
            foreach (Scene e in scenes)
            {
                if(e.IsActive())
                {
                    e.Init();
                }
            }

            foreach (Scene e in scenes)
            {
                if (e.IsActive())
                {
                    e.Start();
                }
            }
        }

        protected override void LoadContent()
        {
            //primitives
            ImageManager.Register("circle", Content.Load<Texture2D>("circle"));
            ImageManager.Register("square", Content.Load<Texture2D>("square"));

            spriteBatch = new SpriteBatch(GraphicsDevice);

            ModelManager.Register("hidrante", Content.Load<Model>("hidrante"));
            ModelManager.Register("base", Content.Load<Model>("base"));
            ModelManager.Register("aro", Content.Load<Model>("aro"));
            ModelManager.Register("tabla", Content.Load<Model>("tabla"));
            ModelManager.Register("ball", Content.Load<Model>("ball"));
            ModelManager.Register("min_ball", Content.Load<Model>("min_ball"));

            scenes.Add(new Basket(true));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Time.clock = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            { 
                Exit();
            }

            Physics.Update();

            foreach (Scene e in scenes)
            {
                if(e.IsActive())
                {
                    e.Actualize();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (Scene e in scenes)
            {
                if(e.IsActive())
                {
                    e.Draw(spriteBatch);
                }
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
