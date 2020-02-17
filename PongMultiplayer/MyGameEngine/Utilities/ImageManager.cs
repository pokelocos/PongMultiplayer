using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace MyEngine
{
    class ImageManager
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static void Register(string filename, Texture2D texture)
        {
            textures.Add(filename,texture);            
        }

        public static Texture2D Get(String name)
        {
            try
            {
                return textures[name];
            }
            catch(NullReferenceException e)
            {
                throw e;
            }
        }
    }
}
