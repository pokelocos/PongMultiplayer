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
            InputFieldPrefab inputPort = new InputFieldPrefab("TextField_Port", ImageManager.Get("square"), FontManager.Get("MainFont"),new Vector2(100,100), new Vector3(200, 200, 0));
           inputPort.textField.text = "";
            inputPort.textField.exampleText = "Port...";
            gameObjects.Add(inputPort);

            ButtonPrefab createRoomButton = new ButtonPrefab("Button_CreateRoom", ImageManager.Get("square"), new Vector2(100, 100), new Vector3(200, 400, 0));
            createRoomButton.button.Action += () =>
            {
                try
                {
                    MultiplayerManager.StartServer(6000);
                    SceneManager.LoadScene(new GameScene("GameScene"));
                }
                catch
                {
                    Console.WriteLine("Invalid port number " + inputPort.textField.text);
                }
            };
            gameObjects.Add(createRoomButton);

    

            ButtonPrefab backButton = new ButtonPrefab("Button_Back", ImageManager.Get("square"), new Vector2(100, 100), new Vector3(400, 400, 0));
            backButton.button.Action += () =>
            {
                SceneManager.LoadScene(new MainMenu("MainMenu"));
                SceneManager.UnloadScene("CreateRoom");
            };
            gameObjects.Add(backButton);

            
        }
    }
}
