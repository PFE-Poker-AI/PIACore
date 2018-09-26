using System;
using System.Diagnostics;

namespace PIACore.Helpers
{
    /// <summary>
    /// Logger is a helper to write styled console message respecting the logger level norm.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Log a message on a Debug level.
        /// </summary>
        /// <param name="input">The message to log</param>
        public static void Debug(string input)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        
        /// <summary>
        /// Log a message on a Info level.
        /// </summary>
        /// <param name="input">The message to log</param>
        public static void Info(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        
        /// <summary>
        /// Log a message on a Warning level.
        /// </summary>
        /// <param name="input">The message to log</param>
        public static void Warning(string input)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(input);
            Console.ResetColor();
        }
        
        /// <summary>
        /// Log a message on a Error level.
        /// </summary>
        /// <param name="input">The message to log</param>
        public static void Error(string input)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}