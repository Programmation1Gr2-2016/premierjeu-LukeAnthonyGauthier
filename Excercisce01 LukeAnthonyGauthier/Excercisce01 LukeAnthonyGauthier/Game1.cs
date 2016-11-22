using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace Excercisce01_LukeAnthonyGauthier
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle fenetre;
        GameObject heros;
        GameObject ennemie;
        GameObject[] tabBullet = new GameObject[40];
        Random Rnd = new Random();
        Texture2D background;    
        SoundEffect gameover;
        SoundEffectInstance gameovers;
        SpriteFont font;

        bool amorceur = false;
        int compteur = 0;


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
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            this.graphics.ToggleFullScreen();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here
            fenetre = graphics.GraphicsDevice.Viewport.Bounds;
            fenetre.Width = graphics.GraphicsDevice.DisplayMode.Width;
            fenetre.Height = graphics.GraphicsDevice.DisplayMode.Height;

            background= Content.Load<Texture2D>("Background.png");

            gameover = Content.Load<SoundEffect>("Sounds\\GameOver");
            gameovers = gameover.CreateInstance();

            font = Content.Load<SpriteFont>("font");


            Song song = Content.Load<Song>("Sounds\\maintrac");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            heros = new GameObject();
            heros.estVivant = true;
            heros.vitesse = 5;
            heros.sprite = Content.Load<Texture2D>("01.png");
            heros.position = heros.sprite.Bounds;
            heros.position.X = 50;
            heros.position.Y = 50;

            ennemie = new GameObject();
            ennemie.estVivant = true;
            ennemie.vitesse = -4;
            ennemie.sprite = Content.Load<Texture2D>("Boss.png");
            ennemie.position = ennemie.sprite.Bounds;
            ennemie.position.X = 1600;
            ennemie.position.Y = 450;


            for (int i = 0; i < 40; i++)
            {
                tabBullet[i] = new GameObject();
                tabBullet[i].estVivant = false;
                tabBullet[i].sprite = Content.Load<Texture2D>("Projectille.png");
                tabBullet[i].position = tabBullet[i].sprite.Bounds;
                tabBullet[i].position.X = ennemie.position.X;
                tabBullet[i].position.Y = ennemie.position.Y; 
                       
            }


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

            // TODO: Add your update logic here

            //Hero mouvemant 
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                heros.position.X += heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                heros.position.X -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                heros.position.Y -= heros.vitesse;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                heros.position.Y += heros.vitesse;
            }

            UpdateHeros();
            UpdateEnnemi();
            UpdateCollision();
            Updatebullet(gameTime);

            base.Update(gameTime);
        }

        protected void UpdateHeros()
        {
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Right - heros.sprite.Width;
            }
            else if (heros.position.X + heros.sprite.Width > fenetre.Right)
            {
                heros.position.X = fenetre.Left;
            }
            else if (heros.position.Y + heros.sprite.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Top;
            }
            else if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Bottom - heros.sprite.Height;
            }
            for (int i = 0; i < 40; i++)
            {
                if (heros.position.Intersects(tabBullet[i].sprite.Bounds))
                {
                    heros.estVivant = false;
                }
            }

        }
        protected void UpdateEnnemi()
        {
            ennemie.position.Y += ennemie.vitesse;

            if (ennemie.position.Y + ennemie.sprite.Height > fenetre.Bottom)
            {
                ennemie.vitesse = -4;
            }
            if (ennemie.position.Y < fenetre.Top)
            {
                ennemie.vitesse = +4;
            }
        }
        protected void Updatebullet(GameTime gameTime)
        {
            if (ennemie.estVivant == true)
            {
                for (int i = 0; i < 40; i++)
                {
                    tabBullet[i].position.X += (tabBullet[i].vitesse);

                    if (tabBullet[i].position.X < fenetre.Left && compteur!=3)
                    {                    
                        tabBullet[i].estVivant = false;
                        compteur++;
                        if (compteur == 3)
                        {
                            tabBullet[i].position.X = tabBullet[i].vitesse*-1;
                            //tabBullet[i].position.Height *= 2;
                            //tabBullet[i].position.Width *= 2;
                            compteur = 0;
                        }
                    }                    
                }
                for (int i = 0; i < 40; i++)
                {

                    if ((Rnd.Next(1, 1000) == 1) && (tabBullet[i].estVivant == false))
                    {
                        tabBullet[i].estVivant = true;
                        tabBullet[i].position.X = ennemie.position.X;
                        tabBullet[i].position.Y = ennemie.position.Y;
                        tabBullet[i].vitesse = Rnd.Next(-6, -2);
                    }
                }
            }
            else
            {
                for (int i = 0; i < 40; i++)
                {
                    tabBullet[i].estVivant = false;
                }
            }
        }
        protected void UpdateCollision()
        { 
            
            for (int i = 0; i < 40; i++)
            {
                if (tabBullet[i].estVivant == true && heros.estVivant == true)
                {
                    if (heros.position.Intersects(tabBullet[i].position))
                    {
                        heros.estVivant = false;
                        amorceur = true;
                        if (amorceur== true)
                        {
                            gameovers.Play();
                            amorceur = false;
                        }                          
                    }
                }
            }
            if (heros.estVivant == true)
            {
                if (ennemie.position.Intersects(heros.position))
                {
                    ennemie.estVivant = false;
                }
            }
        }
      
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(background,fenetre, Color.White);

            

            if (heros.estVivant == true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, Color.White);
            }
            else
            {
              spriteBatch.DrawString(font, "Game OVER!", new Vector2(1000, 500), Color.Black);
            }
            if (ennemie.estVivant == true)
            {
                spriteBatch.Draw(ennemie.sprite, ennemie.position, Color.White);
            }
            
            else
            {
                spriteBatch.DrawString(font, "You WIN!", new Vector2(1000, 500), Color.Black);
            }
            for (int i = 0; i < 40; i++)
            {
                if (tabBullet[i].estVivant == true)
                {                   
                    spriteBatch.Draw(tabBullet[i].sprite, tabBullet[i].position, Color.White);
                }
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
