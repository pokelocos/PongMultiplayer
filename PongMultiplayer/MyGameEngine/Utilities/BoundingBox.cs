using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    
    public class BoundingBox
    {
        public double x, y, w, h;

        public BoundingBox(double x, double y, double w, double h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public BoundingBox()
        {
            this.x = 0;
            this.y = 0;
            this.w = 0;
            this.h = 0;
        }
    }
}
