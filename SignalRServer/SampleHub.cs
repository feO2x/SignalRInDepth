using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRServer
{
    public class SampleHub : Hub
    {
        public void SendMessage(string message)
        {
            ConsoleLogger.LogToConsole(string.Format("{0} send \"{1}\"", Context.ConnectionId, message));
        }

        public override Task OnConnected()
        {
            ConsoleLogger.LogToConsole(Context.ConnectionId + " connected", ConsoleColor.Green);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            ConsoleLogger.LogToConsole(Context.ConnectionId + " reconnected", ConsoleColor.Yellow);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
                ConsoleLogger.LogToConsole(Context.ConnectionId + " disconnected on purpose.", ConsoleColor.Red);
            else
                ConsoleLogger.LogToConsole(Context.ConnectionId + " lost connection.", ConsoleColor.Red);
            return base.OnDisconnected(stopCalled);
        }
    }
}
