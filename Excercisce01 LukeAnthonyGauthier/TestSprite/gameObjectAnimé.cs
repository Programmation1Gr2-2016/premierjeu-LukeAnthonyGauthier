using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestSprite
{
    class GameObjectAnime
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher;
        public enum etats { attenteDroite, attenteGauche, runDroite, runGauche };
        public etats objetState;
        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ
        int nbEtatRun = 6; //Combien il y a de rectangles pour l’état “courrir”
        public Rectangle[] tabRunDroite = {
            new Rectangle(60, 30, 65, 65),
            new Rectangle(130, 30, 65, 65),
            new Rectangle(193, 30, 65, 65),
            new Rectangle(260, 30, 65, 65),
            new Rectangle(320, 30, 65, 65),
            new Rectangle(385, 30, 65, 65) };
        public Rectangle[] tabRunGauche = {
            new Rectangle(60, 95, 65, 65),
            new Rectangle(130, 95, 65, 65),
            new Rectangle(193, 95, 65, 65),
            new Rectangle(260, 95, 65, 65),
            new Rectangle(320, 95, 65, 65),
            new Rectangle(385, 95, 65, 65) };
        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(194, 160, 65, 65)
        };


        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(194, 225, 65, 65)
        };
        

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }

            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }
    }
}
