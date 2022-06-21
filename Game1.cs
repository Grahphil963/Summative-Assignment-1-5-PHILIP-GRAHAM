using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Summative_Assignment_1_5_PHILIP_GRAHAM
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D introScreenTexture;
        
        SoundEffect carsSong;
        SoundEffectInstance carSongInstance;
        SoundEffect explosion;
        SoundEffectInstance explosionInstance;

        bool  explosionEffect= false;
        SpriteFont titleFont;
        Screen currentScreen;
        MouseState mouseState;
        Texture2D crashScreenTexture;
        Texture2D docHudsonTexture;
        Texture2D towMaterTexture;
        Rectangle towMaterRect;
        Rectangle docHudsonRect;
        Vector2 docHudsonSpeed;
        Vector2 towMaterSpeed;
        Texture2D explosionScreenTexture;
        Rectangle towMaterExitRect;
        Rectangle docHudsonExitRect;
        Vector2 towMaterExit;
        Vector2 docHudsonExit;
        Texture2D endScreenTexture;
        Texture2D bigRedTexture;
        SoundEffect crySound;
        SoundEffectInstance crySoundInstance;
        float seconds;
        float startTime;

        enum Screen
        {
            Intro,
            Crash,
            Explosion,
            End
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Cars crash";
            currentScreen = Screen.Intro;
            towMaterRect = new Rectangle(10, 200, 150, 200);
            towMaterSpeed = new Vector2(3, 0);
            docHudsonRect = new Rectangle(550, 200, 150, 200);
            docHudsonSpeed = new Vector2(-2, 0);
            towMaterExitRect = new Rectangle(10, 200, 150, 200);
            docHudsonExitRect = new Rectangle(550, 200, 150, 200);
            towMaterExit = new Vector2(0, -10);
            docHudsonExit = new Vector2(0, -10);
            

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            introScreenTexture = Content.Load<Texture2D>("introScreen");
            explosionScreenTexture = Content.Load<Texture2D>("explosion");
            carsSong = Content.Load<SoundEffect>("carsSong");
            carSongInstance = carsSong.CreateInstance();
            explosion = Content.Load<SoundEffect>("explosionSound");
            explosionInstance = explosion.CreateInstance();

            titleFont = Content.Load<SpriteFont>("title");
            crashScreenTexture  = Content.Load<Texture2D>("radiatorSprings");
            towMaterTexture = Content.Load<Texture2D>("towMater");
            docHudsonTexture = Content.Load<Texture2D>("docHudson");
            endScreenTexture = Content.Load<Texture2D>("endScreen");
            bigRedTexture = Content.Load<Texture2D>("bigRed");
            crySound = Content.Load<SoundEffect>("crySound");
            crySoundInstance = crySound.CreateInstance();  

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            


            if (currentScreen == Screen.Intro)
            {
                if (carSongInstance.State == SoundState.Stopped)
                    carSongInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed)
                    currentScreen = Screen.Crash;
            }
             if (currentScreen == Screen.Crash)
            {
                docHudsonRect.X += (int)docHudsonSpeed.X;
                towMaterRect.X += (int)towMaterSpeed.X;
                if (docHudsonRect.Intersects(towMaterRect))
                {
                    currentScreen = Screen.Explosion;
                    carSongInstance.Stop();

                }
                
            }
             if (currentScreen == Screen.Explosion)
            {
                docHudsonExitRect.Y += (int)docHudsonExit.Y;
                towMaterExitRect.Y += (int)towMaterExit.Y;
                if (explosionEffect == false)
                {
                    explosionInstance.Play();
                    
                    

                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                    currentScreen = Screen.End;
                if (currentScreen == Screen.End)
                {
                    explosionInstance.Stop();
                    crySoundInstance.Play();
                     
                }


            }
                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (currentScreen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(titleFont, ("Click to see next screen"), new Vector2(100, 25), Color.Blue);
            }

            if (currentScreen == Screen.Crash)
            {
                _spriteBatch.Draw(crashScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(towMaterTexture,towMaterRect, Color.White);
                _spriteBatch.Draw(docHudsonTexture,docHudsonRect, Color.White);
            }

            if (currentScreen == Screen.Explosion )
            {
                _spriteBatch.Draw(explosionScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(docHudsonTexture,docHudsonExitRect, Color.White);
                _spriteBatch.Draw(towMaterTexture,towMaterExitRect, Color.White);
                _spriteBatch.DrawString(titleFont, ("Click to see next Screen"), new Vector2(100, 25), Color.Blue);
            }

            if (currentScreen == Screen.End)
            {
                _spriteBatch.Draw(endScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.Draw(bigRedTexture, new Rectangle(550, 300, 150, 200), Color.White);
                _spriteBatch.DrawString(titleFont, ("R.I.P Doc man"), new Vector2(100, 25), Color.Blue);
                
            }
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
