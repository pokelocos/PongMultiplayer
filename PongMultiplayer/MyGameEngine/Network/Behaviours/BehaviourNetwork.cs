using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyEngine.Network.Behaviours
{
    public abstract class BehaviourNetwork : Behaviour
    {
        public float gapMS;
        protected float currentTime;
        public int networkID;

        public int controllerID = 0;
        public bool isUpdatable = true;

        public Mutex mutex = new Mutex();

        public BehaviourNetwork(GameObject gameObject, float gapMS, int networkID) : base(gameObject)
        {
            this.gapMS = gapMS;
            this.currentTime = 0;
            this.networkID = networkID;
        }

        public override void Update()
        {
            base.Update();
           // Console.WriteLine("Behaviour (" + this.gameObject.GetName() + "): " + NetworkManager.Client + ", " + controllerID + " == " + NetworkManager.clientID);
            if (isUpdatable && NetworkManager.Client != null && NetworkManager.Client.Connected && controllerID == NetworkManager.clientID)
            {
                if (gapMS / 1000f < currentTime)
                {
                    currentTime = 0f;
                    UpdateNetwork();
                }
                this.currentTime += Time.DeltaTime();
            }
        }

        public abstract void UpdateNetwork();
        public abstract void ReciveData(DataNetwork data);
    }
}
