using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class Button : Behaviour
    {
        public delegate void Event();

        private Vector2 box;

        public Button(GameObject gameObject, Vector2 box) : base(gameObject)
        {
            this.box = box;
        }

        public override void Update()
        {
            base.Update();
            Rectangle rect = new Rectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, (int)box.X, (int)box.Y);
            if (rect.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton.Equals(ButtonState.Pressed))
            {
                Action.Invoke();
            }
        }


        public Event Action;
    }
}
