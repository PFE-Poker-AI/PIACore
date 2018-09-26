using System;
using dotenv.net;
using PIACore.AI;
using PIACore.Web;

namespace PIACore.Kernel
{
    /// <summary>
    /// Kernel of the project.
    /// </summary>
    public class Kernel
    {
        /// <summary>
        /// Main class.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            DotEnv.Config();
            
            var game = new Game<AlwaysCallAIManager>();
            
            game.Run();
        }
    }
}