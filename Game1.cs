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

        Texture2D button;

        Rectangle buttonRect;

        double dinoIndex;

        Rectangle dinoRect;

        Vector2 dinoSpeed;

        Rectangle bgRect1, bgRect2, bgRect3, bgRect4, bgRect5, bgRect6, bgRect7, bgRect8, bgRect9, bgRect10, bgRect11;
        Vector2 bgSpeed1, bgSpeed2, bgSpeed3, bgSpeed4, bgSpeed5, bgSpeed6, bgSpeed7, bgSpeed8, bgSpeed9, bgSpeed10, bgSpeed11;

        MouseState mouseState;
        enum Screen
        {
            Intro, Game, Outro
        }

        Screen screen;

        Point mouse;

        SpriteFont mousePos;

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

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            dinoRect = new Rectangle(-100, 600, 72, 72);
            dinoTextures = new List<Texture2D>();
            dinoIndex = 0;

            dinoSpeed = new Vector2(1, 0);

            bgSpeed1 = new Vector2(-2, 0);
            bgSpeed2 = new Vector2(-1, 0);

            bgRect1 = new Rectangle(0, 0, 1280, 720);
            bgRect2 = new Rectangle(0, 0, 1280, 720);
            bgRect3 = new Rectangle(0, 0, 1280, 720);
            bgRect4 = new Rectangle(0, 0, 1280, 720);
            bgRect5 = new Rectangle(0, 0, 1280, 720);
            bgRect6 = new Rectangle(0, 0, 1280, 720);
            bgRect7 = new Rectangle(0, 0, 1280, 720);
            bgRect8 = new Rectangle(0, 0, 1280, 720);
            bgRect9 = new Rectangle(0, 0, 1280, 720);
            bgRect10  = new Rectangle(0, 0, 1280, 720);
            bgRect11  = new Rectangle(0, 0, 1280, 720);


            screen = Screen.Intro;

            buttonRect = new Rectangle(200, 200, 405, 195);

            base.Initialize();
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

            button = Content.Load<Texture2D>("start_button");

            mousePos = Content.Load<SpriteFont>("mouse_pos");

            
            Texture2D cropTexture;
            Rectangle sourceRect;

            int width = dinoSpritesheet.Width / 24;
            int height = dinoSpritesheet.Height;

           
            for (int x = 5; x < 10; x++)
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
                if (buttonRect.Contains(mouse))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        screen = Screen.Game;
                    }
                }

            }

            else if (screen == Screen.Game)
            {
                dinoIndex += 0.15;

                if (dinoIndex >= dinoTextures.Count - 0.5)
                    dinoIndex = 0;

                dinoRect.X += (int)dinoSpeed.X;

                bgRect1.X += (int)bgSpeed1.X;
                bgRect2.X += (int)bgSpeed2.X;

                if (bgRect1.Right <= 0)
                {
                    bgRect1 = new Rectangle(0, 0, 1280, 720);
                }
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
                _spriteBatch.Draw(button, buttonRect, Color.White);
                _spriteBatch.DrawString(mousePos, $"{mouseState.X}, {mouseState.Y}", new Vector2(50, 150), Color.Black);
            }

            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(backgroundTexture, bgRect11, Color.White);
                _spriteBatch.Draw(distantClouds2Texture, bgRect10, Color.White);
                _spriteBatch.Draw(distantClouds1Texture, bgRect9, Color.White);
                _spriteBatch.Draw(cloudsTexture, bgRect8, Color.White);
                _spriteBatch.Draw(hugeCloudsTexture, bgRect7, Color.White);
                _spriteBatch.Draw(hill2Texture, bgRect6, Color.White);
                _spriteBatch.Draw(hill1Texture, bgRect5, Color.White);
                _spriteBatch.Draw(bushesTexture, bgRect4, Color.White);
                _spriteBatch.Draw(distantTreesTexture, bgRect3, Color.White);
                _spriteBatch.Draw(treesAndBushesTexture, bgRect2, Color.White);
                _spriteBatch.Draw(groundTexture, bgRect1, Color.White);

                _spriteBatch.Draw(dinoTextures[(int)Math.Round(dinoIndex)], dinoRect, Color.White);

            }
            else if (screen == Screen.Outro)
            {

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}