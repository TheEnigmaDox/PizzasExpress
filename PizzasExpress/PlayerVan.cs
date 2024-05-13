using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace PizzasExpress
{
    internal class PlayerVan
    {
        public int _playerScore = 0;
        public int _playerHealth = 3;

        bool _isMouseButtonPressed = false;
        public bool _isDead = false;

        float _moveSpeed = 5f;

        public Vector2 _position;

        Rectangle _source;
        public Rectangle _colRect;
        Rectangle _fireTrigger;

        Texture2D _vanTexture;
        Texture2D _pizzaBullet;
        Texture2D _debugPixel;

        public List<PizzaBullet> _bullets = new List<PizzaBullet>();

        public PlayerVan(Texture2D vanTexture, Texture2D debugPixel, Vector2 position, Rectangle source, Texture2D pizzaBullet)
        {
            _vanTexture = vanTexture;
            _position = position;
            _pizzaBullet = pizzaBullet;

            _source = source;
            _colRect = new Rectangle((int)_position.X - _source.Height / 2,
                (int)_position.Y - _source.Width / 2,
                _source.Height,
                _source.Width);

            _fireTrigger = new Rectangle((int)_position.X + _source.Height,
                (int)_position.Y - _source.Width / 2,
                _source.Width / 2,
                _source.Width);

            _debugPixel = debugPixel;
        }

        public void UpdatePlayerVan(GameTime gameTime,
            GamePadState gamePadState,
            KeyboardState keyboardState,
            MouseState mouseState,
            List<NonEnemyCars> nonEnemyCars)
        {
            UpdatePLayerVanCollision(nonEnemyCars);
            MovePlayerVan(gamePadState, keyboardState);

            foreach (PizzaBullet eachBullet in _bullets)
            {
                eachBullet.UpdateBullet();
            }

            if (!_isDead)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && !_isMouseButtonPressed)
                {
                    _isMouseButtonPressed = true;
                    _bullets.Add(new PizzaBullet(_pizzaBullet, new Vector2(_position.X, _position.Y - _pizzaBullet.Height / 2)));
                }
                else if (mouseState.LeftButton == ButtonState.Released && _isMouseButtonPressed)
                {
                    _isMouseButtonPressed = false;
                } 
            }

            //Debug.WriteLine("Player health : " + _playerHealth);
        }

        private void MovePlayerVan(GamePadState gamePadState, KeyboardState keyboardState)
        {
            if (gamePadState.ThumbSticks.Left.X < 0 || keyboardState.IsKeyDown(Keys.A))
            {
                _position.X -= _moveSpeed;
            }

            if (gamePadState.ThumbSticks.Left.X > 0 || keyboardState.IsKeyDown(Keys.D))
            {
                _position.X += _moveSpeed;
            }

            if (gamePadState.ThumbSticks.Left.Y > 0 || keyboardState.IsKeyDown(Keys.W))
            {
                _position.Y -= _moveSpeed;
            }

            if (gamePadState.ThumbSticks.Left.Y < 0 || keyboardState.IsKeyDown(Keys.S))
            {
                _position.Y += _moveSpeed;
            }
        }

        private void UpdatePLayerVanCollision(List<NonEnemyCars> nonEnemyCars)
        {
            _colRect = new Rectangle((int)_position.X - _source.Height / 2,
                (int)_position.Y - _source.Width / 2,
                _source.Height,
                _source.Width);

            for (int i = 0; i < nonEnemyCars.Count; i++)
            {
                if (_colRect.Intersects(nonEnemyCars[i]._colRect))
                {
                    nonEnemyCars.Remove(nonEnemyCars[i]);
                    if (_playerHealth > 0)
                    {
                        _playerHealth--;
                    }
                }
            }

            if (_playerHealth == 0)
            {
                _isDead = true;
            }
        }

        public void AddToScore(int score)
        {
            _playerScore += score;
        }

        public void DrawPlayerVan()
        {
            if (!_isDead)
            {
                Globals.spriteBatch.Draw(_vanTexture,
                        new Rectangle((int)_position.X, (int)_position.Y, _source.Width, _source.Height),
                        _source,
                        Color.White,
                        MathHelper.ToRadians(90),
                        new Vector2(_source.Width / 2, _source.Height / 2),
                        SpriteEffects.None,
                        1f);
            }

            foreach (PizzaBullet eachBullet in _bullets)
            {
                eachBullet.DrawBullet(_debugPixel);
            }

            //Globals.spriteBatch.Draw(_debugPixel,
            //    _colRect,
            //    Color.Red);
        }
    }
}
