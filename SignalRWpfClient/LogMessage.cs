using System;

namespace SignalRWpfClient
{
    public sealed class LogMessage
    {
        private readonly DateTime _occuredAt;
        private readonly string _message;
        private readonly SeverityLevel _level;


        public LogMessage(string message, SeverityLevel level = SeverityLevel.Normal)
            : this(message, DateTime.Now, level)
        {
            
        }

        public LogMessage(string message, DateTime occuredAt, SeverityLevel level = SeverityLevel.Normal)
        {
            _occuredAt = occuredAt;
            _message = message;
            _level = level;
        }

        public DateTime OccuredAt
        {
            get { return _occuredAt; }
        }

        public string Message
        {
            get { return _message; }
        }

        public SeverityLevel Level
        {
            get { return _level; }
        }
    }
}