using System;

namespace SignalRServer
{
    public static class ConsoleLogger
    {
        public static void LogToConsole(string message, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            LogToConsole(message);
            Console.ForegroundColor = previousColor;
        }

        public static void LogToConsole(string message)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + message);
        }
    }
}