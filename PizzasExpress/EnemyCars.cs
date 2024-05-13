using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace PizzasExpress
{
    internal class EnemyCars
    {
        public bool _isDead = false;
        bool _topCollision = false;
        bool _bottomCollision = false;

        float crashDistance = 200f;
        float _moveSpeed = 5f;
        float _carRotation;

        Vector2 _position;
        Vector2 directionToDrive;

        Rectangle _source;
        public Rectangle _colRect;

        Texture2D _carTexture;
        Texture2D _debugPixel;

        Game1 _gameOne;

        public EnemyCars(Texture2D carTexture, Vector2 position, Rectangle source, float carRotation, Texture2D debugPixel, Game1 gameOne)
        {
            _carTexture = carTexture;
            _debugPixel = debugPixel;
            _position = position;
            _source = source;
            _carRotation = carRotation;

            if (_position.Y == 200)
            {
                _bottomCollision = true;
            }
            else if (_position.Y == 400)
            {
                _topCollision = true;
            }

            _colRect = new Rectangle((int)_position.X - _source.Height / 2,
                (int)_position.Y - _source.Width / 2,
                _source.Height,
                _source.Width);
            _gameOne = gameOne; 
        }

        public void UpdateEnemyCars(PlayerVan playerVan)
        {
            Vector2 driveLeft = new Vector2(-1, 0);
            Vector2 driveRight = new Vector2(1, 0);

            if (_gameOne.gameState == Game1.GameState.Game)
            {
                Vector2 driveCollision = playerVan._position - _position;
                Vector2 targetDirection = Vector2.Normalize(playerVan._position - _position);

                if (Vector2.Distance(playerVan._position, _position) < crashDistance)
                {
                    if (directionToDrive == driveLeft && driveCollision.X < 1)
                    {
                        directionToDrive = targetDirection;
                        _moveSpeed = Globals.rng.Next(3, 8);
                    }
                    else if (directionToDrive == driveRight && driveCollision.X > 1)
                    {
                        directionToDrive = targetDirection;
                        _moveSpeed = Globals.rng.Next(3, 8);
                    }
                }
            }

            UpdateEnemyCollision();

            if (_position.Y == 200 && _position.X > 0 - _carTexture.Width)
            {
                directionToDrive = driveLeft;
            }
            else if (_position.Y == 400 && _position.X < Globals.screenSize.X + _carTexture.Width)
            {
                directionToDrive = driveRight;
            }

            _position += directionToDrive * _moveSpeed;

            //Debug.WriteLine("Distance : " + Vector2.Distance(playerVan._position, _position));
        }

        void UpdateEnemyCollision()
        {
            _colRect = new Rectangle((int)_position.X - _source.Height / 2,
                (int)_position.Y - _source.Width / 2,
                _source.Height,
                _source.Width);
        }

        public void DrawEnemyCars()
        {
            if (!_isDead)
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
            }

            //Globals.spriteBatch.Draw(_debugPixel,
            //    _colRect,
            //    Color.Red);
        }
    }
}
