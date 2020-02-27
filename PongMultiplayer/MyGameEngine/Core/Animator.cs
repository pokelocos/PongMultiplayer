using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class Animator : Behaviour
    {
        // esto deberia tener un obj animation que tenga timelines :)
        private List<TimeLine<string>> animations = new List<TimeLine<string>>();
        public List<TimeLine<string>> Animations { get { return animations; } }
        private TimeLine<string> actualAnim;

        private List<TimeLine<Action>> events = new List<TimeLine<Action>>();
        public List<TimeLine<Action>> Events { get { return events; } }
        private TimeLine<Action> actualEvent;
        private Action lastEvent;

        private SpriteRender spriteRender;

        public float actualTime = 0;

        public Animator(GameObject gameObject) : base(gameObject)
        {
            this.spriteRender = gameObject.GetComponent<SpriteRender>();
        }

        public override void Update()
        {
            base.Update();

            if (actualAnim == null)
                actualAnim = animations[0];
            if (actualEvent == null)
                actualEvent = events[0];

            var imageName = actualAnim.GetKey(actualTime);

            if (imageName != spriteRender.spriteName)
            {
                spriteRender.spriteName = imageName;
                spriteRender.Sprite = ImageManager.Get(imageName);

                actualEvent.GetKey(actualTime).Invoke();
            }

          

            /*
            var evnt = actualEvent.GetKey(actualTime);
            if(evnt != lastEvent)
            {
                evnt.Invoke();
            }
            */

            actualTime += Time.DeltaTime();
        }
    }
}
