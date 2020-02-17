using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class Rigidbody2D : Behaviour
    {
        private Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        private Vector2 force;
        private double mass;

        private bool kinematic;
        private float gravityScale;

        private Material material;

        public Rigidbody2D(GameObject gameObject,Material material,double mass) : base(gameObject)
        {
            this.name = "[Rigidbody2D]: "+ gameObject.GetName();

            this.velocity = Vector2.Zero;
            this.mass = mass;
            this.force = Vector2.Zero;
            this.gravityScale = 0;
            this.kinematic = false;

            this.material = material;
        }

        public void AddForce(Vector2 force)
        {
            this.force = force;
        }

        public override void Update()
        {
            //Console.WriteLine(velocity);
            if (!kinematic )
            {
                velocity += this.force * (float)(Time.DeltaTime() / this.mass);
                if(velocity == Vector2.Zero)
                {
                    force = Vector2.Zero;
                    return;
                }
               
                if (
                    Vector2.Distance(Vector2.Zero, velocity ) <=
                    Vector2.Distance(Vector2.Zero, (float)(this.mass * Physics.Gravity.Y) * material.estatic * -Vector2.Normalize(velocity)))
                {
                    velocity = Vector2.Zero;
                    force = Vector2.Zero;
                    return;
                }

                Vector2 dynamicForce = (float)(this.mass * Physics.Gravity.Y) * material.dynamic * -Vector2.Normalize(velocity);
                velocity += dynamicForce * (float)(Time.DeltaTime() / this.mass); 
                gameObject.Transform.Translate(new Vector3(this.velocity.X * (float)Time.DeltaTime(), this.velocity.Y * (float)Time.DeltaTime(), 0));
                force = Vector2.Zero;
            }
        }
    }
}
