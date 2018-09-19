using System;
using dotenv.net;

namespace PIACore.Kernel
{
    public class Kernel
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            DotEnv.Config();

            Console.Write(Environment.GetEnvironmentVariable("REMOTE_API_URL"));
        }
    }
}