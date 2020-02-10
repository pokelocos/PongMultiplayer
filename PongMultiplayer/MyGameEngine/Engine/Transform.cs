using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class Transform
    {
        private Vector3 position;
        public Vector3 Position { get { return position; } set { position = value; } }

        private Vector3 rotation;
        public Vector3 Rotation { get { return rotation; } set { rotation = value; } }

        //eliminar esto
        private float rotation2D;
        public float Rotation2D { get { return rotation2D; } set { rotation2D = value; } }
        
        private Vector3 scale;
        public Vector3 Scale { get { return scale; } set { scale = value; } }

        private GameObject gameObject;
        private List<Transform> childs;

        public Transform(GameObject gameObject)
        {
            position = Vector3.Zero;
            rotation = Vector3.Zero;
            rotation2D = 0;
            scale = Vector3.One;

            this.gameObject = gameObject;
            this.childs = new List<Transform>();
        }

        public void Translate(Vector3 traslation)
        {
            position += traslation;
        }

        public void Rotate(Vector3 rotation)
        {
            this.rotation.X = (this.rotation.X + rotation.X) % 360;
            this.rotation.Y = (this.rotation.Y + rotation.Y) % 360;
            this.rotation.Z = (this.rotation.Z + rotation.Z) % 360;
        }

        public void Rotate(float rotation)
        {
            this.rotation2D = (this.rotation2D + rotation) % 360; 
        }
    }
}