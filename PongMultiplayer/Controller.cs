using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace PongMultiplayer
{
    class Controller : Behaviour
    {
        public float speed;

        public Controller(GameObject gameObject,float speed) : base(gameObject)
        {
            this.speed = speed;
        }

        public override void Update()
        {
            base.Update();
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                this.gameObject.Transform.Translate(new Vector3(0,-speed * Time.DeltaTime(),0));
            }
            else if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                this.gameObject.Transform.Translate(new Vector3(0, speed * Time.DeltaTime(), 0));
            }
        }
    }
}
