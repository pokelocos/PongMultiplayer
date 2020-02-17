using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class FontManager
    {
        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        public static void Register(string filename, SpriteFont font)
        {
            fonts.Add(filename, font);
        }

        public static SpriteFont Get(String name)
        {
            try
            {
                return fonts[name];
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
        }
    }
}


