using System;
using System.Collections.Generic;

namespace SignalRWpfClient.SampleData
{
    public sealed class MainWindowViewModelSampleData : IMainWindowViewModel
    {
        private readonly List<LogMessage> _logMessages = new List<LogMessage>
                                                         {
                                                             new LogMessage("Normal Log Message", new DateTime(2015, 5, 14, 12, 51, 10)),
                                                             new LogMessage("Warning message", new DateTime(2015, 5, 14, 12, 51, 0), SeverityLevel.Warning),
                                                             new LogMessage("Normal Log Message", new DateTime(2015, 5, 14, 12, 50, 34)),
                                                             new LogMessage("Severe Message with a longer text that should be readable", new DateTime(2015, 5, 14, 12, 50, 10), SeverityLevel.Error),
                                                             new LogMessage("Normal Log Message", new DateTime(2015, 5, 14, 12, 49, 30))
                                                         };

        private readonly NamedCommandStub _startOrStopConnectectionCommandStub = new NamedCommandStub { Name = "Start Connection" };
        private readonly NamedCommandStub _sendMessageCommandStub = new NamedCommandStub { Name = "Send Message" };

        public IReadOnlyList<LogMessage> LogMessages
        {
            get { return _logMessages; }
        }

        public INamedCommand StartOrStopConnectionCommand
        {
            get { return _startOrStopConnectectionCommandStub; }
        }

        public INamedCommand SendMessageCommand
        {
            get { return _sendMessageCommandStub; }
        }

        public string Message { get; set; }
        public bool CanCommandsExecute { get; set; }
    }
}