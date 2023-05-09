using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lesson_6___Summative
{
    public class Game1 : Game
    {
        List<Texture2D> dinoTexturesWalking, dinoTexturesStanding;
        Texture2D dinoSpritesheet;

        Texture2D groundTexture, treesAndBushesTexture, distantTreesTexture, bushesTexture, hill1Texture, hill2Texture;
        Texture2D hugeCloudsTexture, cloudsTexture, distantClouds1Texture, distantClouds2Texture, backgroundTexture;

        Texture2D buttonTexture, buttonPressed, buttonUnpressed;

        Texture2D planeTexture;

        Rectangle planeRect;

        Vector2 planeSpeed;

        Rectangle buttonRect;

        double dinoIndex;

        List<BackgroundParralax> backgrounds;

        Rectangle dinoRect;

        Vector2 dinoSpeed;

        Rectangle bgRect;


        MouseState mouseState, prevMouseState;
        enum Screen
        {
            Intro, Game, Outro
        }

        Screen screen;

        Point mouse;

        SpriteFont mousePos;

        bool walkRun = false;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            backgrounds = new List<BackgroundParralax>();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            dinoRect = new Rectangle(-100, 600, 72, 72);
            dinoTexturesWalking = new List<Texture2D>();
            dinoTexturesStanding = new List<Texture2D>();
            dinoIndex = 0;

            dinoSpeed = new Vector2(1, 0);

            screen = Screen.Intro;

            bgRect = new Rectangle(0, 0, 1280, 720);

            buttonRect = new Rectangle(200, 200, 405, 195);

            planeRect = new Rectangle(800, 500, 10, 5);

            planeSpeed = new Vector2(-2, -3);

            base.Initialize();
            backgrounds.Add(new BackgroundParralax(distantClouds2Texture, -0.1, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(distantClouds1Texture, -0.2, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(cloudsTexture, -0.3, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(hugeCloudsTexture, -0.4, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(hill2Texture, -0.5, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(hill1Texture, -0.6, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(bushesTexture, -0.7, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(distantTreesTexture, -0.8, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(treesAndBushesTexture, -0.9, new Rectangle(0, 0, 1280, 720)));
            backgrounds.Add(new BackgroundParralax(groundTexture, -1, new Rectangle(0, 0, 1280, 720)));

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            dinoSpritesheet = Content.Load<Texture2D>("dino_sprites");

            groundTexture = Content.Load<Texture2D>("_01_ground");
            treesAndBushesTexture = Content.Load<Texture2D>("_02_trees_and_bushes");
            distantTreesTexture = Content.Load<Texture2D>("_03_distant_trees");
            bushesTexture = Content.Load<Texture2D>("_04_bushes");
            hill1Texture = Content.Load<Texture2D>("_05_hill1");
            hill2Texture = Content.Load<Texture2D>("_06_hill2");
            hugeCloudsTexture = Content.Load<Texture2D>("_07_huge_clouds");
            cloudsTexture = Content.Load<Texture2D>("_08_clouds");
            distantClouds1Texture = Content.Load<Texture2D>("_09_distant_clouds1");
            distantClouds2Texture = Content.Load<Texture2D>("_10_distant_clouds");
            backgroundTexture = Content.Load<Texture2D>("_11_background");

            buttonUnpressed = Content.Load<Texture2D>("start_button");
            buttonPressed = Content.Load<Texture2D>("start_button_clicked");

            mousePos = Content.Load<SpriteFont>("mouse_pos");

            planeTexture = Content.Load<Texture2D>("plane");


            Texture2D cropTexture;
            Rectangle sourceRect;

            int width = dinoSpritesheet.Width / 24;
            int height = dinoSpritesheet.Height;


            for (int x = 4; x < 10; x++)
            {
                sourceRect = new Rectangle(x * width, 0, width, height);
                cropTexture = new Texture2D(GraphicsDevice, width, height);

                Color[] data = new Color[width * height];
                dinoSpritesheet.GetData(0, sourceRect, data, 0, data.Length);

                cropTexture.SetData(data);

                dinoTexturesWalking.Add(cropTexture);
            }

            for (int x = 0; x < 3; x++)
            {
                sourceRect = new Rectangle(x * width, 0, width, height);
                cropTexture = new Texture2D(GraphicsDevice, width, height);

                Color[] data = new Color[width * height];
                dinoSpritesheet.GetData(0, sourceRect, data, 0, data.Length);

                cropTexture.SetData(data);

                dinoTexturesStanding.Add(cropTexture);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            mouseState = Mouse.GetState();

            int x = mouseState.X;
            int y = mouseState.Y;

            mouse = new Point(mouseState.X, mouseState.Y);

            if (screen == Screen.Intro)
            {
                buttonTexture = buttonUnpressed;
                if (buttonRect.Contains(mouse))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        screen = Screen.Game;
                    }

                    prevMouseState = mouseState;
                }

            }

            else if (screen == Screen.Game)
            {
                dinoIndex += 0.15;

                for (int i = 0; i < 10; i++)
                    backgrounds[i].Update();

                if (dinoIndex >= dinoTexturesWalking.Count - 0.5)
                    dinoIndex = 0;

                dinoRect.X += (int)dinoSpeed.X;

                planeRect.X += (int)planeSpeed.X;
                planeRect.Y += (int)planeSpeed.Y;
                planeRect.Width += 2;
                planeRect.Height += 2;
            }

            else if (screen == Screen.Outro)
            {

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(buttonTexture, buttonRect, Color.White);
                _spriteBatch.DrawString(mousePos, $"{mouseState.X}, {mouseState.Y}", new Vector2(50, 150), Color.Black);
            }

            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(backgroundTexture, bgRect, Color.White);

                foreach (BackgroundParralax background in backgrounds)
                    background.Draw(_spriteBatch);


                _spriteBatch.Draw(dinoTexturesWalking[(int)Math.Round(dinoIndex)], dinoRect, Color.White);

                _spriteBatch.Draw(planeTexture, planeRect, Color.White);
            }
            else if (screen == Screen.Outro)
            {

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}