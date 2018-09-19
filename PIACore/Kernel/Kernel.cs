using System;
using dotenv.net;
using PIACore.Web;

namespace PIACore.Kernel
{
    public class Kernel
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DotEnv.Config();
            
            ApiConnector connector = new ApiConnector();

            Console.Write(connector.getId());
        }
    }
}