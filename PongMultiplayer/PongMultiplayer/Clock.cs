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
    class Clock : GameObject
    {
        public Vector2 size;

        public Action actions;

        public Clock(string name, Vector2 size, int networkID) : base(name)
        {
            this.size = size;
            AddBehaviour(new SpriteRender(this,"3", size));

            var network = new SpriteRenderNetwork(this, 100, networkID);
            network.isUpdatable = false;
            AddBehaviour(network);

            if(NetworkManager.isServer)
            {
                Animator animator = new Animator(this);

                List<Tuple<string, float>> anims = new List<Tuple<string, float>>();
                anims.Add(new Tuple<string, float>("3", 0f));
                anims.Add(new Tuple<string, float>("2", 1f));
                anims.Add(new Tuple<string, float>("1", 2f));
                anims.Add(new Tuple<string, float>("Blank", 3f));
                var spriteKeys = new TimeLine<string>(anims, 4f);
                animator.Animations.Add(spriteKeys);

                List<Tuple<Action, float>> events = new List<Tuple<Action, float>>();
                events.Add(new Tuple<Action, float>(() => { network.UpdateNetwork(); }, 0f));
                events.Add(new Tuple<Action, float>(() => { network.UpdateNetwork(); }, 1f));
                events.Add(new Tuple<Action, float>(() => { network.UpdateNetwork(); }, 2f));
                events.Add(new Tuple<Action, float>(() => { network.UpdateNetwork(); actions?.Invoke(); }, 3f));
                var actionKeys = new TimeLine<Action>(events, 4f);
                animator.Events.Add(actionKeys);

                AddBehaviour(animator);
            }
        }

        
    }
}
