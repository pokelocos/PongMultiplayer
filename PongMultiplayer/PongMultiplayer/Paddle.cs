using Microsoft.Xna.Framework.Graphics;
using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class Paddle : GameObject
    {
        public Paddle(string name, int idNetwork, float gapMS,Texture2D texture,Vector2 size) : base(name)
        {
            AddBehaviour(new SpriteRender(this, texture, size));
            AddBehaviour(new Collider.Rect(this, size / 2f, size));
            AddBehaviour(new TransformNetwork(this, gapMS, idNetwork));
        }

        public override void EnterCollision(Collider other)
        {
            base.EnterCollision(other);
        }

    }
}
