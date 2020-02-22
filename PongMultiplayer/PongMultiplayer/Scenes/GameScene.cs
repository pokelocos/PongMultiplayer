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
using MyEngine.Multiplayer.Behaviours;

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
            //Background
            GameObject background = new GameObject("Background");
            background.AddBehaviour(new SpriteRender(background,ImageManager.Get("Background"),new Vector2(800,480)));
            this.gameObjects.Add(background);

            //this.gameObjects.Add(new MultiplayerShowData("ShowData"));

            //Ball
            Ball ball = new Ball("Ball",0,10,0,10,ImageManager.Get("circle"),new Vector2(40,40));
            ball.Transform.Position = new Vector3((Globals.widthScreen/2f) - 20, 240 - 20, 0);
            this.gameObjects.Add(ball);

            //Paddle Left
            Paddle paddleL = new Paddle("PaddleL",1,10,ImageManager.Get("Paddle"),new Vector2(20,120));
            paddleL.Transform.Position = new Vector3(10, 240 - 60, 0);
            this.gameObjects.Add(paddleL);

            //Paddle Right
            Paddle paddleR = new Paddle("PaddlR", 2, 10, ImageManager.Get("Paddle"), new Vector2(20, 120));
            paddleR.Transform.Position = new Vector3(770, 240 - 60, 0);
            this.gameObjects.Add(paddleR);

            //Collider Top
            GameObject wallTop = new GameObject("Top");
            wallTop.AddBehaviour(new Collider.Rect(wallTop,new Vector2(0,-10),new Vector2(800,10)));
            this.gameObjects.Add(wallTop);

            //Collider Bot
            GameObject wallBot = new GameObject("Bot");
            wallBot.Transform.Position = new Vector3(0, 480, 0);
            wallBot.AddBehaviour(new Collider.Rect(wallBot, new Vector2(0, 10), new Vector2(800, 10)));
            this.gameObjects.Add(wallBot);

            //Waiting Text
            waitingOponent = new GameObject("Waiting");
            waitingOponent.AddBehaviour(new SpriteRender(waitingOponent,ImageManager.Get("Waiting"),new Vector2(500,100)));
            waitingOponent.Transform.Position = new Vector3(150,300,0);
            this.gameObjects.Add(waitingOponent);

            //Clock Text
            Clock numberImg = new Clock("clockNumber",0);
            numberImg.SetActive(false);
            numberImg.Transform.Position = new Vector3(350, 300, 0);
            this.gameObjects.Add(numberImg); ;

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
            base.Actualize();
           
        }



    }
}
