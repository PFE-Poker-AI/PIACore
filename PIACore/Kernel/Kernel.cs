using System;
using dotenv.net;
using PIACore.AI;
using PIACore.Web;

namespace PIACore.Kernel
{
    public class Kernel
    {
        public static void Main(string[] args)
        {
            DotEnv.Config();
            
            //var game = new Game<AlwaysCallAIManager>();
            
            
            var connector = new ApiConnector();

            Console.WriteLine(connector.currentTables());



        }
    }
}