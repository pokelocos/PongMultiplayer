using MyEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongMultiplayer
{
    class OtherMatch : Scene
    {
        public OtherMatch(string name,bool winBool, bool active = true): base(name, active)
        {
            GameObject panel = new GameObject("panel");
            panel.Transform.Position = new Vector3(Globals.widthScreen / 2 - 150, 15, 0);

            if (!winBool)
            {
;               panel.AddBehaviour(new SpriteRender(panel,"YouWin", new Vector2(300, 200)));
            }
            else
            {
                panel.AddBehaviour(new SpriteRender(panel, "YouLose", new Vector2(300, 200)));
            }

            this.gameObjects.Add(panel);

            /*
            ButtonPrefab retry = new ButtonPrefab("Retry", "Retry", new Vector2(200, 100), new Vector3(Globals.widthScreen/2 - 100, 240, 0));
            retry.button.Action = () =>
            {
                SceneManager.LoadScene(new GameScene("GameScene"));
            };
            this.gameObjects.Add(retry);
            */

            ButtonPrefab back = new ButtonPrefab("Back", "Back2", new Vector2(200, 100), new Vector3(Globals.widthScreen / 2 - 100, 240, 0));
            //ButtonPrefab back = new ButtonPrefab("Back", "Back2", new Vector2(200, 100), new Vector3(Globals.widthScreen / 2 - 100, 360, 0));
            back.button.Action = () =>
            {
                NetworkManager.Disconect();
                SceneManager.LoadScene(new MainMenu("MainMenu"));
            };
            this.gameObjects.Add(back);
            
        }
    }
}
