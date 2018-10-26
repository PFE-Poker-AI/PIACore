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
        /// <param name="slug">The optional slug (identify the AI)</param>
        public static void Debug(string input, string slug = "")
        {
            WriteMessage(input, ConsoleColor.Gray, slug);
        }

        /// <summary>
        /// Log a message on a Info level.
        /// </summary>
        /// <param name="input">The message to log</param>
        /// <param name="slug">The optional slug (identify the AI)</param>
        public static void Info(string input, string slug = "")
        {
            WriteMessage(input, ConsoleColor.Green, slug);
        }

        /// <summary>
        /// Log a message on a Warning level.
        /// </summary>
        /// <param name="input">The message to log</param>
        /// <param name="slug">The optional slug (identify the AI)</param>
        public static void Warning(string input, string slug = "")
        {
            WriteMessage(input, ConsoleColor.Yellow, slug);
        }

        /// <summary>
        /// Log a message on a Error level.
        /// </summary>
        /// <param name="input">The message to log</param>
        /// <param name="slug">The optional slug (identify the AI)</param>
        public static void Error(string input, string slug = "")
        {
            WriteMessage(input, ConsoleColor.Red, slug);
        }

        /// <summary>
        /// Writes the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="slug"></param>
        private static void WriteMessage(string message, ConsoleColor foregroundColor,string slug)
        {
            
            if (!string.IsNullOrEmpty(slug))
            {
                Console.Write(slug);
                Console.ForegroundColor = foregroundColor;
                Console.Write(" -> ");
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}