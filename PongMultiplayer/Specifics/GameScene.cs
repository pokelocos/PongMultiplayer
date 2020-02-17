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

namespace PongMultiplayer
{
    

    class GameScene : Scene
    {
        public float time;
        public float maxTime = 3;

        GameObject waitingOponent;
        SpriteRender numberRender;


        public GameScene(string name, bool active = true) : base(name, active)
        {
            GameObject background = new GameObject("Background");
            background.AddBehaviour(new SpriteRender(background,ImageManager.Get("Background"),new Vector2(800,480)));
            this.gameObjects.Add(background);

            //this.gameObjects.Add(new MultiplayerShowData("ShowData"));

            Ball ball = new Ball("Ball",0,10,0,10,ImageManager.Get("circle"),new Vector2(40,40));
            ball.Transform.Position = new Vector3((Globals.widthScreen/2f) - 20, 240 - 20, 0);
            this.gameObjects.Add(ball);

            Paddle paddleL = new Paddle("PaddleL",1,10,ImageManager.Get("Paddle"),new Vector2(20,120));
            paddleL.Transform.Position = new Vector3(10, 240 - 60, 0);
            this.gameObjects.Add(paddleL);

            Paddle paddleR = new Paddle("PaddlR", 2, 10, ImageManager.Get("Paddle"), new Vector2(20, 120));
            paddleR.Transform.Position = new Vector3(770, 240 - 60, 0);
            this.gameObjects.Add(paddleR);

            GameObject wallTop = new GameObject("Top");
            wallTop.AddBehaviour(new Collider.Rect(wallTop,new Vector2(0,-10),new Vector2(800,10)));
            this.gameObjects.Add(wallTop);

            GameObject wallBot = new GameObject("Bot");
            wallBot.Transform.Position = new Vector3(0, 480, 0);
            wallBot.AddBehaviour(new Collider.Rect(wallBot, new Vector2(0, 10), new Vector2(800, 10)));
            this.gameObjects.Add(wallBot);

            waitingOponent = new GameObject("Waiting");
            waitingOponent.AddBehaviour(new SpriteRender(waitingOponent,ImageManager.Get("Waiting"),new Vector2(500,100)));
            waitingOponent.Transform.Position = new Vector3(150,300,0);
            this.gameObjects.Add(waitingOponent);

            GameObject numberImg = new GameObject("clockNumber");
            numberRender = new SpriteRender(numberImg, ImageManager.Get("3"), new Vector2(100, 100));
            numberImg.AddBehaviour(numberRender);
            numberImg.SetActive(false);
            numberImg.Transform.Position = new Vector3(350, 300, 0);
            this.gameObjects.Add(numberImg);

            if (MultiplayerManager.isServer)
            {
                Controller controller = new Controller(paddleL, 200);
                paddleL.GetComponent<TransformNetwork>().controllerID = MultiplayerManager.clientID;
                paddleL.AddBehaviour(controller);

                ball.GetComponent<TransformNetwork>().controllerID = MultiplayerManager.clientID;
            }
            else            
            {
                Controller controller = new Controller(paddleR, 200);
                paddleR.GetComponent<TransformNetwork>().controllerID = MultiplayerManager.clientID;
                paddleR.AddBehaviour(controller);
            }
            
        }

        public override void Actualize()
        {
            if(MultiplayerManager.Client != null && MultiplayerManager.Client.Connected)
            {
                waitingOponent.SetActive(false);
                numberRender.GameObject.SetActive(true);                
                switch((int)time)
                {
                    case 0:
                        numberRender.SetImage(ImageManager.Get("3"));
                        break;
                    case 1:
                        numberRender.SetImage(ImageManager.Get("2"));
                        break;
                    case 2:
                        numberRender.SetImage(ImageManager.Get("1"));
                        break;
                    default:
                        base.Actualize();
                        break;
                }
                time += Time.DeltaTime();
            }
        }



    }
}
