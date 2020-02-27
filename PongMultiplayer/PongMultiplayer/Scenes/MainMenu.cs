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
            GameObject background = new GameObject("Background");
            background.AddBehaviour(new SpriteRender(background, "Background", new Vector2(800, 480)));
            gameObjects.Add(background);

            GameObject title = new GameObject("Title");
            title.AddBehaviour(new SpriteRender(title, "Title",new Vector2(230,160)));
            title.Transform.Position = new Vector3(400 - 115, 40, 0);
            gameObjects.Add(title);

            ButtonPrefab makeRoom = new ButtonPrefab("Button_MakeRoom", "CreateRoom",new Vector2(240,100), new Vector3(280, 200, 0));
            makeRoom.button.Action += () =>
            {
                SceneManager.LoadScene(new CreateRoom("CreateRoom"));
            };
            gameObjects.Add(makeRoom);

            ButtonPrefab joinRoom = new ButtonPrefab("Button_JoinRoom", "JoinRoom", new Vector2(240, 100), new Vector3(280, 320, 0));
            joinRoom.button.Action += () =>
            {
                SceneManager.LoadScene(new JoinRoom("JoinRoom"));
            };
            gameObjects.Add(joinRoom);


        }

        
    }
}
