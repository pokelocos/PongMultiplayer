using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using MyEngine.Network.Behaviours;

namespace PongMultiplayer
{
    

    class GameScene : Scene
    {
        
        private float score = 0;

        private GameObject waitingOponent;
        private Clock clock;
        private Ball ball;

        private Controller controller;
        private List<lifeSlot> lifeSlotsL;
        private List<lifeSlot> lifeSlotsR;


        private Random random;

        public GameScene(string name, bool active = true) : base(name, active)
        {
            random = new Random();

            //Background
            GameObject background = new GameObject("Background");
            background.AddBehaviour(new SpriteRender(background, "Background", new Vector2(800, 480)));
            this.gameObjects.Add(background);

            //this.gameObjects.Add(new MultiplayerShowData("ShowData"));

            //Ball
            ball = new Ball("Ball", 0, 10, 0, 10, "circle", new Vector2(40, 40));
            ball.Transform.Position = new Vector3((Globals.widthScreen / 2f) - 20, 240 - 20, 0);
            this.gameObjects.Add(ball);

            //Paddle Left
            Paddle paddleL = new Paddle("PaddleL", 1, 10, "Paddle", new Vector2(20, 120));
            paddleL.Transform.Position = new Vector3(10, 240 - 60, 0);
            this.gameObjects.Add(paddleL);

            //Paddle Right
            Paddle paddleR = new Paddle("PaddlR", 2, 10, "Paddle", new Vector2(20, 120));
            paddleR.Transform.Position = new Vector3(770, 240 - 60, 0);
            this.gameObjects.Add(paddleR);

            //Collider Top
            GameObject wallTop = new GameObject("Top");
            wallTop.AddBehaviour(new Collider.Rect(wallTop, new Vector2(0, -10), new Vector2(800, 10)));
            this.gameObjects.Add(wallTop);

            //Collider Bot
            GameObject wallBot = new GameObject("Bot");
            wallBot.Transform.Position = new Vector3(0, 480, 0);
            wallBot.AddBehaviour(new Collider.Rect(wallBot, new Vector2(0, 10), new Vector2(800, 10)));
            this.gameObjects.Add(wallBot);

            //Trigger goal
            TriggerGoal trigerL = new TriggerGoal("TriggerL", () => { GoalBall(-1); ResetBall(new Vector3(1, 0, 0)); });
            TriggerGoal trigerR = new TriggerGoal("TriggerR", () => { GoalBall(1); ResetBall(new Vector3(-1, 0, 0)); });

            //Waiting Text
            waitingOponent = new GameObject("Waiting");
            waitingOponent.AddBehaviour(new SpriteRender(waitingOponent,"Waiting",new Vector2(500,100)));
            waitingOponent.Transform.Position = new Vector3(150,300,0);
            this.gameObjects.Add(waitingOponent);


            //Clock Text
            clock = new Clock("Clock",new Vector2(100,100),3);
            clock.SetActive(false);
            clock.Transform.Position = new Vector3(350,300,0);
            this.gameObjects.Add(clock);

            //lifes
            lifeSlotsL = new List<lifeSlot>();
            for (int i = 0; i < 3; i++)
            {
                var slot = new lifeSlot("lifeSlotL_" + i, new Vector3(320 - (i * 60), 20, 0), 4+i);
                lifeSlotsL.Add(slot);
                gameObjects.Add(slot);
            }

            lifeSlotsR = new List<lifeSlot>();
            for (int i = 0; i < 3; i++)
            {
                var slot = new lifeSlot("lifeSlotR_" + i, new Vector3(420 + (i * 60), 20, 0), 7+i);
                lifeSlotsR.Add(slot);
                gameObjects.Add(slot);
            }



            if (NetworkManager.Client == null)
            {
                NetworkManager.OnClientAmountChange += StartGame;
            }
            else
            {
                //StartGame();
            }



            if (NetworkManager.isServer)
            {
                controller = new Controller(paddleL, 200);
                //controller.SetActive(false);
                paddleL.GetComponent<TransformNetwork>().controllerID = NetworkManager.clientID;
                paddleL.AddBehaviour(controller);

                ball.GetComponent<TransformNetwork>().controllerID = NetworkManager.clientID;

                clock.GetComponent<SpriteRenderNetwork>().controllerID = NetworkManager.clientID;
                clock.GetComponent<Animator>().Events[0].Keys.Add( new Tuple<Action, float>(() => {
                        ResetBall(Vector3.Normalize(new Vector3(random.Next()-int.MaxValue/2f, random.Next() - int.MaxValue / 2f, 0)));
                    },3f));
                
            }
            else            
            {
                controller = new Controller(paddleR, 200);
                //controller.SetActive(false);
                paddleR.GetComponent<TransformNetwork>().controllerID = NetworkManager.clientID;
                paddleR.AddBehaviour(controller);

                //clock.GetComponent<Animator>().SetActive(false);
            }

    
        }

        private void ResetBall(Vector3 direction)
        {
            ball.Transform.Position = new Vector3((Globals.widthScreen / 2f) - 20, 240 - 20, 0);
            ball.direction = direction;
            ball.speed = 80;
        }

        public override void Actualize()
        {
            base.Actualize();
           
        }

        public void StartGame()
        {
            // controller.SetActive(true);
            waitingOponent.SetActive(false);
            clock.SetActive(true);
        }

        

        public void GoalBall(int n)
        {
            score += n;

            if (score < 0)
            {
                for (int i = 0; i < lifeSlotsL.Count; i++)
                {
                    lifeSlotsL[i].SetLife(n < i);
                }
            }
            else if (score > 0)
            {
                for (int i = 0; i < lifeSlotsR.Count; i++)
                {
                    lifeSlotsR[i].SetLife(n < i);
                }
            }
            else
            {
                for (int i = 0; i < lifeSlotsL.Count; i++)
                {
                    lifeSlotsL[i].SetLife(false);
                }
                for (int i = 0; i < lifeSlotsR.Count; i++)
                {
                    lifeSlotsR[i].SetLife(false);
                }
            }

            if (score <= -lifeSlotsL.Count)
            {
                SceneManager.LoadSyncScene(new OtherMatch("Other Match"));
            }

            if (score >= lifeSlotsR.Count)
            {
                SceneManager.LoadSyncScene(new OtherMatch("Other Match"));
            }

        }
    }
}
