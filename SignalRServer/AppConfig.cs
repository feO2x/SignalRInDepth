using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SignalRInDepth
{
    public static class AppConfig
    {
        private const string ServerAddress = "ServerAddress";
        private const string Port = "Port";
        private const string UseMachineName = "use MachineName";

        public static string GetSignalRServerUrl()
        {
            var serverAddress = ConfigurationManager.AppSettings[ServerAddress];
            var signalRUrl = serverAddress == UseMachineName ? CreateUrlWithMachineName() : CreateCustomUrl(serverAddress);
            return signalRUrl;
        }

        private static string CreateUrlWithMachineName()
        {
            return string.Format("http://{0}:{1}/", Environment.MachineName, ConfigurationManager.AppSettings[Port]);
        }

        private static string CreateCustomUrl(string serverAddress)
        {
            return string.Format("{0}:{1}/", serverAddress, ConfigurationManager.AppSettings[Port]);
        }
    }
}