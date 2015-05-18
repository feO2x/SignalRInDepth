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

            var persistentConnectionUrl = ConfigurationManager.AppSettings["persistentConnectionUrl"];

            MainWindow = new MainWindow { DataContext = new MainWindowViewModel(persistentConnectionUrl, SynchronizationContext.Current) };
            MainWindow.Show();
        }
    }
}