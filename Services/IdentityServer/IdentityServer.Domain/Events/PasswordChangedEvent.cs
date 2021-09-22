using IdentityServer.Domain.Events.Models;

namespace IdentityServer.Domain.Events
{
    public class PasswordChangedEvent : DomainEvent<PasswordChangedEventModel>
    {
        public PasswordChangedEvent(PasswordChangedEventModel model) : base(model) { }
    }
}
