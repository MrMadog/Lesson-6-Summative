using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Lesson_6___Summative
{
    public class Game1 : Game
    {
        List<Texture2D> dinoTextures;
        Texture2D dinoSpritesheet;

        Texture2D groundTexture, treesAndBushesTexture, distantTreesTexture, bushesTexture, hill1Texture, hill2Texture;
        Texture2D hugeCloudsTexture, cloudsTexture, distantClouds1Texture, distantClouds2Texture, backgroundTexture;

        Texture2D buttonTexture, startButtonPressed, startButtonUnpressed, exitButtonTexture, exitButtonPressed, exitButtonUnpressed;
        Texture2D menuButtonTexture, menuButtonUnpressed, menuButtonPressed;

        Texture2D planeTexture;

        Texture2D introScreen, outroScreen;

        Rectangle planeRect;

        Vector2 planeSpeed;

        Rectangle startButtonRect, exitButtonRect, menuButtonRect, exitButtonRectOutro;

        double dinoIndex;

        SpriteFont titleFont;

        List<BackgroundParralax> backgrounds;

        Rectangle dinoRect;

        Vector2 dinoSpeed;

        Rectangle bgRect;

        Color bgColour;

        MouseState mouseState, prevMouseState;
        enum Screen
        {
            Intro, Game, Outro
        }

        Screen screen;

        Point mouse;

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
            dinoTextures = new List<Texture2D>();
            dinoIndex = 0;

            dinoSpeed = new Vector2(1, 0);

            screen = Screen.Intro;

            bgRect = new Rectangle(0, 0, 1280, 720);

            startButtonRect = new Rectangle(440, 500, 400, 130);
            exitButtonRect = new Rectangle(1130, 20, 96, 39);
            exitButtonRectOutro = new Rectangle(528, 350, 224, 91);
            menuButtonRect = new Rectangle(413, 500, 455, 91);

            planeRect = new Rectangle(800, 400, 12, 5);

            planeSpeed = new Vector2(-2, -3);

            bgColour = Color.CornflowerBlue;

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

            startButtonUnpressed = Content.Load<Texture2D>("SButton");
            startButtonPressed = Content.Load<Texture2D>("SButtonPressed");
            exitButtonUnpressed = Content.Load<Texture2D>("EButton");
            exitButtonPressed = Content.Load<Texture2D>("EButtonPressed");
            menuButtonUnpressed = Content.Load<Texture2D>("MMButton");
            menuButtonPressed = Content.Load<Texture2D>("MMButtonPressed");

            planeTexture = Content.Load<Texture2D>("plane");

            introScreen = Content.Load<Texture2D>("intro_screen");
            outroScreen = Content.Load<Texture2D>("outro_screen");

            titleFont = Content.Load<SpriteFont>("title_font");


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

                dinoTextures.Add(cropTexture);
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
                buttonTexture = startButtonUnpressed;
                exitButtonTexture = exitButtonUnpressed;
                menuButtonTexture = menuButtonUnpressed;

                if (startButtonRect.Contains(mouse))
                {
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        buttonTexture = startButtonPressed;

                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Game;
                        }
                    }
                    prevMouseState = mouseState;
                }

                if (exitButtonRect.Contains(mouse))
                {
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        exitButtonTexture = exitButtonPressed;

                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Outro;
                        }
                    }
                    prevMouseState = mouseState;
                }
            }

            else if (screen == Screen.Game)
            {
                exitButtonTexture = exitButtonUnpressed;

                dinoIndex += 0.15;

                for (int i = 0; i < 10; i++)
                    backgrounds[i].Update();

                if (dinoIndex >= dinoTextures.Count - 0.5)
                    dinoIndex = 0;

                dinoRect.X += (int)dinoSpeed.X;


                if (dinoRect.X >= 300)
                {
                    planeRect.X += (int)planeSpeed.X;
                    planeRect.Y += (int)planeSpeed.Y;
                    planeRect.Width += 2;
                    planeRect.Height += 2;
                }

                if (dinoRect.X > 1280)
                    screen = Screen.Outro;

                if (exitButtonRect.Contains(mouse))
                {
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        exitButtonTexture = exitButtonPressed;

                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Outro;
                        }
                    }
                    prevMouseState = mouseState;
                }
            }

            else if (screen == Screen.Outro)
            {
                dinoRect.X = -100;
                planeRect = new Rectangle(800, 400, 12, 5);
                exitButtonTexture = exitButtonUnpressed;
                menuButtonTexture = menuButtonUnpressed;

                if (exitButtonRectOutro.Contains(mouse))
                {
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        exitButtonTexture = exitButtonPressed;

                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            Exit();
                        }
                    }
                    prevMouseState = mouseState;
                }

                if (menuButtonRect.Contains(mouse))
                {
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        menuButtonTexture = menuButtonPressed;

                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Intro;
                        }
                    }
                    prevMouseState = mouseState;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColour);

            _spriteBatch.Begin();

            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreen, new Rectangle(0, 0, 1280, 720), Color.White);
                _spriteBatch.Draw(buttonTexture, startButtonRect, Color.White);
                _spriteBatch.Draw(exitButtonTexture, exitButtonRect, Color.White);
                _spriteBatch.DrawString(titleFont, "Dino Animation", new Vector2(70, 70), Color.Black);
            }

            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(backgroundTexture, bgRect, Color.White);

                foreach (BackgroundParralax background in backgrounds)
                    background.Draw(_spriteBatch);

                _spriteBatch.Draw(dinoTextures[(int)Math.Round(dinoIndex)], dinoRect, Color.White);
                
                if (dinoRect.X >= 300)
                     _spriteBatch.Draw(planeTexture, planeRect, Color.White);

                _spriteBatch.Draw(exitButtonTexture, exitButtonRect, Color.White);

            }
            else if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(outroScreen, new Rectangle(0, 0, 1280, 720), Color.White);
                _spriteBatch.Draw(exitButtonTexture, exitButtonRectOutro, Color.White);
                _spriteBatch.Draw(menuButtonTexture, menuButtonRect, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}