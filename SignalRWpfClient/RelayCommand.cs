using System;

namespace SignalRWpfClient
{
    public sealed class RelayCommand : NotifyingObject, INamedCommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private string _name;

        public RelayCommand(Action execute, string name)
            : this(execute, CanAlwayExecute, name)
        {
            
        }

        public RelayCommand(Action execute, Func<bool> canExecute, string name)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            if (canExecute == null) throw new ArgumentNullException("canExecute");
            if (name == null) throw new ArgumentNullException("name");

            _execute = execute;
            _canExecute = canExecute;
            _name = name;
        }

        private static bool CanAlwayExecute()
        {
            return true;
        }

        public string Name
        {
            get { return _name; }
            set { this.SetValueIfDifferent(ref _name, value); }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}