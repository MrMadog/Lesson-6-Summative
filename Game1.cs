using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Lesson_6___Summative
{
    public class Game1 : Game
    {
        List<Texture2D> dinoTextures;
        Texture2D dinoSpritesheet;

        int dinoIndex;

        Rectangle dinoRect;

        MouseState mouseState, prevMouseState;

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

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            dinoRect = new Rectangle(300, 50, 100, 100);
            dinoTextures = new List<Texture2D>();
            dinoIndex = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            dinoSpritesheet = Content.Load<Texture2D>("dino_sprites");

            Texture2D cropTexture;
            Rectangle sourceRect;

            int width = dinoSpritesheet.Width / 24;
            int height = dinoSpritesheet.Height / 1;

            for (int y = 1; y < 1; y++)
            {
                for (int x = 1; x < 24; x++)
                {
                    sourceRect = new Rectangle(x * width, y * height, width, height);
                    cropTexture = new Texture2D(GraphicsDevice, width, height);

                    Color[] data = new Color[width * height];
                    dinoSpritesheet.GetData(0, sourceRect, data, 0, data.Length);

                    cropTexture.SetData(data);

                    if (dinoTextures.Count < 24)
                        dinoTextures.Add(cropTexture);
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                dinoIndex -= 1;

            else if (mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
                dinoIndex += 1;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(dinoTextures[dinoIndex], dinoRect, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}