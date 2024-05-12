using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace PizzasExpress
{
    internal class CarSpawner
    {
        int enemyHealth = 2;

        bool _isEnemy;

        float _spawnTimer = 0f;
        float _easySpawnTime = 6f;
        float _medSpawnTime = 4f;
        float _hardSpawnTime = 2f;

        Vector2 _position;
        Vector2 _spawnOffset = new Vector2(50, 0);

        Texture2D _carSheet;
        Texture2D _debugPixel;
        List<Rectangle> _enemysourceRects = new List<Rectangle>();
        List<Rectangle> _nonEnemysourceRects = new List<Rectangle>();
        List<Vector2> _spawnPositions = new List<Vector2>();
        public List<NonEnemyCars> _nonEnemyCars = new List<NonEnemyCars>();
        public List<EnemyCars> _enemyCars = new List<EnemyCars>();

        public CarSpawner(Texture2D carSheet, Texture2D debugPixel) 
        {
            _carSheet = carSheet;
            _debugPixel = debugPixel;

            _nonEnemysourceRects.Add(new Rectangle(10, 346, 45, 86));
            _nonEnemysourceRects.Add(new Rectangle(65, 346, 45, 86));
            _nonEnemysourceRects.Add(new Rectangle(122, 346, 45, 86));
            _nonEnemysourceRects.Add(new Rectangle(178, 346, 45, 86));
            _nonEnemysourceRects.Add(new Rectangle(7, 446, 51, 96));
            _nonEnemysourceRects.Add(new Rectangle(72, 446, 51, 96));
            _nonEnemysourceRects.Add(new Rectangle(137, 446, 51, 96));
            _nonEnemysourceRects.Add(new Rectangle(201, 446, 51, 96));

            _enemysourceRects.Add(new Rectangle(124, 226, 55, 103));
            _enemysourceRects.Add(new Rectangle(186, 226, 55, 103));
            _enemysourceRects.Add(new Rectangle(308, 223, 55, 106));

            _spawnPositions.Add(new Vector2(Globals.screenSize.X, 200));
            _spawnPositions.Add(new Vector2(0, 400));
        }

        public void UpdateCarSpawner(GameTime gameTime, PlayerVan playerVan)
        {
            if(_spawnTimer <= 0)
            {
                _isEnemy = Globals.rng.Next(0, 2) == 1;
                Vector2 spawnPoint = _spawnPositions[Globals.rng.Next(0, _spawnPositions.Count)];

                if(spawnPoint.Y < Globals.screenSize.Y / 2)
                {
                    if (!_isEnemy)
                    {
                        _nonEnemyCars.Add(new NonEnemyCars(_carSheet,
                            spawnPoint + _spawnOffset,
                            _nonEnemysourceRects[Globals.rng.Next(0, _nonEnemysourceRects.Count)],
                            -90f,
                            _debugPixel)); 
                    }
                    else
                    {
                        _enemyCars.Add(new EnemyCars(_carSheet,
                            spawnPoint - _spawnOffset,
                            _enemysourceRects[Globals.rng.Next(0, _enemysourceRects.Count)],
                            -90,
                            _debugPixel));
                    }
                }
                else if(spawnPoint.Y > Globals.screenSize.Y / 2)
                {
                    if (!_isEnemy)
                    {
                        _nonEnemyCars.Add(new NonEnemyCars(_carSheet,
                            spawnPoint + _spawnOffset,
                            _nonEnemysourceRects[Globals.rng.Next(0, _nonEnemysourceRects.Count)],
                            90f, _debugPixel)); 
                    }
                    else
                    {
                        _enemyCars.Add(new EnemyCars(_carSheet,
                            spawnPoint - _spawnOffset,
                            _enemysourceRects[Globals.rng.Next(0, _enemysourceRects.Count)],
                            9090,
                            _debugPixel));
                    }
                }

                _spawnTimer = _hardSpawnTime;
            }
            else
            {
                _spawnTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            for(int i = 0; i < _nonEnemyCars.Count; i++)
            {
                _nonEnemyCars[i].UpdateNonEnemyCars();
            }

            for(int i = 0; i < _nonEnemyCars.Count; ++i)
            {
                if (_nonEnemyCars[i]._position.Y == _spawnPositions[0].Y &&
                    _nonEnemyCars[i]._position.X < -100)
                {
                    _nonEnemyCars.Remove(_nonEnemyCars[i]);
                }
                else if(_nonEnemyCars[i]._position.Y == _spawnPositions[1].Y &&
                    _nonEnemyCars[i]._position.X > Globals.screenSize.X + 100)
                {
                    _nonEnemyCars.Remove(_nonEnemyCars[i]);
                }
            }

            for (int i = 0; i < _enemyCars.Count; i++)
            {
                _enemyCars[i].UpdateEnemyCars(playerVan);
            }

            for(int i = 0; i < _enemyCars.Count; i++)
            {
                if (_enemyCars[i]._colRect.Intersects(playerVan._colRect))
                {
                    _enemyCars.Remove(_enemyCars[i]);
                    playerVan._playerHealth--;
                }
            }

            for (int i = 0; i < playerVan._bullets.Count; i++)
            {
                for (int j = 0; j < _enemyCars.Count; j++)
                {
                    if (_enemyCars[j]._colRect.Intersects(playerVan._bullets[i]._colRect))
                    {
                        if(enemyHealth > 0)
                        {
                            enemyHealth--;
                            playerVan._bullets.Remove(playerVan._bullets[i]);
                        }
                        else
                        {
                            playerVan._bullets.Remove(playerVan._bullets[i]);
                            _enemyCars.Remove(_enemyCars[j]);
                        }
                    } 
                }
            }

            //Debug.WriteLine(_nonEnemyCars.Count);
        }

        public void DrawNonEnemyCars()
        {
            foreach(NonEnemyCars eachCar in _nonEnemyCars)
            {
                eachCar.DrawNonEnemyCars();
            }

            foreach (EnemyCars eachEnemyCar in _enemyCars)
            {
                eachEnemyCar.DrawEnemyCars();
            }
        }
    }
}
