using FjasR.PushNotifactions;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using NServiceBus;

namespace FjasR.Messaging.Endpoint
{
    public class MyEventHandler : IHandleMessages<IUpdatedEvent>
    {
        private readonly IConnectionManager _connectionManager;

        public void Handle(IUpdatedEvent message)
        {
            IHubContext hubContext = _connectionManager.GetHubContext<MessageOverviewHub>();
            hubContext.Clients.All.invoke(new MessageUpdated { Time = message.Time });
        }

        public MyEventHandler(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
    }
}