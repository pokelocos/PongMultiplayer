using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    class TransformNetwork : BehaviourNetwork
    {
        private Transform target;

        public TransformNetwork(GameObject gameObject,float gapTime,int networkID) : base(gameObject,gapTime,networkID)
        {
            this.target = gameObject.Transform;
            MultiplayerManager.SuscribeNetworkBehaviour(this);
        }
      
        public override void UpdateNetwork()
        {
            DataNetwork dataNetwork = new DataNetwork(networkID,MultiplayerManager.clientID,target);

            var msg = UtilitiesMultiplayer.ObjectToByteArray(dataNetwork); 
            MultiplayerManager.Send(msg);
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

            target.Position = obj.Position;
        }

        

    }
}
