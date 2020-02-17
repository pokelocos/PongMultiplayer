using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class MainMenu : Scene
    {
        public MainMenu(string name, bool active = true) : base(name, active)
        {
            GameObject title = new GameObject("Title");
            title.AddBehaviour(new SpriteRender(title, ImageManager.Get("square"),new Vector2(600,160)));
            title.Transform.Position = new Vector3(100, 40, 0);
            gameObjects.Add(title);

            ButtonPrefab makeRoom = new ButtonPrefab("Button_MakeRoom", ImageManager.Get("CreateRoom"),new Vector2(240,100), new Vector3(280, 240, 0));
            makeRoom.button.Action += () =>
            {
                SceneManager.LoadScene(new CreateRoom("CreateRoom"));
            };
            gameObjects.Add(makeRoom);

            ButtonPrefab joinRoom = new ButtonPrefab("Button_JoinRoom", ImageManager.Get("JoinRoom"), new Vector2(240, 100), new Vector3(280, 360, 0));
            joinRoom.button.Action += () =>
            {
                SceneManager.LoadScene(new JoinRoom("JoinRoom"));
            };
            gameObjects.Add(joinRoom);


        }

        
    }
}
