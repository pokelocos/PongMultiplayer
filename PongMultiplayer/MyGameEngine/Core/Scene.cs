using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public class Scene
    {
        public bool isInit = false;

        internal List<GameObject> gameObjects = new List<GameObject>();

        public string name;
        protected bool active;

        public Scene(string name, bool active = false)
        {
            this.name = name;
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

        public virtual void Actualize()
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
                if (go.IsActive())
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

        internal void OnExiting()
        {
            foreach (GameObject go in gameObjects)
            {
                go.OnExit();
            }
        }
    }
}
