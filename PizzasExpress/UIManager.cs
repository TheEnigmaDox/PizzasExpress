using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class UIManager
    {
        Texture2D _scoreBar;
        List<Texture2D> _playerHealth = new List<Texture2D>();

        SpriteFont _gameFont;

        PlayerVan _playerVan;

        public UIManager(List<Texture2D> playerHealth, Texture2D scoreBar, SpriteFont gameFont) 
        {
            _playerHealth = playerHealth;

            _gameFont = gameFont;

            _scoreBar = scoreBar;   
        }

        public void Update(PlayerVan player)
        {
            _playerVan = player;
        }

        public void DrawUI()
        {
            if (_playerVan._playerHealth == 3)
            {
                Globals.spriteBatch.Draw(_playerHealth[2],
                    new Vector2(10, Globals.screenSize.Y - _playerHealth[2].Height - 10),
                    Color.White);
            }
            else if(_playerVan._playerHealth == 2)
            {
                Globals.spriteBatch.Draw(_playerHealth[1],
                    new Vector2(10, Globals.screenSize.Y - _playerHealth[1].Height - 10),
                    Color.White);
            }
            else if(_playerVan._playerHealth == 1)
            {
                Globals.spriteBatch.Draw(_playerHealth[0],
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
        }
    }
}
