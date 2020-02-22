using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MyEngine
{
    public class InputManager
    {
        public static List<ISelectable> selectables = new List<ISelectable>();

        public static ISelectable over;
        public static ISelectable focus;

        public static void Update()
        {
            foreach (var selectable in selectables)
            {
                if(selectable.GetRect().Contains(Mouse.GetState().Position))
                {

                }
            }
        }
    }
}
