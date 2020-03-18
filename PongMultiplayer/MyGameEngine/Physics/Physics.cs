using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public class Physics
    {
        private static Vector3 gravity = new Vector3(0, 9.8f,0);
        public static Vector3 Gravity { get { return gravity; } set { gravity = value; } }

        public static List<Collider> colliders = new List<Collider>();

        public static void Check(Collider collider)
        {
            foreach (Collider c in Physics.colliders)
            {
                if (collider != c)
                {
                    c.IsCollider(collider);
                }
            }
        }

        public static void Update()
        {
            //Console.WriteLine(colliders.Count);
            foreach (Collider c in colliders)
            {
                Check(c);
            }
        }

        private static void Fix(Collider collider,Collider other)
        {
            if(collider.GameObject.GetComponent<Rigidbody2D>() != null)
            {


            }
        }
    }
}
