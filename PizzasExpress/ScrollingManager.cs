using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class ScrollingManager
    {
        float _moveSpeed = 3f;

        List<Road> _roads = new List<Road>();
        List<Grass> _grass = new List<Grass>();

        public ScrollingManager(List<Road> roads, List<Grass> grass) 
        {
            _roads = roads;
            _grass = grass;
        }

        public void UpdateScrollingManager()
        {
            for (int i = 0; i < _roads.Count; i++)
            {
                _roads[i]._position.X -= _moveSpeed;
            }

            if (_roads[0]._position.X + _roads[0]._roadTexture.Width / 2 < 0)
            {
                _roads[0]._position.X = _roads[1]._position.X + _roads[1]._roadTexture.Width;
            }

            if (_roads[1]._position.X + _roads[1]._roadTexture.Width / 2 < 0)
            {
                _roads[1]._position.X = _roads[0]._position.X + _roads[0]._roadTexture.Width;
            }

            for (int i = 0; i < _grass.Count; i++)
            {
                _grass[i]._position.X -= _moveSpeed;
            }

            if (_grass[0]._position.X + _grass[0]._grassTexture.Width / 2 < 0)
            {
                _grass[0]._position.X = _grass[1]._position.X + _grass[1]._grassTexture.Width;
            }

            if (_grass[1]._position.X + _grass[1]._grassTexture.Width / 2 < 0)
            {
                _grass[1]._position.X = _grass[0]._position.X + _grass[0]._grassTexture.Width;
            }

        }

        public void UpdateScrollDraw()
        {
            for (int i = 0; i < _grass.Count; i++)
            {
                _grass[i].DrawGrass();
            }

            for (int i = 0; i < _roads.Count; i++)
            {
                _roads[i].DrawRoads();
            }
        }
    }
}
