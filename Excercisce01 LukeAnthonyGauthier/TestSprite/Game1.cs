using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestSprite
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        GameObjectAnime rambo;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight= graphics.GraphicsDevice.DisplayMode.Height;

            Window.Position = new Point(0, 0);
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
       
            rambo = new GameObjectAnime();
            rambo.direction = Vector2.Zero;
            rambo.vitesse.X = 2;
            rambo.objetState = GameObjectAnime.etats.attenteDroite;
            rambo.position = new Rectangle(350, 250, 65, 65);   //Position initiale de Rambo
            rambo.sprite = Content.Load<Texture2D>("Rambo.png");
               
       }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keys = Keyboard.GetState();
            rambo.position.X += (int)(rambo.vitesse.X * rambo.direction.X);


            if (keys.IsKeyDown(Keys.Right))
            {
                rambo.direction.X = 2;
                rambo.objetState = GameObjectAnime.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                rambo.direction.X = 0;
                rambo.objetState = GameObjectAnime.etats.attenteDroite;
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                rambo.direction.X = -2;
                rambo.objetState = GameObjectAnime.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                rambo.direction.X = 0;
                rambo.objetState = GameObjectAnime.etats.attenteGauche;
            }
            rambo.Update(gameTime);
            previousKeys = keys;

            base.Update(gameTime);
        }
     
           


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
          
            spriteBatch.Begin();
            spriteBatch.Draw(rambo.sprite, rambo.position, rambo.spriteAfficher, Color.White);
            spriteBatch.End();
         


            base.Draw(gameTime);
        }
    }
}
