using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public class Rigidbody : Behaviour
    {
        private Vector3 velocity;
        public Vector3 Velocity { get { return velocity; } set { velocity = value; } }

        private Vector3 force;
        private double mass;
        public double Mass { get { return mass; } }
        private bool kinematic;
        public bool Kinematic { set { kinematic = value; } }

        public Rigidbody(GameObject gameObject) : base(gameObject)
        {
            this.velocity = Vector3.Zero;
            this.force = Vector3.Zero;
            this.kinematic = false;
            mass = 1;
        }

        public void AddForce(Vector3 force)
        {
            this.force += force;
        }

        public override void Update()
        {
            if(!kinematic)
            {
                velocity -= Physics.Gravity;
                velocity += this.force * (float)(Time.DeltaTime() / this.mass);

                gameObject.Transform.Translate(new Vector3(this.velocity.X * (float)Time.DeltaTime(), this.velocity.Y * (float)Time.DeltaTime(), this.velocity.Z * (float)Time.DeltaTime()));
                force = Vector3.Zero;
            }
        }
    }
}
