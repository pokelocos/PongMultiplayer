using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class CreateRoom : Scene
    {
        public CreateRoom(string name, bool active = true) : base(name, active)
        {
            GameObject textIp = new GameObject("Text Ip");
            textIp.Transform.Position = new Vector3(295,120,0);
            Text text = new Text(textIp,new Vector2(0,0),FontManager.Get("MainFont"), Microsoft.Xna.Framework.Color.White,"IP: " + UtilitiesNetwork.GetIPs()[0]);
            textIp.AddBehaviour(text);
            gameObjects.Add(textIp);

            InputFieldPrefab inputPort = new InputFieldPrefab("TextField_Port", "TextField", FontManager.Get("MainFont"),new Vector2(240,40), new Vector3(280, 150, 0));
            inputPort.textField.text = "";
            inputPort.textField.exampleText = "Port...";
            gameObjects.Add(inputPort);

            ButtonPrefab createRoomButton = new ButtonPrefab("Button_CreateRoom", "CreateRoom", new Vector2(240, 100), new Vector3(280, 250, 0));
            createRoomButton.button.Action += () =>
            {
                try
                {
                    NetworkManager.StartServer(8000);
                    //MultiplayerManager.StartServer(Int16.Parse(inputPort.textField.text));
                }
                catch
                {
                    Console.WriteLine("Invalid port Number: " + 8000);
                    //Console.WriteLine("Invalid port Number: " + Int16.Parse(inputPort.textField.text));
                }

                SceneManager.LoadScene(new GameScene("GameScene"));
            };
            gameObjects.Add(createRoomButton);

    

            ButtonPrefab backButton = new ButtonPrefab("Button_Back","BackToMenu", new Vector2(200, 80), new Vector3(300, 360, 0));
            backButton.button.Action += () =>
            {
                SceneManager.LoadScene(new MainMenu("MainMenu"));
            };
            gameObjects.Add(backButton);

            
        }
    }
}
