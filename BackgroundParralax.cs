using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_6___Summative
{
    public class BackgroundParralax
    {
        Rectangle _locationRecta;
        Rectangle _locationRectb;

        // _speed into _location to locationRect
        Vector2 _speed;
        Vector2 _location;
        Texture2D _texture;

        public BackgroundParralax(Texture2D texture, Vector2 speed, Rectangle locationRect)
        {
            _locationRecta = locationRect;
            _locationRectb = locationRect;
            _speed = speed;
            _texture = texture;
        }

        public void Update()
        {
            _speed = new Vector2((float)0.1, 0);

            _location = (int)Math.Round(_speed.X, 1);

            _locationRecta.X += (int)_location.X;
            _locationRectb.X = _locationRecta.X + 1280;

            if (_locationRecta.Right < 0)
                _locationRecta.X = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _locationRecta, Color.White);
            spriteBatch.Draw(_texture, _locationRectb, Color.White);
        }



    }
}
