using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class NonEnemyCars
    {
        float _moveSpeed;
        float _carRotation;

        public Vector2 _position;

        Rectangle _source;
        public Rectangle _colRect;

        Texture2D _carTexture;
        Texture2D _debugPixel;

        public NonEnemyCars(Texture2D carTexture, Vector2 position, Rectangle source, float carRotation, Texture2D debugPixel) 
        {
            _carTexture = carTexture;
            _debugPixel = debugPixel;
            _position = position;
            _source = source;
            _carRotation = carRotation;

            _colRect = new Rectangle((int)_position.X - _source.Height / 2,
                (int)_position.Y - _source.Width / 2,
                _source.Height,
                _source.Width);
        }

        public void UpdateNonEnemyCars()
        {
            UpdateNonEnemyCollision();

            if(_position.Y < Globals.screenSize.Y / 2)
            {
                _moveSpeed = -5f;
            }
            else
            {
                 if(_position.Y > Globals.screenSize.Y / 2)
                 {
                    _moveSpeed = 5f;
                 }
            }

            _position.X += _moveSpeed;
        }

        void UpdateNonEnemyCollision()
        {
            _colRect = new Rectangle((int)_position.X - _source.Height / 2,
                (int)_position.Y - _source.Width / 2,
                _source.Height,
                _source.Width);
        }

        public void DrawNonEnemyCars()
        {
            Globals.spriteBatch.Draw(_carTexture,
                _position,
                _source,
                Color.White,
                MathHelper.ToRadians(_carRotation),
                new Vector2(_source.Width / 2, _source.Height / 2),
                1f,
                SpriteEffects.None,
                1f);

            //Globals.spriteBatch.Draw(_debugPixel,
            //    _colRect,
            //    Color.Red);
        }
    }
}
