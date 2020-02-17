using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class Time
    {
        public static GameTime clock = new GameTime();

        public static float DeltaTime()
        {
            return clock.ElapsedGameTime.Milliseconds / 1000f;
        }

        public static float CurrentTime()
        {
            return clock.TotalGameTime.Milliseconds / 1000f;
        }

    }
}
