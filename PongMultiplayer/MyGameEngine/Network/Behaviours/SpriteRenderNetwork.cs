using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine.Network.Behaviours
{
    class SpriteRenderNetwork : BehaviourNetwork
    {
        private SpriteRender target;

        public SpriteRenderNetwork(GameObject gameObject, float gapMS, int networkID) : base(gameObject, gapMS, networkID)
        {
            this.target = gameObject.GetComponent<SpriteRender>();
            NetworkManager.SuscribeNetworkBehaviour(this);
        }

        public override void UpdateNetwork()
        {
            mutex.WaitOne();
            DataNetwork dataNetwork = new DataNetwork(networkID, NetworkManager.clientID, target);

            Console.WriteLine("ID: " + networkID + " ,clientID: " + NetworkManager.clientID + " ,target: " + target);

            var msg = UtilitiesNetwork.ObjectToByteArray(dataNetwork);
            NetworkManager.Send(msg);
            mutex.ReleaseMutex();
        }

        public override void ReciveData(DataNetwork data)
        {
            if (data == null){
                Console.WriteLine("Data null");
                return;
            }

            var obj = (SpriteRender)data.obj;
            if (obj == null){
                Console.WriteLine("Obj null");
                return;
            }

            Console.WriteLine("Recive data: Sprite network: "+ obj);

            mutex.WaitOne();
            //target.Sprite = obj.Sprite;
            target.Sprite = ImageManager.Get(obj.spriteName);
            mutex.ReleaseMutex();
        }

       
    }
}
