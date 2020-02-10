using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Engine
{
    class Camera : GameObject
    {
        private Vector2 dimension;
        private Vector3 target;
        public Vector3 Target { get { return target; } set { target = value; }}

        private Vector3 cameraUp;
        public Vector3 CameraUp { get { return cameraUp; } set { cameraUp = value; } }

        private float aspectRatio;
        public float AspectRatio { get { return aspectRatio; } set { aspectRatio = value; } }

        private float near = 1f;
        public float Near { get { return near; } set { near = value; } }

        private float far = 100000f;
        public float Far { get { return far; } set { far = value; } }

        private float fov = MathHelper.PiOver2;
        public float Fov { get { return fov; } set { fov = MathHelper.Clamp(value, MathHelper.PiOver2 * 0.5f, MathHelper.PiOver2 * 1.5f); } }

        public static Camera main;

        public float GetFov()
        {
            return fov;
        }

        public Camera(int width,int heigth) : base("Camera")
        {
            this.dimension = new Vector2(width,heigth);
            this.target = this.Transform.Position + Vector3.UnitZ;
            this.cameraUp = Vector3.UnitY;
            aspectRatio = width / (float)heigth;
        }

    }

    enum CameraMode
    {
        Perspective,
        Ortographyc
    }
}
