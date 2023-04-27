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

        Rectangle bgRect;

        double dinoIndex;

        Rectangle dinoRect;

        Vector2 dinoSpeed;

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

            bgRect = new Rectangle(0, 0, 1280, 720);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            dinoIndex += 0.15;

            if (dinoIndex >= dinoTextures.Count - 0.5)
                dinoIndex = 0;

            dinoRect.X += (int)dinoSpeed.X;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

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

            _spriteBatch.Draw(dinoTextures[(int)Math.Round(dinoIndex)], dinoRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}