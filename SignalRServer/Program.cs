using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Configuration;

namespace SignalRServer
{
    internal class Program
    {
        private static void Main()
        {
            var targetPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            var baseAddress = string.Format("http://{0}:{1}", Environment.MachineName, targetPort);
            var messageSender = new MessageSender();

            StartService:
            using (WebApp.Start(baseAddress, Configure))
            {
                Console.WriteLine("SignalR service running on " + baseAddress);
                Console.WriteLine("Press ENTER to shutdown the service");
                Console.WriteLine("Press S to send messages to connected clients periodically");

                while (true)
                {
                    var keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                        break;
                    if (keyInfo.Key == ConsoleKey.S)
                        messageSender.StartOrStop();
                }
            }

            Console.WriteLine("Service shut down.");
            Console.WriteLine("Press ENTER to restart the service");
            Console.WriteLine("Press ESCAPE to quit");

            while (true)
            {
                var keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    goto StartService;
                }
                if(keyInfo.Key == ConsoleKey.Escape)
                    break;
            }
        }

        private static void Configure(IAppBuilder appBuilder)
        {
            appBuilder.MapSignalR();
        }
    }
}
