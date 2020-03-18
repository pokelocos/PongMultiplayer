using Microsoft.Xna.Framework.Graphics;
using MyEngine;
using MyEngine.Network.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class Paddle : GameObject
    {
        public TransformNetwork transformNetwork;

        public Paddle(string name, int idNetwork, float gapMS,string texture,Vector2 size) : base(name)
        {
            AddBehaviour(new SpriteRender(this, texture, size));
            AddBehaviour(new Collider.Rect(this,Vector2.Zero, size,false));
            transformNetwork = new TransformNetwork(this, gapMS, idNetwork);
            AddBehaviour(transformNetwork);
        }

        public override void EnterCollision(Collider other)
        {
            base.EnterCollision(other);
        }

    }
}
