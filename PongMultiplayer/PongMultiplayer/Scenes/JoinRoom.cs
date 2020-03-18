using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class JoinRoom : Scene
    {
        public JoinRoom(string name, bool active = true) : base(name, active)
        {
            InputFieldPrefab inputPort = new InputFieldPrefab("TextField_Port","TextField", FontManager.Get("MainFont"), new Vector2(240, 40), new Vector3(280, 100, 0));
            inputPort.textField.text = "";
            inputPort.textField.exampleText = "Port...";
            gameObjects.Add(inputPort);

            InputFieldPrefab addresPort = new InputFieldPrefab("TextField_Port","TextField", FontManager.Get("MainFont"), new Vector2(240, 40), new Vector3(280, 150, 0));
            addresPort.textField.text = "";
            addresPort.textField.exampleText = "Addres Ip...";
            gameObjects.Add(addresPort);

            ButtonPrefab joinRoomButton = new ButtonPrefab("Button_CreateRoom","JoinRoom", new Vector2(240, 100), new Vector3(280, 250, 0));
            joinRoomButton.button.Action += () =>
            {
                try
                {
                    if(Globals.DebugNetWorkMode)
                    {
                        NetworkManager.port = 8000;
                        NetworkManager.address = UtilitiesNetwork.GetIPs()[0];
                    }
                    else
                    {
                        NetworkManager.port = 8000;
                        var address = addresPort.textField.text;
                        NetworkManager.address = IPAddress.Parse(address);
                    }
                    SceneManager.LoadScene(new GameScene("GameScene"));
                    NetworkManager.OnErrorToTryConect = () => 
                    {
                        SceneManager.LoadScene(new MainMenu("MainMenu"));
                    };
                    NetworkManager.ConectToServer();

                }
                catch
                {
                    Console.WriteLine("Invalid port or addres number.");
                }
            };
            gameObjects.Add(joinRoomButton);


            ButtonPrefab backButton = new ButtonPrefab("Button_Back", "BackToMenu", new Vector2(200, 80), new Vector3(300, 360, 0));
            backButton.button.Action += () =>
            {
                SceneManager.LoadScene(new MainMenu("MainMenu"));
            };
            gameObjects.Add(backButton);

        }
    }
}
