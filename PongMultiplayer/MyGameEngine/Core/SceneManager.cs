using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEngine
{
    public static class SceneManager
    {
        private static EngineGame engine;

        internal static void SetEngine(EngineGame engine)
        {
            SceneManager.engine = engine;
        }

        public static void LoadScene(Scene scene)
        {
            InputManager.UnSuscribeFromScenes(engine.scenes);
            engine.scenes = new List<Scene>();
            engine.scenes.Add(scene);
        }

        public static void LoadScenes(Scene[] scenes)
        {
            InputManager.UnSuscribeFromScenes(engine.scenes);
            engine.scenes = new List<Scene>();
            foreach (var scene in scenes)
                engine.scenes.Add(scene);
        }

        public static void LoadSyncScene(Scene scene)
        {
            engine.scenes.Add(scene);
        }

        public static void LoadSyncScenes(Scene[] scenes)
        {
            foreach (var scene in scenes)
                engine.scenes.Add(scene);
        }

        public static void UnloadScene(String name)
        {
            Scene toRemove = null;
            foreach (var scene in engine.scenes)
            {
                if(scene.name.Equals(name))
                {
                    toRemove = scene;
                    break;
                }
            }

            if(toRemove != null)
            {
                InputManager.UnSuscribeFromScene(toRemove);
                engine.scenes.Remove(toRemove);
            }
            else
            {
                Console.WriteLine("There is no scene loaded with the name: '" + name + "'");
            }
        }
    }
}
