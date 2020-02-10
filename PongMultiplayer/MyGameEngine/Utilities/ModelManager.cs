using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Utilities
{
    class ModelManager
    {
        public static Dictionary<string, Model> models = new Dictionary<string, Model>();

        public static void Register(string filename, Model model)
        {
            models.Add(filename, model);
        }

        public static Model Get(String name)
        {
            try
            {
                return models[name];
            }
            catch (NullReferenceException e)
            {
                throw e;
            }
        }
    }
}
