using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace SignalRWpfClient
{
    public sealed class MainWindowViewModel : NotifyingObject, IMainWindowViewModel
    {
        private readonly string _serviceUrl;
        private readonly string _hubName;
        private readonly ObservableCollection<LogMessage> _logMessages = new ObservableCollection<LogMessage>();
        private readonly RelayCommand _startOrStopConnectionCommand;
        private bool _canCommandsExecute = true;
        private readonly RelayCommand _sendMessageCommand;
        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;
        private int _numberOfLogMessages = 300;
        private string _message = "Type your message here";
        private const string StartConnectionText = "Start Connection";
        private const string StopConnectionText = "Stop Connection";
        private const string SendMessageText = "Send Message";

        public MainWindowViewModel(string serviceUrl, string hubName)
        {
            if (serviceUrl == null) throw new ArgumentNullException("serviceUrl");
            if (hubName == null) throw new ArgumentNullException("hubName");

            _serviceUrl = serviceUrl;
            _hubName = hubName;

            Func<bool> canCommandExecute = () => _canCommandsExecute;
            _startOrStopConnectionCommand = new RelayCommand(StartOrStopConnection,
                                                             canCommandExecute,
                                                             StartConnectionText);
            _sendMessageCommand = new RelayCommand(SendMessage,
                                                   canCommandExecute,
                                                   SendMessageText);
        }

        private async void StartOrStopConnection()
        {
            if (_hubConnection == null)
                await StartConnection();
            else
                StopConnection();
        }

        private async Task StartConnection()
        {
            _hubConnection = new HubConnection(_serviceUrl);
            _hubProxy = _hubConnection.CreateHubProxy(_hubName);

            ChangeCanCommandsExecute();
            try
            {
                await _hubConnection.Start();
                _startOrStopConnectionCommand.Name = StopConnectionText;
                CreateLogMessage("You successfully connected to " + _serviceUrl);
            }
            catch (HttpRequestException ex)
            {
                CreateLogMessage(string.Format("The hub connection could not be established ({0})",
                                               ex.GetType().FullName),
                                 SeverityLevel.Error);
                _hubConnection = null;
                _hubProxy = null;
            }
            finally
            {
                ChangeCanCommandsExecute();
            }
        }

        private void ChangeCanCommandsExecute()
        {
            _canCommandsExecute = !_canCommandsExecute;
            _startOrStopConnectionCommand.RaiseCanExecuteChanged();
            _sendMessageCommand.RaiseCanExecuteChanged();
        }

        private void StopConnection()
        {
            _hubConnection.Stop();
            _hubConnection = null;
            _hubProxy = null;
            _startOrStopConnectionCommand.Name = StartConnectionText;
        }

        private async void SendMessage()
        {
            if (_hubProxy == null)
            {
                CreateLogMessage("You cannot send a message because the hub connection is not initialized", SeverityLevel.Warning);
                return;
            }

            try
            {
                await _hubProxy.Invoke("SendMessage", _message);
                CreateLogMessage("You successfully sent: " + _message);
            }
            catch (Exception ex)
            {
                CreateLogMessage(string.Format("Message could not be sent ({0})", ex.GetType().FullName), SeverityLevel.Error);
            }
        }

        private void CreateLogMessage(string message, SeverityLevel level = SeverityLevel.Normal)
        {
            var logMessage = new LogMessage(message, level);
            _logMessages.Insert(0, logMessage);

            while (_logMessages.Count > _numberOfLogMessages)
                _logMessages.RemoveAt(_logMessages.Count - 1);
        }

        public IReadOnlyList<LogMessage> LogMessages
        {
            get { return _logMessages; }
        }

        public INamedCommand StartOrStopConnectionCommand
        {
            get { return _startOrStopConnectionCommand; }
        }

        public INamedCommand SendMessageCommand
        {
            get { return _sendMessageCommand; }
        }

        public int NumberOfLogMessages
        {
            get { return _numberOfLogMessages; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value");

                _numberOfLogMessages = value;
            }
        }

        public string Message
        {
            get { return _message; }
            set { this.SetValueIfDifferent(ref _message, value); }
        }
    }
}