using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Excercisce01_LukeAnthonyGauthier
{
    class GameObjectAnim
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher;
        public enum etats { attenteDroite, attenteGauche, attenteHaut, attenteBas, runDroite, runGauche,runHaut,runBas };
        public etats objetState;
        public bool estVivant;
        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ
        int nbEtatRun = 3; //Combien il y a de rectangles pour l’état “courrir”
        public Rectangle[] tabRunDroite = {
            new Rectangle(544, 0, 46, 84),
            new Rectangle(677, 0, 42, 83),
            new Rectangle(809, 0, 42, 81) };
        public Rectangle[] tabRunGauche = {
            new Rectangle(410, 0, 46, 83),
            new Rectangle(279, 0, 43, 84),
            new Rectangle(147, 0, 42, 81)};        
        public Rectangle[] tabRunBas= {
            new Rectangle(391, 273, 82, 68),
            new Rectangle(259, 282, 82, 50),
            new Rectangle(129, 282, 79, 50) };
        public Rectangle[] tabRunHaut= {
            new Rectangle(525, 273, 82, 68),
            new Rectangle(652, 280, 82, 50),
            new Rectangle(791, 280, 79, 50) };
        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(544, 0, 46, 84)
        };
        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(410, 0, 46, 84)
        };
        public Rectangle[] tabAttenteHaut =
        {
            new Rectangle(525, 273, 82, 68)
        };
        public Rectangle[] tabAttenteBas =
        {
            new Rectangle(391, 273, 82, 68)
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
            if (objetState == etats.attenteHaut)
            {
                spriteAfficher = tabAttenteHaut[waitState];
            }
            if (objetState == etats.attenteBas)
            {
                spriteAfficher = tabAttenteBas[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }
            if (objetState == etats.runBas)
            {
                spriteAfficher = tabRunBas[runState];
            }
            if (objetState == etats.runHaut)
            {
                spriteAfficher = tabRunHaut[runState];
            }

            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 10) //Vitesse défilement
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
