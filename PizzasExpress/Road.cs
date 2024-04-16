using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class Road
    {
        public Vector2 _position;

        public Texture2D _roadTexture;
        public Texture2D _debugPixel;

        public Road(Texture2D roadTexture, Vector2 position, Texture2D debugPixel)
        {
            _roadTexture = roadTexture;
            _position = position;
            _debugPixel = debugPixel;
        }

        public void DrawRoads()
        {
            Globals.spriteBatch.Draw(_roadTexture,
                new Rectangle((int)_position.X, (int)_position.Y, _roadTexture.Width, _roadTexture.Height),
                null,
                Color.White,
                0f,
                new Vector2(_roadTexture.Width / 2, _roadTexture.Height / 2),
                SpriteEffects.None,
                0f);

            Globals.spriteBatch.Draw(_debugPixel,
                new Rectangle((int)_position.X + _roadTexture.Width / 2, (int)_position.Y, -10, 10),
                Color.White);
        }
    }
}
