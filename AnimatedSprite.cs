using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;

namespace Lesson_6___Summative
{
    public class AnimatedSprite
    {
        public List<Texture2D> dinoTextures;
        public Texture2D dinoSpritesheet;
        public int frameStart { get; set; }
        public int frameEnd { get; set; }

        public double dinoIndex;
        Rectangle dinoRect;

        public AnimatedSprite(int start, int end)
        {
            frameStart = start;
            frameEnd = end;
        }

        public void Initialize()
        {
            dinoTextures = new List<Texture2D>();

            dinoRect = new Rectangle(100, 600, 72, 72);
        }

        public void Animation(GraphicsDevice graphicsDevice)
        {
            dinoTextures = new List<Texture2D>();

            Texture2D cropTexture;
            Rectangle sourceRect;

            int width = dinoSpritesheet.Width / 24;
            int height = dinoSpritesheet.Height;


            for (int x = frameStart; x < frameEnd; x++)
            {
                sourceRect = new Rectangle(x * width, 0, width, height);
                cropTexture = new Texture2D(graphicsDevice, width, height);

                Color[] data = new Color[width * height];
                dinoSpritesheet.GetData(0, sourceRect, data, 0, data.Length);

                cropTexture.SetData(data);

                dinoTextures.Add(cropTexture);
            }
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle dinoRect)
        {

            spriteBatch.Draw(dinoTextures[(int)Math.Round(dinoIndex)], dinoRect, Color.White);
        }
    }
}
