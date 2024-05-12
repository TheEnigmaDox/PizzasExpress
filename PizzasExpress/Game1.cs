﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PizzasExpress
{
    public class Game1 : Game
    {
        Texture2D carSheet;
        Texture2D roadTexture;
        Texture2D debugPixel;
        Texture2D pizzaBullet;
        Texture2D backgroundGrass;

        List<Road> roadList = new List<Road>();
        List<Grass> grassList = new List<Grass>();
        List<Texture2D> playerHealth = new List<Texture2D>();

        ScrollingManager roadManager;
        PlayerVan playerVan;
        CarSpawner carSpawner;
        UIManager uiManager;

        SpriteFont gameFont;

        GamePadState gamePadState;
        KeyboardState keyboardState;
        MouseState mouseState;

        public Game1()
        {
            Globals.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Globals.graphics.PreferredBackBufferWidth = Globals.screenSize.X;
            Globals.graphics.PreferredBackBufferHeight = Globals.screenSize.Y;
            Globals.graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            carSheet = Content.Load<Texture2D>("Textures/CarSheet");
            debugPixel = Content.Load<Texture2D>("Textures/DebugPixel");
            roadTexture = Content.Load<Texture2D>("Textures/Road");
            pizzaBullet = Content.Load<Texture2D>("Textures/PizzaSprites/PizzaBullet");
            backgroundGrass = Content.Load<Texture2D>("Textures/Grass");
            //gameFont = Content.Load<SpriteFont>("Fonts/GameFont");

            grassList.Add(new Grass(backgroundGrass, new Vector2(0, Globals.screenSize.Y / 2), debugPixel));
            grassList.Add(new Grass(backgroundGrass, new Vector2(grassList[0]._position.X + grassList[0]._grassTexture.Width,
                Globals.screenSize.Y / 2), debugPixel));
            grassList.Add(new Grass(backgroundGrass, new Vector2(grassList[1]._position.X + grassList[1]._grassTexture.Width,
                Globals.screenSize.Y / 2), debugPixel));
            grassList.Add(new Grass(backgroundGrass, new Vector2(grassList[2]._position.X + grassList[2]._grassTexture.Width,
                Globals.screenSize.Y / 2), debugPixel));

            roadList.Add(new Road(roadTexture, new Vector2(roadTexture.Width / 2, Globals.screenSize.Y / 2), debugPixel));
            roadList.Add(new Road(roadTexture, new Vector2(roadList[0]._position.X + roadList[0]._roadTexture.Width,
                Globals.screenSize.Y / 2), debugPixel));

            playerHealth.Add(Content.Load<Texture2D>("Textures/PlayerHealth/PlayerHealth00"));
            playerHealth.Add(Content.Load<Texture2D>("Textures/PlayerHealth/PlayerHealth01"));
            playerHealth.Add(Content.Load<Texture2D>("Textures/PlayerHealth/PlayerHealth02"));

            // TODO: use this.Content to load your game content here

            roadManager = new ScrollingManager(roadList, grassList);
            playerVan = new PlayerVan(carSheet,
                debugPixel,
                new Vector2(Globals.screenSize.X / 3, Globals.screenSize.Y / 2),
                new Rectangle(247, 123, 52, 92),
                pizzaBullet);

            carSpawner = new CarSpawner(carSheet,debugPixel);

            uiManager = new UIManager(playerHealth, Content.Load<Texture2D>("Textures/ScoreBar"),
                Content.Load<SpriteFont>("Fonts/GameFont"));            
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gamePadState = GamePad.GetState(PlayerIndex.One);
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            // TODO: Add your update logic here

            roadManager.UpdateScrollingManager();
            playerVan.UpdatePlayerVan(gameTime, gamePadState, keyboardState, mouseState, carSpawner._nonEnemyCars);
            carSpawner.UpdateCarSpawner(gameTime, playerVan);
            uiManager.Update(playerVan);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Globals.spriteBatch.Begin();

            roadManager.UpdateScrollDraw();
            playerVan.DrawPlayerVan();
            carSpawner.DrawNonEnemyCars();

            uiManager.DrawUI();

            Globals.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
