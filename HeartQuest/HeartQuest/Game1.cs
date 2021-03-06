﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace HeartQuest
{
    public class Game1 : Game
    {
        public static Vector2 Gravity = new Vector2(0, 100.0f);
        World world;
        GameState gameState = GameState.TITLE_SCREEN;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // textures for menus
        public static Texture2D menuCorner;
        public static Texture2D menuBar;
        public static Texture2D menuCenter;
        public static Texture2D textCorner;
        public static Texture2D textBar;
        public static Texture2D textCenter;

        public static Rectangle screenBounds;

        //fonts
        public static SpriteFont font144;
        public static SpriteFont font72;
        public static SpriteFont font16;
        public static SpriteFont font12;
        public static SpriteFont font10;

        public static Texture2D[] bossImages;

        // flower pot images
        public static Texture2D[] flowerImages;

        // menus and text boxes for screens
        Menu titleMenu;
        TextDisplayBox titleBox;
        Menu infoMenu;
        TextDisplayBox infoBox;
        Menu victoryMenu;
        TextDisplayBox victoryBox;
        Menu gameoverMenu;

        // misc text boxes
        TextDisplayBox gameoverBox;
        TextDisplayBox thankYou;
        TextDisplayBox credits;

        // backgrounds
        Texture2D titleBG;
        Texture2D gameoverBG;
        Texture2D victoryBG;
        Texture2D gameBG;

        // world images
        Texture2D[] playerImages;
        Texture2D[] tileImages;
        Texture2D healthBack;
        Texture2D healthFront;
        public static Texture2D blackRect;


        // sounds
        public static SoundEffectInstance bossLoop;
        public static SoundEffectInstance roamLoop;
        public static SoundEffectInstance menuLoop;

        // static sounds
        public static SoundEffect jump;
        public static SoundEffect death;
        public static SoundEffect hurt;
        public static SoundEffect menuSelection;
        public static SoundEffect potBreak;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            InputManager.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font144 = Content.Load<SpriteFont>("source-sans-144");
            font72 = Content.Load<SpriteFont>("source-sans-72");
            font16 = Content.Load<SpriteFont>("source-sans-16");
            font12 = Content.Load<SpriteFont>("source-sans-12");
            font10 = Content.Load<SpriteFont>("source-sans-10");

            titleBG = Content.Load<Texture2D>("BackgroundGradRed");
            gameoverBG = Content.Load<Texture2D>("BackgroundBlack");
            victoryBG = Content.Load<Texture2D>("BackgroundRed");

            menuCorner = Content.Load<Texture2D>("MenuCorner");
            menuBar = Content.Load<Texture2D>("MenuSide");
            menuCenter = Content.Load<Texture2D>("Center");

            textCorner = Content.Load<Texture2D>("MenuCorner");
            textBar = Content.Load<Texture2D>("MenuSide");
            textCenter = Content.Load<Texture2D>("Center");

            string[] t = { "Start Game", "Info Screen", "Exit" };
            titleMenu = new Menu(t, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t2 = { "Heart Quest" };
            titleBox = new TextDisplayBox(t2, new Vector2(40, 50), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t5 = { "Return to title", "Exit" };
            infoMenu = new Menu(t5, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t6 = { "Made the weekend of April 30, 2016, at a 48 hr Neumont University Game Jam", "Created by Justin Furtado and Devin Duren", "Controls: w, a, d, to move, (s does nothing), e to punch, space bar to interact" };
            infoBox = new TextDisplayBox(t6, new Vector2(40, 50), textCorner, textBar, textCenter, 45, 10, font16);

            string[] t7 = { "Return to title", "Exit" };
            victoryMenu = new Menu(t7, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t8 = { "Congratulations!" };
            victoryBox = new TextDisplayBox(t8, new Vector2(40, 10), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t9 = { "Return to title", "Exit" };
            gameoverMenu = new Menu(t9, new Vector2(240, 240), menuCorner, menuBar, menuCenter, 20, 10, font16);

            string[] t10 = { "GAME OVER!" };
            gameoverBox = new TextDisplayBox(t10, new Vector2(40, 10), textCorner, textBar, textCenter, 45, 10, font72);

            string[] t11 = { "Art, programming and sound by Justin Furtado and Devin Duren" };
            credits = new TextDisplayBox(t11, new Vector2(40, 416), textCorner, textBar, textCenter, 45, 4, font16);

            string[] t12 = { "Thanks for playing, we hope you enjoyed Heart Quest" };
            thankYou = new TextDisplayBox(t12, new Vector2(40, 170), textCorner, textBar, textCenter, 45, 4, font16);

            playerImages = new Texture2D[12];
            playerImages[0] = Content.Load<Texture2D>("PlayerSprite");
            playerImages[1] = Content.Load<Texture2D>("PlayerSpriteWalk");

            playerImages[2] = Content.Load<Texture2D>("PlayerSpriteLeft");
            playerImages[3] = Content.Load<Texture2D>("PlayerSpriteLeftWalk");

            playerImages[4] = Content.Load<Texture2D>("PlayerSpriteBlack");
            playerImages[5] = Content.Load<Texture2D>("PlayerSpriteWalkBlack");

            playerImages[6] = Content.Load<Texture2D>("PlayerSpriteLeftBlack");
            playerImages[7] = Content.Load<Texture2D>("PlayerSpriteLeftWalkBlack");

            playerImages[8] = Content.Load<Texture2D>("PlayerSpritePunch");
            playerImages[9] = Content.Load<Texture2D>("PlayerSpriteLeftPunch");

            playerImages[10] = Content.Load<Texture2D>("PlayerSpriteBlackPunch");
            playerImages[11] = Content.Load<Texture2D>("PlayerSpriteLeftBlackPunch");


            bossImages = new Texture2D[12];
            bossImages[0] = Content.Load<Texture2D>("BossSprite");
            bossImages[1] = Content.Load<Texture2D>("BossSpriteWalk");
            bossImages[2] = Content.Load<Texture2D>("BossSpriteLeft");
            bossImages[3] = Content.Load<Texture2D>("BossSpriteLeftWalk");
            bossImages[4] = Content.Load<Texture2D>("BossSpriteBlack");
            bossImages[5] = Content.Load<Texture2D>("BossSpriteWalkBlack");
            bossImages[6] = Content.Load<Texture2D>("BossSpriteLeftBlack");
            bossImages[7] = Content.Load<Texture2D>("BossSpriteLeftWalkBlack");
            bossImages[8] = Content.Load<Texture2D>("BossSpritePunch");
            bossImages[9] = Content.Load<Texture2D>("BossSpriteLeftPunch");
            bossImages[10] = Content.Load<Texture2D>("BossSpriteBlackPunch");
            bossImages[11] = Content.Load<Texture2D>("BossSpriteLeftBlackPunch");


            healthBack = Content.Load<Texture2D>("HealthBarBackground");
            healthFront = Content.Load<Texture2D>("HealthBarStatus");

            tileImages = new Texture2D[3];
            tileImages[0] = Content.Load<Texture2D>("BaseTile"); // grass
            tileImages[1] = Content.Load<Texture2D>("TreeSection"); // trunks
            tileImages[2] = Content.Load<Texture2D>("TreeTops"); // leaves/branches

            flowerImages = new Texture2D[3];
            flowerImages[0] = Content.Load<Texture2D>("Pot");
            flowerImages[1] = Content.Load<Texture2D>("Flower");
            flowerImages[2] = Content.Load<Texture2D>("CrushedPot");

            menuLoop = (Content.Load<SoundEffect>("title1")).CreateInstance();
            bossLoop = (Content.Load<SoundEffect>("bossv1")).CreateInstance();
            roamLoop = (Content.Load<SoundEffect>("roam1")).CreateInstance();

            menuLoop.IsLooped = true;
            menuLoop.Play();
            bossLoop.IsLooped = true;
            roamLoop.IsLooped = true;

            death = Content.Load<SoundEffect>("death");
            menuSelection = Content.Load<SoundEffect>("menuselect");
            hurt = Content.Load<SoundEffect>("hurt");
            potBreak = Content.Load<SoundEffect>("potbreak1");
            jump = Content.Load<SoundEffect>("jump");

            blackRect = Content.Load<Texture2D>("rect");

            screenBounds = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            gameBG = Content.Load<Texture2D>("Forest");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            menuLoop.Dispose();
            bossLoop.Dispose();
            roamLoop.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            switch (gameState)
            {
                case GameState.TITLE_SCREEN:
                    InputManager.Update(gameTime);
                    titleMenu.ProcessInput();

                    if (titleMenu.IsOver)
                    {
                        if (titleMenu.Selection == 0)
                        {
                            gameState = GameState.PLAYING;
                            world = new World(tileImages, playerImages, healthBack, healthFront);
                            menuLoop.Stop();
                            roamLoop.Play();
                        }
                        else if (titleMenu.Selection == 1)
                        {
                            gameState = GameState.INFO_SCREEN;
                            infoMenu.ResetMenu();
                        }
                        else if (titleMenu.Selection == 2)
                        {
                            menuLoop.Stop();
                            Exit();
                        }
                    }
                    break;

                case GameState.PLAYING:
                    InputManager.Update(gameTime);
                    world.Update(gameTime);

                    if (world.IsPlayerDead)
                    {
                        death.Play();
                        gameState = GameState.GAMEOVER_SCREEN;
                        gameoverMenu.ResetMenu();
                        bossLoop.Stop();
                        menuLoop.Play();
                    }

                    if (world.IsBossDead)
                    {
                        gameState = GameState.VICTORY_SCREEN;
                        victoryMenu.ResetMenu();
                        // todo replace w/ boss music
                        bossLoop.Stop();
                        menuLoop.Play();
                    }
                    break;

                case GameState.INFO_SCREEN:
                    InputManager.Update(gameTime);
                    infoMenu.ProcessInput();

                    if (infoMenu.IsOver)
                    {
                        if (infoMenu.Selection == 0)
                        {
                            gameState = GameState.TITLE_SCREEN;
                            titleMenu.ResetMenu();
                        }
                        else if (infoMenu.Selection == 1)
                        {
                            menuLoop.Stop();
                            Exit();
                        }

                    }
                    break;

                case GameState.VICTORY_SCREEN:
                    InputManager.Update(gameTime);
                    victoryMenu.ProcessInput();

                    if (victoryMenu.IsOver)
                    {
                        if (victoryMenu.Selection == 0)
                        {
                            gameState = GameState.TITLE_SCREEN;
                            titleMenu.ResetMenu();
                        }
                        else if (victoryMenu.Selection == 1)
                        {
                            menuLoop.Stop();
                            Exit();
                        }
                    }
                    break;

                case GameState.GAMEOVER_SCREEN:
                    InputManager.Update(gameTime);
                    gameoverMenu.ProcessInput();

                    if (gameoverMenu.IsOver)
                    {
                        if (gameoverMenu.Selection == 0)
                        {
                            gameState = GameState.TITLE_SCREEN;
                            titleMenu.ResetMenu();
                        }
                        else if (gameoverMenu.Selection == 1)
                        {
                            menuLoop.Stop();
                            Exit();
                        }
                    }
                    break;

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.TITLE_SCREEN:
                    spriteBatch.Draw(titleBG, screenBounds, Color.White);
                    titleBox.Draw(spriteBatch);
                    titleMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;

                case GameState.PLAYING:
                    spriteBatch.Draw(gameBG, screenBounds, Color.White);
                    world.Draw(spriteBatch);
                    break;

                case GameState.INFO_SCREEN:
                    spriteBatch.Draw(titleBG, screenBounds, Color.White);
                    infoBox.Draw(spriteBatch);
                    infoMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    break;

                case GameState.VICTORY_SCREEN:
                    spriteBatch.Draw(victoryBG, screenBounds, Color.White);
                    victoryBox.Draw(spriteBatch);
                    victoryMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    thankYou.Draw(spriteBatch);
                    break;

                case GameState.GAMEOVER_SCREEN:
                    spriteBatch.Draw(gameoverBG, screenBounds, Color.White);
                    gameoverBox.Draw(spriteBatch);
                    gameoverMenu.Draw(spriteBatch);
                    credits.Draw(spriteBatch);
                    thankYou.Draw(spriteBatch);
                    break;

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

enum GameState
{
    TITLE_SCREEN,
    PLAYING,
    GAMEOVER_SCREEN,
    VICTORY_SCREEN,
    INFO_SCREEN
}