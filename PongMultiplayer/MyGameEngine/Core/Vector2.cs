using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Vector2 Zero { get { return new Vector2(0, 0); } }

        public static Vector2 Normalize(Vector2 v)
        {
            var a = new Microsoft.Xna.Framework.Vector2(v.X, v.Y);
            var r = Microsoft.Xna.Framework.Vector2.Normalize(a);
            return new Vector2(r.X, r.Y);
        }

        internal static float Distance(Vector2 v1, Vector2 v2)
        {
            var a = new Microsoft.Xna.Framework.Vector2(v1.X, v1.Y);
            var b = new Microsoft.Xna.Framework.Vector2(v2.X, v2.Y);
            return Microsoft.Xna.Framework.Vector2.Distance(a, b);
        }

        public Microsoft.Xna.Framework.Vector2 ToXna()
        {
            return new Microsoft.Xna.Framework.Vector2(this.X, this.Y);
        }

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator *(Vector2 v1,Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator *(Vector2 v,float a)
        {
            return new Vector2(v.X * a, v.Y * a);
        }

        public static Vector2 operator *(float a, Vector2 v)
        {
            return new Vector2(v.X * a, v.Y * a);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vector2 operator /(Vector2 v1, float a)
        {
            return new Vector2(v1.X / a, v1.Y / a);
        }
    }
}
