using IdentityServer.Domain.Events.Contracts;
using IdentityServer.Domain.Events.Models;
using System;

namespace IdentityServer.Domain.Events
{
    public abstract class DomainEvent<TModel> : IDomainEvent where TModel : EventModel
    {
        public DomainEvent(TModel model)
        {
            Model = model;
        }

        public DateTime DateOccurred => DateTime.Now;

        public TModel Model { get; private set; }
    }
}
