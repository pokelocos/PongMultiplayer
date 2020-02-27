using MyEngine;
using MyEngine.Network.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class lifeSlot : GameObject
    {
        string[] imgName = { "LifeEmpty" , "LifeFull" };

        SpriteRender spriteRender;

        public lifeSlot(string name, Vector3 position,int networkID) : base(name)
        {
            spriteRender = new SpriteRender(this, "LifeEmpty", new Vector2(60, 40));
            transform.Position = position;
            AddBehaviour(spriteRender);

            if (NetworkManager.isServer)
            {
                var network = new SpriteRenderNetwork(this, 100, networkID);
                network.isUpdatable = false;
                AddBehaviour(network);
            }
        }

        public void SetLife(bool i)
        {
            spriteRender.spriteName = i ? imgName[1] : imgName[0];
            spriteRender.Sprite = i ? ImageManager.Get(imgName[1]) : ImageManager.Get(imgName[0]);
        }
    }
}
