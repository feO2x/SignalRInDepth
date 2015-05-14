using System.Collections.Generic;

namespace SignalRWpfClient
{
    public interface IMainWindowViewModel
    {
        IReadOnlyList<LogMessage> LogMessages { get; }
        INamedCommand StartOrStopConnectionCommand { get; }
        INamedCommand SendMessageCommand { get; }
        string Message { get; set; }
    }
}