using MyEngine;
using MyEngine.Network.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class GameDataBheaviour : BehaviourNetwork
    {
        public int score = 0;
        public List<lifeSlot> lifeSlotsL, lifeSlotsR;

        public GameDataBheaviour(GameObject gameObject, float gapMS, int networkID,List<lifeSlot> lifeSlotsL, List<lifeSlot> lifeSlotsR) : base(gameObject, gapMS, networkID)
        {
            NetworkManager.SuscribeNetworkBehaviour(this);
            this.lifeSlotsL = lifeSlotsL;
            this.lifeSlotsR = lifeSlotsR;
        }

        public override void Update()
        {
            base.Update();


        }

        public override void UpdateNetwork()
        {
            mutex.WaitOne();
            DataNetwork dataNetwork = new DataNetwork(networkID, NetworkManager.clientID, score);

            var msg = UtilitiesNetwork.ObjectToByteArray(dataNetwork);
            NetworkManager.Send(msg);
            mutex.ReleaseMutex();
        }

        public override void ReciveData(DataNetwork data)
        {
            if (data == null)
            {
                Console.WriteLine("Data null");
                return;
            }

            var obj = (int)data.obj;
            if (obj == null)
            {
                Console.WriteLine("Obj null");
                return;
            }

            mutex.WaitOne();
            Console.WriteLine("OBJ: " + obj);
            this.score = obj;
            mutex.ReleaseMutex();

        }

        public void GoalBall(int n)
        {
            score += n;
            Console.WriteLine("Score: " + score);
            UpdateNetwork();

            for (int i = 0; i < lifeSlotsL.Count; i++)
                lifeSlotsL[i].SetLife(false);

            for (int i = 0; i < lifeSlotsR.Count; i++)
                lifeSlotsR[i].SetLife(false);

            ActiveLifes(score);

            for (int i = 0; i < lifeSlotsL.Count; i++)
                lifeSlotsL[i].network.UpdateNetwork();

            for (int i = 0; i < lifeSlotsR.Count; i++)
                lifeSlotsR[i].network.UpdateNetwork();
           
        }

        private void ActiveLifes(int n)
        {
            switch (score)
            {
                case -3:
                    lifeSlotsR[2].SetLife(true);
                    lifeSlotsR[1].SetLife(true);
                    lifeSlotsR[0].SetLife(true);
                    break;
                case -2:
                    lifeSlotsR[1].SetLife(true);
                    lifeSlotsR[0].SetLife(true);
                    break;
                case -1:
                    lifeSlotsR[0].SetLife(true);
                    break;
                case 0:
                    break;
                case 1:
                    lifeSlotsL[0].SetLife(true);
                    break;
                case 2:
                    lifeSlotsL[0].SetLife(true);
                    lifeSlotsL[1].SetLife(true);
                    break;
                case 3:
                    lifeSlotsL[0].SetLife(true);
                    lifeSlotsL[1].SetLife(true);
                    lifeSlotsL[2].SetLife(true);
                    break;
            }
        }
    }
}
