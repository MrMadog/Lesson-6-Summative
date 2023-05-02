using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

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

        Rectangle bgRect;

        double dinoIndex;

        Rectangle dinoRect;

        //Vector2 dinoSpeed;

        AnimatedSprite animatedSprite;

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

            //dinoSpeed = new Vector2(1, 0);

            bgRect = new Rectangle(0, 0, 1280, 720);

            animatedSprite = new AnimatedSprite(5, 10);

            screen = Screen.Intro;

            buttonRect = new Rectangle(200, 200, 405, 195);

            mouse = new Point(mouseState.X, mouseState.Y);

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

            /*
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
            */
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            int x = mouseState.X;
            int y = mouseState.Y;

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

                //dinoRect.X += (int)dinoSpeed.X;
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
                _spriteBatch.Draw(backgroundTexture, bgRect, Color.White);
                _spriteBatch.Draw(distantClouds2Texture, bgRect, Color.White);
                _spriteBatch.Draw(distantClouds1Texture, bgRect, Color.White);
                _spriteBatch.Draw(cloudsTexture, bgRect, Color.White);
                _spriteBatch.Draw(hugeCloudsTexture, bgRect, Color.White);
                _spriteBatch.Draw(hill2Texture, bgRect, Color.White);
                _spriteBatch.Draw(hill1Texture, bgRect, Color.White);
                _spriteBatch.Draw(bushesTexture, bgRect, Color.White);
                _spriteBatch.Draw(distantTreesTexture, bgRect, Color.White);
                _spriteBatch.Draw(treesAndBushesTexture, bgRect, Color.White);
                _spriteBatch.Draw(groundTexture, bgRect, Color.White);

                //animatedSprite.Draw(_spriteBatch, new Rectangle());

            }
            else if (screen == Screen.Outro)
            {

            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}