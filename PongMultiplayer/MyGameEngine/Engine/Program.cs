using System;

namespace MyEngine
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var engine = new EngineGame();
            SceneManager.SetEngine(engine);
            using (var game = engine)
            {
                game.Run();
            }
        }
    }
#endif
}
