using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Excercisce01_LukeAnthonyGauthier
{
    class backgrounds
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public void Draw (SpriteBatch spriteBathc)
        {
            spriteBathc.Draw(texture, rectangle, Color.White);
        }
    }

    class Scrolling : backgrounds
    {
        public Scrolling(Texture2D newtexture, Rectangle newRectangle)
        {
            texture = newtexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 2;
        }       
    }

}
