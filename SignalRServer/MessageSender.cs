using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRServer
{
    public sealed class MessageSender
    {
        private bool _isRunning;
        private IHubContext _sampleHubContext;

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
            if (_sampleHubContext == null)
                _sampleHubContext = GlobalHost.ConnectionManager.GetHubContext<SampleHub>();

            _isRunning = true;

            Task.Run(new Action(SendMessagesPeriodically));
        }

        private async void SendMessagesPeriodically()
        {
            while (_isRunning)
            {
                _sampleHubContext.Clients.All.ReceiveMessage("Message from server");
                await Task.Delay(1000);
            }
        }
    }
}