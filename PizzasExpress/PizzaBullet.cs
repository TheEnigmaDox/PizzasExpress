using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class PizzaBullet
    {
        float _moveSpeed = 10f;

        Vector2 _position;

        public Rectangle _colRect;

        Texture2D _bulletTexture;

        public PizzaBullet(Texture2D bulletTexture, Vector2 position) 
        {
            _bulletTexture = bulletTexture;
            _position = position;

            _colRect = new Rectangle((int)_position.X, 
                (int)_position.Y,
                _bulletTexture.Width,
                _bulletTexture.Height);
        }

        public void UpdateBullet()
        {
            UpdateCollision();
            _position.X += _moveSpeed;
        }

        void UpdateCollision()
        {
            _colRect = new Rectangle((int)_position.X,
                (int)_position.Y,
                _bulletTexture.Width,
                _bulletTexture.Height);
        }

        public void DrawBullet(Texture2D debugPixel)
        {
            Globals.spriteBatch.Draw(_bulletTexture,
                _position,
                Color.White);

            //Globals.spriteBatch.Draw(debugPixel,
            //    _colRect,
            //    Color.White);
        }
    }
}
