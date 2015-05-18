using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRServer
{
    public sealed class MessageSender
    {
        private bool _isRunning;
        private IPersistentConnectionContext _persistentConnectionContext;

        public void StartOrStop()
        {
            if (_isRunning)
            {
                _isRunning = false;
                return;
            }

            Start();
        }

        private void Start()
        {
            if (_persistentConnectionContext == null)
                _persistentConnectionContext = GlobalHost.ConnectionManager.GetConnectionContext<SignalRConnectionEndPoint>();

            _isRunning = true;

            Task.Run(new Action(SendMessagesPeriodically));
        }

        private async void SendMessagesPeriodically()
        {
            while (_isRunning)
            {
                await _persistentConnectionContext.Connection.Broadcast("Message from server");
                await Task.Delay(1000);
            }
        }
    }
}