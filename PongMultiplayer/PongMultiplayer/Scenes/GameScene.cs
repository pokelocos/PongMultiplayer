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
        private GameObject waitingOponent;
        private Clock clock;
        private Ball ball;

        private Controller controller;
        private List<lifeSlot> lifeSlotsL;
        private List<lifeSlot> lifeSlotsR;

        private GameDataBheaviour GDB;


        private Random random;

        public GameScene(string name, bool active = true) : base(name, active)
        {
            random = new Random();

            //Background
            GameObject background = new GameObject("Background");
            background.AddBehaviour(new SpriteRender(background, "Background", new Vector2(800, 480)));
            this.gameObjects.Add(background);

            if(Globals.DebugNetWorkMode)
                this.gameObjects.Add(new MultiplayerShowData("ShowData"));

            //Ball
            ball = new Ball("Ball", 0, 10, 0, 0, "circle", new Vector2(40, 40));
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
            wallTop.AddBehaviour(new Collider.Rect(wallTop, new Vector2(0, -30), new Vector2(800, 30),false));
            this.gameObjects.Add(wallTop);

            //Collider Bot
            GameObject wallBot = new GameObject("Bot");
            wallBot.Transform.Position = new Vector3(0, 480, 0);
            wallBot.AddBehaviour(new Collider.Rect(wallBot, new Vector2(0, 0), new Vector2(800, 30),false));
            this.gameObjects.Add(wallBot);

            //Trigger goal
            if (NetworkManager.isServer)
            {
                TriggerGoal trigerL = new TriggerGoal("TriggerL", () => {
                    GDB.GoalBall(-1); ball.Reset(new Vector3(1, 0, 0));
                });
                trigerL.Transform.Position = new Vector3(-50, 0, 0);
                this.gameObjects.Add(trigerL);

                TriggerGoal trigerR = new TriggerGoal("TriggerR", () => {
                    GDB.GoalBall(1); ball.Reset(new Vector3(-1, 0, 0));
                });
                trigerR.Transform.Position = new Vector3(Globals.widthScreen,0,0);
                this.gameObjects.Add(trigerR);
            }

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
                var slot = new lifeSlot("lifeSlotL_" + i, new Vector3( (Globals.widthScreen/2) -80 - (i * 60), 20, 0), 4+i);
                lifeSlotsL.Add(slot);
                gameObjects.Add(slot);
            }

            lifeSlotsR = new List<lifeSlot>();
            for (int i = 0; i < 3; i++)
            {
                var slot = new lifeSlot("lifeSlotR_" + i, new Vector3((Globals.widthScreen / 2) + 20 + (i * 60), 20, 0), 7+i);
                lifeSlotsR.Add(slot);
                gameObjects.Add(slot);
            }

            var GDBObject = new GameObject("GameData");
            GDB = new GameDataBheaviour(GDBObject,-1,10,lifeSlotsL,lifeSlotsR);
            GDBObject.AddBehaviour(GDB);
            this.gameObjects.Add(GDBObject);


            if (NetworkManager.Client == null)
            {
                NetworkManager.OnClientAmountChange += StartGame;
            }
            else
            {
                StartGame();
            }



            if (NetworkManager.isServer)
            {
                controller = new Controller(paddleL, 200);
                //controller.SetActive(false);
                paddleL.transformNetwork.controllerID = NetworkManager.clientID;
                paddleL.AddBehaviour(controller);

                ball.GetComponent<TransformNetwork>().controllerID = NetworkManager.clientID;

                clock.GetComponent<SpriteRenderNetwork>().controllerID = NetworkManager.clientID;
         
                clock.OnEndOfCount = () => {
                    ball.Reset(Vector3.Normalize(new Vector3(random.Next() - int.MaxValue / 2f, random.Next() - int.MaxValue / 2f, 0)));
                };

            }
            else            
            {
                controller = new Controller(paddleR, 200);
                //controller.SetActive(false);
                //paddleR.transformNetwork.controllerID = NetworkManager.clientID;
                paddleR.transformNetwork.controllerID = 1; // segun yo en este punto "clientID" ya deberia estar bien asignada pero es = 0 asi que lo asigno a la mala nomas :C
                paddleR.AddBehaviour(controller);

            }

    
        }

       

        public override void Actualize()
        {
            base.Actualize();

           // Console.WriteLine("score: " + GDB.score);
            if (-GDB.score > lifeSlotsL.Count)
            {
                var v = NetworkManager.isServer ? true : false;
                SceneManager.LoadScene(new OtherMatch("OtherMatch", v));
            }
            else if (GDB.score > lifeSlotsR.Count)
            {
                var v = NetworkManager.isServer ? false : true;
                SceneManager.LoadScene(new OtherMatch("OtherMatch", v));
            }

        }

        public void StartGame()
        {
            // controller.SetActive(true);
            waitingOponent.SetActive(false);
            clock.SetActive(true);
        }

        

      
    }
}
