using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine.Multiplayer.Behaviours
{
    class SpriteRenderNetwork : BehaviourNetwork
    {
        private SpriteRender target;

        public SpriteRenderNetwork(GameObject gameObject, float gapMS, int networkID) : base(gameObject, gapMS, networkID)
        {
            this.target = gameObject.GetComponent<SpriteRender>();
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

            target.Sprite = obj.Sprite;
        }

        public override void UpdateNetwork()
        {
            DataNetwork dataNetwork = new DataNetwork(networkID, MultiplayerManager.clientID, target);
            var msg = UtilitiesMultiplayer.ObjectToByteArray(dataNetwork);
            MultiplayerManager.Send(msg);
        }
    }
}
