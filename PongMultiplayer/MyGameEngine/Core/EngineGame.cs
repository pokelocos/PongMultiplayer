using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongMultiplayer;
using System;
using System.Collections.Generic;

namespace MyEngine
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class EngineGame : Game
    {
        // Graphics
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        //SceneManager
        public List<Scene> scenes;

        public EngineGame()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = Globals.widthScreen;  
            graphics.PreferredBackBufferHeight = Globals.heightScreen;  
            graphics.ApplyChanges();

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
            ImageManager.Register("circle", Content.Load<Texture2D>("Primitives/circle"));
            ImageManager.Register("square", Content.Load<Texture2D>("Primitives/square"));
            FontManager.Register("MainFont", Content.Load<SpriteFont>("MainFont"));

            //Specific assets
            ImageManager.Register("CreateRoom", Content.Load<Texture2D>("CreateRoom"));
            ImageManager.Register("JoinRoom", Content.Load<Texture2D>("JoinRoom"));
            ImageManager.Register("BackToMenu", Content.Load<Texture2D>("BackToMenu"));
            ImageManager.Register("TextField", Content.Load<Texture2D>("TextField"));
            ImageManager.Register("Background", Content.Load<Texture2D>("BackGround"));
            ImageManager.Register("Paddle", Content.Load<Texture2D>("Paddle"));

            ImageManager.Register("1", Content.Load<Texture2D>("1"));
            ImageManager.Register("2", Content.Load<Texture2D>("2"));
            ImageManager.Register("3", Content.Load<Texture2D>("3"));
            ImageManager.Register("Blank",Content.Load<Texture2D>("Blank"));

            ImageManager.Register("LifeEmpty", Content.Load<Texture2D>("LifeEmpty"));
            ImageManager.Register("LifeFull", Content.Load<Texture2D>("LifeFull"));

            ImageManager.Register("Waiting", Content.Load<Texture2D>("Waiting"));
            ImageManager.Register("Title", Content.Load<Texture2D>("Title"));

            spriteBatch = new SpriteBatch(GraphicsDevice);

            scenes.Add(new MainMenu("MainMenu",true));
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
            InputManager.Update();

            foreach (Scene e in scenes)
            {
                if(!e.isInit)
                {
                    e.Init();
                    e.Start();
                    e.isInit = true;
                }

                if (e.IsActive())
                {
                    e.Actualize();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Globals.color);
            spriteBatch.Begin();

            // Draw common game elements
            foreach (Scene e in scenes)
            {
                if (e.IsActive())
                {
                    e.Draw(spriteBatch);
                }
            }

            // Draw relevant elements in debug mode
            if (Globals.DebugMode)
            { 
                foreach (Scene e in scenes)
                {
                    if (e.IsActive())
                    {
                        e.DrawDebug(spriteBatch);
                    }
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);

            NetworkManager.Disconect();

            foreach (Scene e in scenes)
            {
                e.OnExiting();
            }
        }
    }
}
