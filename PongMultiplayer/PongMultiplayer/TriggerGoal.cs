using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class TriggerGoal : GameObject
    {

        Action action;
       
        public TriggerGoal(string name, Action action) : base(name)
        {
            AddBehaviour(new Collider.Rect(this,Vector2.Zero,new Vector2(50,600),true));
            this.action = action;
        }

        public override void EnterCollision(Collider other)
        {
           
        }

        public override void EnterTrigger(Collider other)
        {
            if (other.GameObject.GetName().Equals("Ball"))
            {
                action?.Invoke();
                // var ball = (Ball)other.GameObject;
                // ball.Reset(new Vector3(0,0,0));
            }
        }
    }
}
