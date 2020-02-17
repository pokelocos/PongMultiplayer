using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class JoinRoom : Scene
    {
        public JoinRoom(string name, bool active = true) : base(name, active)
        {
            InputFieldPrefab inputPort = new InputFieldPrefab("TextField_Port", ImageManager.Get("square"), FontManager.Get("MainFont"), new Vector2(100, 100), new Vector3(200, 200, 0));
            inputPort.textField.text = "";
            inputPort.textField.exampleText = "Port...";
            gameObjects.Add(inputPort);

            InputFieldPrefab addresPort = new InputFieldPrefab("TextField_Addres", ImageManager.Get("square"), FontManager.Get("MainFont"), new Vector2(100, 100), new Vector3(200, 0, 0));
            addresPort.textField.text = "";
            addresPort.textField.exampleText = "Addres Ip...";
            gameObjects.Add(addresPort);

            ButtonPrefab joinRoomButton = new ButtonPrefab("Button_CreateRoom", ImageManager.Get("square"), new Vector2(100, 100), new Vector3(200, 400, 0));
            joinRoomButton.button.Action += () =>
            {
                try
                {
                    MultiplayerManager.port = 8000;
                    MultiplayerManager.ConectToServer();
                    SceneManager.LoadScene(new GameScene("GameScene"));
                    SceneManager.UnloadScene("CreateRoom");
                }
                catch
                {
                    Console.WriteLine("Invalid port or addres number.");
                }
            };
            gameObjects.Add(joinRoomButton);

            
            ButtonPrefab backButton = new ButtonPrefab("Button_Back", ImageManager.Get("square"), new Vector2(100, 100), new Vector3(400, 400, 0));
            backButton.button.Action += () =>
            {
                SceneManager.LoadScene(new MainMenu("MainMenu"));
            };
            gameObjects.Add(backButton);

        }
    }
}
