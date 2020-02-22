using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    [System.Serializable]
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;
        

        public Vector3(float X,float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public static Vector3 Zero { get { return new Vector3(0, 0, 0); } }

        public static Vector3 One { get { return new Vector3(1, 1, 1); } }


        public static Vector3 UnitX{ get { return new Vector3(1, 0, 0); } }
        public static Vector3 UnitZ { get { return new Vector3(0, 0, 1); } }
        public static Vector3 UnitY { get { return new Vector3(0, 1, 0); } }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X+ b.X,a.Y+ b.Y,a.Z+b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        internal static double Distance(Vector3 v1, Vector3 v2)
        {
            var a = new Microsoft.Xna.Framework.Vector3(v1.X,v1.Y,v1.Z);
            var b = new Microsoft.Xna.Framework.Vector3(v2.X, v2.Y, v2.Z);
            return Microsoft.Xna.Framework.Vector3.Distance(a, b);
        }

        public static Vector3 Normalize(Vector3 v)
        {
            var a = new Microsoft.Xna.Framework.Vector3(v.X, v.Y,v.Z);
            var r = Microsoft.Xna.Framework.Vector3.Normalize(a);
            return new Vector3(r.X, r.Y,r.Z);
        }

        public Microsoft.Xna.Framework.Vector3 ToXna()
        {
            return new Microsoft.Xna.Framework.Vector3(this.X,this.Y,this.Z);
        }

    }
}
