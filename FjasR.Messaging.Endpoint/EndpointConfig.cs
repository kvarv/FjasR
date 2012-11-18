using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace FjasR.Messaging.Endpoint
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            var connectionManager = GlobalHost.DependencyResolver.Resolve<IConnectionManager>();
            var container = new WindsorContainer();
            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IConnectionManager>().Instance(GlobalHost.DependencyResolver.Resolve<IConnectionManager>())
                );
            Configure.With().CastleWindsorBuilder(container);
        }
    }
}