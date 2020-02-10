using System;
using System.Collections.Generic;

namespace Game1
{
    public abstract class Behaviour
    {
        protected GameObject gameObject;
        public GameObject GameObject { get { return gameObject; } }

        protected bool active;
        protected string name;

        public Behaviour(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.active = true;
            this.name = "[Undefined Name]";
        }

        public void Actualize()
        {
            Update();
            FixedUpdate();
        }

        public virtual void Init() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        public virtual void StayCollision(Collider other) { }
        public virtual void EnterCollision(Collider other) { }
        public virtual void ExitCollision(Collider other) { }

        public bool IsActive()
        {
            return active;
        }

        public void SetActive(bool active)
        {
            if(this.active && !active)
            {
                OnDisable();
            }
            else if(!this.active && active)
            {
                OnEnable();
            }
            this.active = active;
        }

        
    }

    
}
