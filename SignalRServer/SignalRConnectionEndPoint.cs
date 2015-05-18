using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRServer
{
    public class SignalRConnectionEndPoint : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            ConsoleLogger.LogToConsole(connectionId + " connected", ConsoleColor.Green);
            return base.OnConnected(request, connectionId);
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            ConsoleLogger.LogToConsole(connectionId + " reconnected", ConsoleColor.Yellow);
            return base.OnReconnected(request, connectionId);
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            ConsoleLogger.LogToConsole(string.Format("{0} send \"{1}\"", connectionId, data));
            return base.OnReceived(request, connectionId, data);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            if (stopCalled)
                ConsoleLogger.LogToConsole(connectionId + " disconnected on purpose.", ConsoleColor.Red);
            else
                ConsoleLogger.LogToConsole(connectionId + " lost connection.", ConsoleColor.Red);
            return base.OnDisconnected(request, connectionId, stopCalled);
        }
    }
}
