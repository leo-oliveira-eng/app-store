using IdentityServer.Domain.Events.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Events
{
    public class DomainEventHandler : IDomainEventHandler
    {
        private IServiceProvider ServiceProvider { get; }

        public DomainEventHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Raise<T>(T args) where T : IDomainEvent
        {
            foreach (var handler in ServiceProvider.GetServices<IHandle<T>>())
                new Task(() => handler.Handle(args)).Start();
        }
    }
}
