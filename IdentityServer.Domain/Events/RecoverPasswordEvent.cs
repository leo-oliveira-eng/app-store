using IdentityServer.Domain.Events.Models;

namespace IdentityServer.Domain.Events
{
    public class RecoverPasswordEvent : DomainEvent<RecoverPasswordEventModel>
    {
        public RecoverPasswordEvent(RecoverPasswordEventModel model) : base(model) { }
    }
}
