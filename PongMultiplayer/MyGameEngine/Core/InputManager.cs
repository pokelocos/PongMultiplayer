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
        public delegate void Event();

        public static List<ISelectable> selectables = new List<ISelectable>();

        public static ISelectable focus;

        public static ButtonState lastState = ButtonState.Released;

        public static List<ISelectable> lastOver = new List<ISelectable>();

        public static List<Action> Actions = new List<Action>();

        public static void Update()
        {
            foreach (var selectable in selectables)
            {
                if(selectable.GetRect().Contains(Mouse.GetState().Position))
                {
                    if(!lastOver.Contains(selectable))
                    {
                        lastOver.Add(selectable);
                        Actions.Add(selectable.OnMouseEnter);
                    }
                }
                else
                {
                    if(lastOver.Contains(selectable))
                    {
                        lastOver.Remove(selectable);
                        Actions.Add(selectable.OnMouseOut);
                    }
                }

                if (lastOver.Contains(selectable) &&  Mouse.GetState().LeftButton == ButtonState.Pressed && lastState == ButtonState.Released)
                {
                    Actions.Add(selectable.OnClickDown);
                    focus = selectable;
                }

                if (lastOver.Contains(selectable) && Mouse.GetState().LeftButton == ButtonState.Released && lastState == ButtonState.Pressed)
                {
                    Actions.Add(selectable.OnClickUp);
                }
            }
            lastState = Mouse.GetState().LeftButton;

            foreach (var item in Actions)
                item.Invoke();

            Actions = new List<Action>();
        }

        internal static void UnSuscribeFromScene(Scene scene)
        {
            foreach (var gameobject in scene.gameObjects)
            {
                if (gameobject is ISelectable selectable && selectables.Contains(selectable))
                {
                    selectables.Remove(selectable);
                }
                foreach (var behaviour in gameobject.Behaviours)
                {
                    if (behaviour is ISelectable _selectable && selectables.Contains(_selectable))
                    {
                        selectables.Remove(_selectable);
                    }
                }
            }
        }

        internal static void UnSuscribeFromScenes(List<Scene> scenes)
        {
            foreach (var scene in scenes)
            {
                UnSuscribeFromScene(scene);
            }
        }
    }
}
