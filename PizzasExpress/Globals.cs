using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace PizzasExpress
{
    internal class Globals
    {
        public static Point screenSize = new Point(1200, 600);

        public static Random rng = new Random();

        public static GraphicsDeviceManager graphics;

        public static SpriteBatch spriteBatch;
    }
}
