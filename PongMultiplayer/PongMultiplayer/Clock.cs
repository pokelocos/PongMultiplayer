using Microsoft.Xna.Framework.Graphics;
using MyEngine;
using MyEngine.Multiplayer.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class Clock : GameObject
    {
        public Texture2D[] sprites;

        public delegate void TimeEvent(float time);

        public TimeEvent OnStartClock;
        public TimeEvent OnEverySecond;
        public TimeEvent OnEndClock;

        private float totalTime = 0;
        private float time = 0;

        public bool isRunning = false;

        private SpriteRender spriteRender;
        private SpriteRenderNetwork network;

        public Clock(string name, int networkID) : base(name)
        {
            sprites[0] = ImageManager.Get("3"); // dejo esto aqui apesar de que no sirva para acordarme que esto podria ser una animacion <3 :C
            sprites[1] = ImageManager.Get("2");
            sprites[2] = ImageManager.Get("1");

            spriteRender = new SpriteRender(this, ImageManager.Get("3"), new Vector2(100, 100));
            AddBehaviour(spriteRender);
            network = new SpriteRenderNetwork(this, 100, 0);
            network.isUpdatable = false;
            AddBehaviour(network);
            OnEverySecond += CancerMethod;
        }

        public override void Actualize()
        {
            base.Actualize();

            if(isRunning)
            {
                if (totalTime == 0)
                {
                    OnStartClock.Invoke(0f);
                }

                if (time >= 1)
                {
                    OnStartClock.Invoke(totalTime);
                    time = 0;
                }
                time += Time.DeltaTime();
                totalTime += Time.DeltaTime();
            }
        }

        public void CancerMethod(float seg)
        {
            switch((int)seg)
            {
                case 0:
                    spriteRender.Sprite = (ImageManager.Get("3"));
                    network.UpdateNetwork();
                    break;
                case 1:
                    spriteRender.Sprite = (ImageManager.Get("3"));
                    network.UpdateNetwork();
                    break;
                case 2:
                    spriteRender.Sprite = (ImageManager.Get("2"));
                    network.UpdateNetwork();
                    break;
                case 3:
                    spriteRender.Sprite = (ImageManager.Get("1"));
                    network.UpdateNetwork();
                    break;
                default:
                    spriteRender.Sprite = (ImageManager.Get("Blank"));
                    network.UpdateNetwork();
                    StopClock();
                    break;
            }
        }

        public void StopClock()
        {
            OnEndClock.Invoke(totalTime);
            isRunning = false;
            totalTime = 0;
            time = 0;
        }

        public void StartClock()
        {
            isRunning = true;
        }
    }
}
