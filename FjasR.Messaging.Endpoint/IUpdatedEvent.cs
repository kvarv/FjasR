using System;
using NServiceBus;

namespace FjasR.Messaging.Endpoint
{
    public interface IUpdatedEvent : IMessage
    {
        DateTime Time { get; set; }
    }
}
