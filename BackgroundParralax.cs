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
        double _speed;
        Vector2 _location;
        Texture2D _texture;

        public BackgroundParralax(Texture2D texture, double speed, Rectangle locationRect)
        {
            _locationRecta = locationRect;
            _locationRectb = locationRect;
            _speed = speed;
            _texture = texture;

            _location.X = _locationRecta.X;
        }

        public void Update()
        {
            _location.X += (float)_speed;

            _locationRecta.X = (int)Math.Round(_location.X);
            _locationRectb.X = _locationRecta.X + 1280;

            if (_locationRecta.Right < 0)
                _location.X = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _locationRecta, Color.White);
            spriteBatch.Draw(_texture, _locationRectb, Color.White);
        }
    }
}