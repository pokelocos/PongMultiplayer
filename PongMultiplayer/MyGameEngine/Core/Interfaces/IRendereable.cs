using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    interface IRendereable
    {
        void Draw(Camera cam);
    }
}
