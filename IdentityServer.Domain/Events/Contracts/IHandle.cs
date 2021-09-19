using System.Threading.Tasks;

namespace IdentityServer.Domain.Events.Contracts
{
    public interface IHandle<T> where T : IDomainEvent
    {
        Task Handle(T args);
    }
}
