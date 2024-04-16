using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    internal class RoadManager
    {
        float _moveSpeed = 3f;

        List<Road> _roads = new List<Road>();

        public RoadManager(List<Road> roads) 
        {
            _roads = roads;
        }

        public void UpdateRoadManager()
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
        }

        public void UpdateRoadDraw()
        {
            for(int i = 0; i < _roads.Count; i++)
            {
                _roads[i].DrawRoads();
            }
        }
    }
}
