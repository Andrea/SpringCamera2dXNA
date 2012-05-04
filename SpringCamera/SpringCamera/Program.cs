using System;

namespace SpringCamera
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ShipGame game = new ShipGame())
            {
                game.Run();
            }
        }
    }
#endif
}

