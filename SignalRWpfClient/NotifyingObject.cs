using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SignalRWpfClient
{
    public abstract class NotifyingObject : INotifyPropertyChanged, IRaisePropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        void IRaisePropertyChanged.OnPropertyChanged(string propertyName)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            OnPropertyChanged(propertyName);
        }
    }
}
