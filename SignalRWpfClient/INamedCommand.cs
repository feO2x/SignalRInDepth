using System.Windows.Input;

namespace SignalRWpfClient
{
    public interface INamedCommand : ICommand
    {
        string Name { get; }
    }
}