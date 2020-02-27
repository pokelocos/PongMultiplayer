using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine.Network.Behaviours
{
    class TransformNetwork : BehaviourNetwork
    {
        private Transform target;

        public TransformNetwork(GameObject gameObject,float gapTime,int networkID) : base(gameObject,gapTime,networkID)
        {
            this.target = gameObject.Transform;
            NetworkManager.SuscribeNetworkBehaviour(this);
        }
      
        public override void UpdateNetwork()
        {
            mutex.WaitOne();
            DataNetwork dataNetwork = new DataNetwork(networkID,NetworkManager.clientID,target);

            var msg = UtilitiesNetwork.ObjectToByteArray(dataNetwork); 
            NetworkManager.Send(msg);
            mutex.ReleaseMutex();
        }

        public override void ReciveData(DataNetwork data)
        {
            if (data == null) {
                Console.WriteLine("Data null");
                return;
            }

            var obj = (Transform)data.obj;
            if (obj == null)
            {
                Console.WriteLine("Obj null");
                return;
            }

            mutex.WaitOne();
            target.Position = obj.Position;
            mutex.ReleaseMutex();
        }

        

    }
}
