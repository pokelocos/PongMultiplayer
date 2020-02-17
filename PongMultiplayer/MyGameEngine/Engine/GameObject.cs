using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyEngine
{
    [System.Serializable]
    public class GameObject 
    {
        protected string name;
        protected Transform transform;
        public Transform Transform { get { return transform; } }

        protected bool active;

        protected List<Behaviour> behaviours = new List<Behaviour>();
        public List<Behaviour> Behaviours { get { return behaviours; } }
       
        public GameObject(string name) 
        {
            this.name = name;
            this.transform = new Transform(this);
            active = true;
        }

        public T GetComponent<T>() where T : Behaviour
        {
            foreach (Behaviour b in behaviours)
            {
                if (b is T)
                {
                    return (T)b;
                }
            }
            return null;
        }

        public virtual void StayCollision(Collider other) { }
        public virtual void EnterCollision(Collider other) { }
        public virtual void ExitCollision(Collider other) { }

        public void AddBehaviour(Behaviour behaviour)
        {
            behaviours.Add(behaviour);
        }

        public string GetName()
        {
            return this.name;
        }

        public void Init()
        {
            foreach (Behaviour b in behaviours)
            {
                b.Init();
            }
        }

        public void Start()
        {
            foreach (Behaviour b in behaviours)
            {
                if (b.IsActive())
                {
                    b.Start();
                }
            }
        }

        public virtual void Actualize()
        {
            foreach (Behaviour b in behaviours)
            {
                if(b.IsActive())
                {
                    b.Actualize();
                }                
            }
        }

        internal void OnExiting()
        {
            foreach (Behaviour b in behaviours)
            {
                b.OnExiting();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Behaviour b in behaviours)
            {
                if(b.IsActive())
                {
                    if (b is IDrawable)
                    {
                        ((IDrawable)b).Draw(sb);
                    }
                    if(b is IRendereable)
                    {
                        ((IRendereable)b).Draw(Camera.main);
                    }
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


        ////////


        public int SizeBehaviour()
        {
            return behaviours.Count;
        }

        public Behaviour GetBehaviour(int i)
        {
            try
            {
                return behaviours[i];
            }
            catch(NullReferenceException e)
            {
                throw e;
            }
        }
    }
}
