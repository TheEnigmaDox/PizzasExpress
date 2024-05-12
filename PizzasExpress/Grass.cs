using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class Grass
    {
        public Vector2 _position;

        public Texture2D _grassTexture;
        public Texture2D _debugPixel;
        public Grass(Texture2D grassTexture, Vector2 position, Texture2D debugPixel)
        {
            _grassTexture = grassTexture;
            _position = position;
            _debugPixel = debugPixel;
        }

        public void DrawGrass()
        {
            Globals.spriteBatch.Draw(_grassTexture,
                new Rectangle((int)_position.X, (int)_position.Y,
                _grassTexture.Width, /*_grassTexture.Height*/ Globals.screenSize.Y ),
                null,
                Color.White,
                0f,
                new Vector2(_grassTexture.Width / 2, _grassTexture.Height / 2),
                SpriteEffects.None,
                0f);

            //Globals.spriteBatch.Draw(_debugPixel,
            //    new Rectangle((int)_position.X + _grassTexture.Width / 2, (int)_position.Y, -10, 10),
            //    Color.White);
        }
    }
}
