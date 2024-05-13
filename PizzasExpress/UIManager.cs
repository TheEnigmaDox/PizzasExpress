using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class UIManager
    {
        int _playerScore;

        float _titleTimer = 1f;
        float _maxTitleTimer = 1f;

        Vector2 _origin;

        Rectangle _startButton;
        Rectangle _exitButton;
        Rectangle _mouseRect;

        Texture2D _scoreBar;
        Texture2D _debugPixel;
        List<Texture2D> _playerHealth = new List<Texture2D>();

        SpriteFont _gameFont;
        SpriteFont _titleFont;

        Color _titleColour;

        PlayerVan _playerVan;

        Game1 _gameOne;

        public UIManager(List<Texture2D> playerHealth, Texture2D scoreBar, SpriteFont gameFont, SpriteFont titleFont, Game1 gameOne, PlayerVan playerVan, Texture2D debugPixel) 
        {
            _gameOne = gameOne;

            _playerVan = playerVan;

            _playerHealth = playerHealth;

            _gameFont = gameFont;
            _titleFont = titleFont;

            _scoreBar = scoreBar;

            _origin = new Vector2(_playerHealth[3].Width / 2, _playerHealth[3].Height / 2);

            _titleColour = new Color(Globals.rng.Next(0, 256), Globals.rng.Next(0, 256), Globals.rng.Next(0, 256));

            _debugPixel = debugPixel;
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            switch (_gameOne.gameState)
            {
                case Game1.GameState.TitleScreen:
                    UpdateTitleUI(gameTime, mouseState);
                    break;
                case Game1.GameState.Game:
                    UpdateGameUI();
                    break;
                case Game1.GameState.GameOver:
                    UpdateGameOverUI();
                    break;
            }

            //if (_gameOne.gameState == Game1.GameState.Game)
            //{
            //    _playerScore = _playerVan._playerScore; 
            //}
        }

        void UpdateTitleUI(GameTime gameTime, MouseState mouseState) 
        {
            _startButton = new Rectangle((int)(Globals.screenSize.X / 3 - _playerHealth[3].Width * 1.5f / 2),
                (int)(Globals.screenSize.Y / 4 * 3 + 20), (int)(_playerHealth[3].Width * 1.5f), _playerHealth[3].Height);

            _exitButton = new Rectangle(Globals.screenSize.X / 3 * 2 - (int)(_playerHealth[3].Width * 1.5f / 2),
                Globals.screenSize.Y / 4 * 3 + 20, (int)(_playerHealth[3].Width * 1.5f), _playerHealth[3].Height);

            _mouseRect = new Rectangle(mouseState.Position.X - 4, mouseState.Position.Y - 4, 8, 8);

            if(_mouseRect.Intersects(_startButton) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _gameOne.gameState = Game1.GameState.Game;
            }

            if (_mouseRect.Intersects(_exitButton) && mouseState.LeftButton == ButtonState.Pressed)
            {
                _gameOne.Exit();
            }

            if (_titleTimer > 0)
            {
                _titleTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                _titleColour = new Color(Globals.rng.Next(0, 256), Globals.rng.Next(0, 256), Globals.rng.Next(0, 256));
                _titleTimer = _maxTitleTimer;
            }
        }

        void UpdateGameUI()
        {
            if (_gameOne.gameState == Game1.GameState.Game)
            {
                _playerScore = _playerVan._playerScore;
            }
        }

        void UpdateGameOverUI()
        {

        }

        public void DrawUI()
        {
            switch (_gameOne.gameState)
            {
                case Game1.GameState.TitleScreen:
                    DrawTitleUI();
                    break;
                case Game1.GameState.Game:
                    DrawGameUI();
                    break;
                case Game1.GameState.GameOver:
                    DrawGameOverUI();
                    break;
            }
        }

        void DrawTitleUI()
        {
            //Title background
            Globals.spriteBatch.Draw(_playerHealth[3],
                new Rectangle(Globals.screenSize.X / 2, Globals.screenSize.Y / 2, _playerHealth[3].Width * 5, _playerHealth[3].Height * 2),
                null,
                Color.White,
                0f,
                _origin,
                SpriteEffects.None,
                0f);

            //Start button
            Globals.spriteBatch.Draw(_playerHealth[3],
                new Rectangle(Globals.screenSize.X / 3, Globals.screenSize.Y / 4 * 3 + 50, (int)(_playerHealth[3].Width * 1.5f), _playerHealth[3].Height),
                null,
                Color.White,
                0f,
                _origin,
                SpriteEffects.None,
                0f);

            //Exit button
            Globals.spriteBatch.Draw(_playerHealth[3],
                new Rectangle(Globals.screenSize.X / 3 * 2, Globals.screenSize.Y / 4 * 3 + 50, (int)(_playerHealth[3].Width * 1.5f), _playerHealth[3].Height),
                null,
                Color.White,
                0f,
                _origin,
                SpriteEffects.None,
                0f);

            Globals.spriteBatch.DrawString(_titleFont,
                "Pizzas Express",
                new Vector2(Globals.screenSize.X / 2 - _titleFont.MeasureString("Pizzas Express").X / 2,
                Globals.screenSize.Y / 2 - _titleFont.MeasureString("Pizzas Express").Y / 2),
                _titleColour);

            Globals.spriteBatch.Draw(_debugPixel, _mouseRect, Color.White);
        }

        void DrawGameUI()
        {
            if (_gameOne.gameState == Game1.GameState.Game)
            {
                if (_playerVan._playerHealth == 3)
                {
                    Globals.spriteBatch.Draw(_playerHealth[2],
                        new Vector2(10, Globals.screenSize.Y - _playerHealth[2].Height - 10),
                        Color.White);
                }
                else if (_playerVan._playerHealth == 2)
                {
                    Globals.spriteBatch.Draw(_playerHealth[1],
                        new Vector2(10, Globals.screenSize.Y - _playerHealth[1].Height - 10),
                        Color.White);
                }
                else if (_playerVan._playerHealth == 1)
                {
                    Globals.spriteBatch.Draw(_playerHealth[0],
                       new Vector2(10, Globals.screenSize.Y - _playerHealth[0].Height - 10),
                       Color.White);
                }
                else
                {
                    Globals.spriteBatch.Draw(_playerHealth[3],
                       new Vector2(10, Globals.screenSize.Y - _playerHealth[0].Height - 10),
                       Color.White);
                }

                Globals.spriteBatch.Draw(_scoreBar,
                    new Rectangle(Globals.screenSize.X - (_scoreBar.Width * 2) - 10, Globals.screenSize.Y - _scoreBar.Height - 10,
                    280, _scoreBar.Height),
                    Color.White);

                Globals.spriteBatch.DrawString(_gameFont, "HEALTH",
                    new Vector2(10 + _gameFont.MeasureString("HEALTH").X / 4, Globals.screenSize.Y - 10 - (_playerHealth[2].Height + _gameFont.MeasureString("HEALTH").Y)),
                    Color.White);

                Globals.spriteBatch.DrawString(_gameFont, "SCORE",
                    new Vector2(Globals.screenSize.X - _scoreBar.Width - 50, Globals.screenSize.Y - _scoreBar.Height - 50),
                    Color.White);

                Globals.spriteBatch.DrawString(_gameFont, _playerScore.ToString(),
                    new Vector2(Globals.screenSize.X - _scoreBar.Width, Globals.screenSize.Y - _scoreBar.Height),
                    Color.White);
            }
        }

        void DrawGameOverUI()
        {

        }
    }
}
