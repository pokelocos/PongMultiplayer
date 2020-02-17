using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class MeshRender : Behaviour , IRendereable
    {
        private Model model;

        private Matrix[] matrixArray;
        private Matrix modelMatrix;

        public MeshRender(GameObject gameObject,Model model) : base(gameObject)
        {
            this.model = model;
            this.matrixArray = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(matrixArray);

            this.modelMatrix = new Matrix();
        }

        public override void Update()
        {
            Matrix Rotation = 
                Matrix.CreateRotationX(gameObject.Transform.Rotation.X) * 
                Matrix.CreateRotationY(gameObject.Transform.Rotation.Y) * 
                Matrix.CreateRotationZ(gameObject.Transform.Rotation.Z);

            Matrix Traslation = Matrix.CreateTranslation(gameObject.Transform.Position.ToXna());

            modelMatrix = Rotation * Traslation;
        }

        public void Draw(Camera camera)
        {
            foreach(ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    Matrix world = matrixArray[mesh.ParentBone.Index] * modelMatrix;
                    Matrix view = Matrix.CreateLookAt(camera.Transform.Position.ToXna(),camera.Target.ToXna(), camera.CameraUp.ToXna());
                    Matrix projection = Matrix.CreatePerspectiveFieldOfView(camera.Fov,camera.AspectRatio,camera.Near,camera.Far);

                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
    }
}
