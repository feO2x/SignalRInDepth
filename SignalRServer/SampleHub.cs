using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRServer
{
    public class SampleHub : Hub
    {
        public void SendMessage(string message)
        {
            LogToConsole(string.Format("{0} send \"{1}\"", Context.ConnectionId, message));
        }

        public override Task OnConnected()
        {
            LogToConsole(Context.ConnectionId + " connected", ConsoleColor.Green);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            LogToConsole(Context.ConnectionId + " reconnected", ConsoleColor.Yellow);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
                LogToConsole(Context.ConnectionId + " disconnected on purpose.", ConsoleColor.Red);
            else
                LogToConsole(Context.ConnectionId + " lost connection.", ConsoleColor.DarkRed);
            return base.OnDisconnected(stopCalled);
        }

        private static void LogToConsole(string message, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            LogToConsole(message);
            Console.ForegroundColor = previousColor;
        }

        private static void LogToConsole(string message)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + message);
        }
    }
}
