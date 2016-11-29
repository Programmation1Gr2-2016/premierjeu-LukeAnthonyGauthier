using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;

namespace Excercisce01_LukeAnthonyGauthier
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        KeyboardState keys = new KeyboardState();
        KeyboardState previousKeys = new KeyboardState();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Rectangle fenetre;
        GameObjectAnim heros;
        GameObject ennemie;
        GameObject[] tabBullet = new GameObject[100];
        Random Rnd = new Random();  
        SoundEffect gameover;
        SoundEffectInstance gameovers;
        SpriteFont font;

        bool amorceur = false;
        bool amorceur1 = false;
        int compteur = 0;
        string texte = "";
        string time = "";




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
            this.graphics.ApplyChanges();
            this.Window.Position = new Point(0, 0);

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

            scrolling1= new Scrolling (Content.Load<Texture2D>("backgrounds/background"), new Rectangle(0,0,2000,1200));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("backgrounds/background1."), new Rectangle(2000, 0, 2000, 1200));

            gameover = Content.Load<SoundEffect>("Sounds\\GameOver");
            gameovers = gameover.CreateInstance();

            font = Content.Load<SpriteFont>("font");


            Song song = Content.Load<Song>("Sounds\\maintrac");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);

            heros= new GameObjectAnim();
            heros.estVivant = true;
            heros.direction = Vector2.Zero;
            heros.vitesse.X = 2;
            heros.vitesse.Y = 2;
            heros.objetState = GameObjectAnim.etats.attenteDroite;
            heros.position = new Rectangle(544, 0, 46, 84);   //Position initiale de heroes
            heros.sprite = Content.Load<Texture2D>("heroe.png");


            ennemie = new GameObject();
            ennemie.estVivant = true;
            ennemie.vitesse = -4;
            ennemie.sprite = Content.Load<Texture2D>("Boss.png");
            ennemie.position = ennemie.sprite.Bounds;
            ennemie.position.X = 1600;
            ennemie.position.Y = 450;


            for (int i = 0; i <tabBullet.GetLength(0); i++)
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

            // background move 
            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
            {
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            }
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
            {
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;
            }
            scrolling1.Update();
            scrolling2.Update();
            Updatebullet(gameTime);
            UpdateEnnemi();
            UpdateHeros(gameTime);
            UpdateCollision();           

            base.Update(gameTime);
        }

        protected void UpdateHeros(GameTime gameTime)
        {
            keys = Keyboard.GetState();
            heros.position.X += (int)(heros.vitesse.X * heros.direction.X);
            heros.position.Y += (int)(heros.vitesse.Y * heros.direction.Y);
            //Movement
            if (keys.IsKeyDown(Keys.Right))
            {
                heros.direction.X = 2;
                heros.objetState = GameObjectAnim.etats.runDroite;
            }
            if (keys.IsKeyUp(Keys.Right) && previousKeys.IsKeyDown(Keys.Right))
            {
                heros.direction.X = 0;
                heros.objetState = GameObjectAnim.etats.attenteDroite;
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                heros.direction.X = -2;
                heros.objetState = GameObjectAnim.etats.runGauche;
            }
            if (keys.IsKeyUp(Keys.Left) && previousKeys.IsKeyDown(Keys.Left))
            {
                heros.direction.X = 0;
                heros.objetState = GameObjectAnim.etats.attenteGauche;
            }
            if (keys.IsKeyDown(Keys.Up))
            {
                heros.direction.Y = -2;
                heros.objetState = GameObjectAnim.etats.runHaut;
            }
            if (keys.IsKeyUp(Keys.Up) && previousKeys.IsKeyDown(Keys.Up))
            {
                heros.direction.Y = 0;
                heros.objetState = GameObjectAnim.etats.attenteHaut;
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                heros.direction.Y = 2;
                heros.objetState = GameObjectAnim.etats.runBas;
            }
            if (keys.IsKeyUp(Keys.Down) && previousKeys.IsKeyDown(Keys.Down))
            {
                heros.direction.Y = 0;
                heros.objetState = GameObjectAnim.etats.attenteBas;
            }
            //dextion de coter
            if (heros.position.X < fenetre.Left)
            {
                heros.position.X = fenetre.Left;
            }
            else if (heros.position.X + heros.spriteAfficher.Width > fenetre.Width)
            {
                heros.position.X = fenetre.Right - heros.spriteAfficher.Width;
            }
            else if (heros.position.Y + heros.spriteAfficher.Height > fenetre.Bottom)
            {
                heros.position.Y = fenetre.Top;
            }
            else if (heros.position.Y < fenetre.Top)
            {
                heros.position.Y = fenetre.Bottom - heros.spriteAfficher.Height;
            }

            heros.Update(gameTime);
            previousKeys = keys;

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
                
                for (int i = 0; i < tabBullet.GetLength(0); i++)
                {
                 

                    tabBullet[i].position.X += tabBullet[i].vitesse;

                    if ((tabBullet[i].position.X < fenetre.Left) && (compteur!=3)&&(tabBullet[i].estVivant==true))
                    {                    
                        tabBullet[i].estVivant = false;
                        compteur++;
                        //MegaPOULPE
                        if (compteur == 3)
                        {
                            tabBullet[i].estVivant = true;
                            tabBullet[i].vitesse = 2 ;
                            tabBullet[i].position.Height *= 2;
                            tabBullet[i].position.Width *= 2;                                          
                            compteur = 0;
                        }
                    }
                    if (tabBullet[i].position.X + tabBullet[i].sprite.Width > fenetre.Right)
                    {
                        tabBullet[i].estVivant = false;
                        amorceur1 = true;
                        if (amorceur1 == true)
                        {
                            tabBullet[i].position.Height = tabBullet[i].position.Height/2;
                            tabBullet[i].position.Width = tabBullet[i].position.Width/2;
                            amorceur1 = false;
                        }
                    }
                   
                }
                for (int i = 0; i < tabBullet.GetLength(0); i++)
                {
                    if ((Rnd.Next(1, 1000) == 1) && (tabBullet[i].estVivant == false))
                    {
                        tabBullet[i].estVivant = true;
                        tabBullet[i].position.X = ennemie.position.X;
                        tabBullet[i].position.Y = ennemie.position.Y;
                        tabBullet[i].vitesse = Rnd.Next(-7, -2);

                    }
                }
                                              
            }
            else
            {
                for (int i = 0; i < tabBullet.GetLength(0); i++)
                {
                    tabBullet[i].estVivant = false;
                }
            }
        }
        protected void UpdateCollision()
        { 
            
            for (int i = 0; i < tabBullet.GetLength(0); i++)
            {
                if (tabBullet[i].estVivant == true && heros.estVivant == true)
                {
                    if (heros.position.Intersects(tabBullet[i].position))
                    {
                        heros.estVivant = false;
                        amorceur = true;
                        if (amorceur== true)
                        {
                            MediaPlayer.Volume = 0.25F;
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

            
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);

            if (heros.estVivant == true)
            {
                spriteBatch.Draw(heros.sprite, heros.position, heros.spriteAfficher, Color.White);

            }
            else
            {
                
                if (amorceur == false)
                {
                    texte = "--- Game Over! ---\n  Survive: " + gameTime.TotalGameTime.Seconds + " Sec";
                    amorceur = true;
                }

                spriteBatch.DrawString(font, texte, new Vector2((fenetre.Width/2 - font.MeasureString(texte).X)+180, (fenetre.Height / 3 - font.MeasureString(texte).Y)), Color.White);
            }
            if (ennemie.estVivant == true)
            {
                spriteBatch.Draw(ennemie.sprite, ennemie.position, Color.White);
            }

            else
            {
                texte = "You WIN!";
                spriteBatch.DrawString(font, texte, new Vector2((fenetre.Width / 2 - font.MeasureString(texte).X), (fenetre.Height / 2 - font.MeasureString(texte).Y)), Color.White);
            }
            for (int i = 0; i < tabBullet.GetLength(0); i++)
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
// finir sprite en haut 