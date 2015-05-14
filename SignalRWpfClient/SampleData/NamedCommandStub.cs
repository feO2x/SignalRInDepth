using System;
using System.Windows.Input;

namespace SignalRWpfClient.SampleData
{
    public sealed class NamedCommandStub : INamedCommand
    {
        public NamedCommandStub()
        {
            CanExecute = true;
        }
        
        public bool CanExecute { get; set; }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public void Execute(object parameter)
        {
            
        }

        public event EventHandler CanExecuteChanged;

        public string Name { get; set; }
    }
}