using System.Configuration;
using System.Threading;
using System.Windows;
using SignalRInDepth;

namespace SignalRWpfClient
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class App : Application
    {
        private const string HubName = "HubName";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var signalRUrl = AppConfig.GetSignalRServerUrl();
            var hubName = ConfigurationManager.AppSettings[HubName];

            MainWindow = new MainWindow { DataContext = new MainWindowViewModel(signalRUrl, hubName, SynchronizationContext.Current) };
            MainWindow.Show();
        }
    }
}