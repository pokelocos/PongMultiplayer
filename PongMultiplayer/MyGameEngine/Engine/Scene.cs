using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Scene
    {
        protected List<GameObject> gameObjects = new List<GameObject>();

        protected bool active;

        public Scene(bool active)
        {
            this.active = active;
        }

        public void Init()
        {
            foreach (GameObject go in gameObjects)
            {
                go.Init();
            }
        }

        public void Start()
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.IsActive())
                {
                    go.Start();
                }
            }            
        }

        public void Actualize()
        {
            
            foreach (GameObject go in gameObjects)
            {
                if (go.IsActive())
                {
                    go.Actualize();
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject go in gameObjects)
            {
                if(go.IsActive())
                {
                    go.Draw(sb);
                }
            }
        }

        public bool IsActive()
        {
            return active;
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }
    }
}
