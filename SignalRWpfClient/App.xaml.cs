using System.Configuration;
using System.Threading;
using System.Windows;

namespace SignalRWpfClient
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var signalRUrl = ConfigurationManager.AppSettings["SignalRUrl"];
            var hubName = ConfigurationManager.AppSettings["HubName"];

            MainWindow = new MainWindow { DataContext = new MainWindowViewModel(signalRUrl, hubName, SynchronizationContext.Current) };
            MainWindow.Show();
        }
    }
}