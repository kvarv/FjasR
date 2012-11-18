using System;
using Microsoft.AspNet.SignalR.Hosting.Self;
using Microsoft.AspNet.SignalR.Hubs;
using NServiceBus;

namespace FjasR.Messaging.Endpoint
{
    public class ServerEndpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            const string url = "http://localhost:8081/";
            var server = new Server(url);

            // Map the default hub url (/signalr)
            server.MapHubs();

            // Start the server
            server.Start();


            Console.WriteLine("Push notification server running on {0}", url);

            Console.WriteLine("Press 'Enter' to publish a message.To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
                IUpdatedEvent commandMessage;

                commandMessage = Bus.CreateInstance<IUpdatedEvent>();

                commandMessage.Time = DateTime.Now;

                Bus.SendLocal(commandMessage);

                Console.WriteLine("==========================================================================");
            }
        }

        public void Stop()
        {
        }
    }

    public class MessageOverviewHub : Hub
    {
    }
}