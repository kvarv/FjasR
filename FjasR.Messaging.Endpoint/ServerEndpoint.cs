using System;
using FjasR.PushNotifactions;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin.Hosting;
using NServiceBus;
using Owin;

namespace FjasR.Messaging.Endpoint
{
    public class ServerEndpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }
        private readonly IConnectionManager _connectionManager;

        public ServerEndpoint(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public void Run()
        {
            string url = "http://localhost:9081";

            using (WebApplication.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.WriteLine("Push notification server running on {0}", url);

                Console.WriteLine("Press 'Enter' to publish a message.To exit, Ctrl + C");

                while (Console.ReadLine() != null)
                {
                    //IUpdatedEvent commandMessage;

                    //commandMessage = Bus.CreateInstance<IUpdatedEvent>();

                    //commandMessage.Time = DateTime.Now;

                    //Bus.SendLocal(commandMessage);

                    IHubContext hubContext = _connectionManager.GetHubContext<MessageOverviewHub>();
                    hubContext.Clients.All.invoke(new MessageUpdated { Time = DateTime.Now });

                    Console.WriteLine("==========================================================================");
                }
            }

            
        }

        public void Stop()
        {
        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapHubs("/signalr");
        }
    }

    public class MessageOverviewHub : Hub
    {
    }
}