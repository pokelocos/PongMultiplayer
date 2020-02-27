using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class TimeLine<T>
    {
        public bool loop = false;
        public float speed = 1;
        public float duration;

        private List<Tuple<T, float>> keys;
        public List<Tuple<T, float>> Keys { get { return keys; } }

        public TimeLine(List<Tuple<T, float>> keys,float duration)
        {
            this.keys = keys;
            this.duration = duration;
        }

        public T GetKey(float time)
        {
            float deltaTime = float.PositiveInfinity;
            T toReturn = default(T);
            foreach (var frame in keys)
            {
                float delta  = (loop)? (time % duration) * speed - frame.Item2 : time * speed - frame.Item2;

                if (delta >= 0 && delta < deltaTime)
                {
                    deltaTime = delta;
                    toReturn = frame.Item1;
                }
            }
            return toReturn;
        }

    }
}
