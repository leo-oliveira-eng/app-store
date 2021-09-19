namespace IdentityServer.Domain.Events.Contracts
{
    public interface IDomainEventHandler
    {
        void Raise<T>(T args) where T : IDomainEvent;
    }
}
