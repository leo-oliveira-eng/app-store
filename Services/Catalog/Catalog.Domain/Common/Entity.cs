using BaseEntity.Domain.Events;
using System;
using System.Collections.Generic;

namespace Catalog.Domain.Common
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public Guid Id { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public DateTime LastUpdate { get; private set; } = DateTime.Now;

        public DateTime? DeletedAt { get; private set; }

        public bool Deleted => DeletedAt.HasValue;

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);

        public void RemoveDomainEvent(DomainEvent domainEvent) => _domainEvents.Remove(domainEvent);

        public void ClearDomainEvents() => _domainEvents?.Clear();
    }
}
