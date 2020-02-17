using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public abstract class BehaviourNetwork : Behaviour
    {
        public float gapMS;
        protected float currentTime;
        public int networkID;

        public int controllerID = 0;

        public BehaviourNetwork(GameObject gameObject, float gapMS, int networkID) : base(gameObject)
        {
            this.gapMS = gapMS;
            this.currentTime = 0;
            this.networkID = networkID;
        }

        public override void Update()
        {
            base.Update();
            if (MultiplayerManager.Client != null && MultiplayerManager.Client.Connected && controllerID == MultiplayerManager.clientID)
            {
                if (gapMS / 1000f < currentTime)
                {
                    currentTime = 0f;
                    UpdateNetwork();
                }
                this.currentTime += Time.DeltaTime();
            }
        }

        protected abstract void UpdateNetwork();
        public abstract void ReciveData(DataNetwork data);
    }
}
