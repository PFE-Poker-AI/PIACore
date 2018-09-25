using System;
using System.Diagnostics;

namespace PIACore.Log
{
    public class Logger
    {
        public static void Debug(string input)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        
        
        
        public static void Info(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        
        public static void Warning(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        
        public static void Error(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}