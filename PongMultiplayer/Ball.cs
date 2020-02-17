using Microsoft.Xna.Framework.Graphics;
using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class Ball : GameObject
    {
        private float speed;
        private float aceleration;
        private Vector3 direction;

        public Ball(string name,int idNetwork,float gapMS,float speed,float acceleration,Texture2D texture,Vector2 size) : base(name)
        {
            AddBehaviour(new SpriteRender(this,texture,size));
            AddBehaviour(new Collider.Rect(this, size / 2f, size));
            AddBehaviour(new TransformNetwork(this, gapMS, idNetwork));
           

            this.speed = speed;
            this.aceleration = acceleration;
            direction = Vector3.UnitX;
        }

        public override void Actualize()
        {
            base.Actualize();

            this.transform.Translate(direction * speed * Time.DeltaTime()); 

        }

        public override void EnterCollision(Collider other)
        {
            if (other.GameObject.GetName().Equals("Top") || other.GameObject.GetName().Equals("Bot"))
            {
                direction = Vector3.Normalize(new Vector3(direction.X, -direction.Y,0));
                return;
            }

            var otherCenter = other.GameObject.Transform.Position;
            var myCenter = this.transform.Position;
            var dir = Vector3.Normalize(myCenter - otherCenter) + Vector3.Normalize(new Vector3(-direction.X, direction.Y, 0));

            speed += aceleration;
            this.direction = Vector3.Normalize(dir);
        }

    }
}
