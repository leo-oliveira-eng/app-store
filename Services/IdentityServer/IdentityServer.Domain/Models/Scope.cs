using BaseEntity.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IdentityServer.Domain.Models
{
    public sealed class Scope : Entity
    {
        public string Name { get; private set; }

        public ICollection<ClientScope> Clients { get; private set; } = new HashSet<ClientScope>();

        public ICollection<ScopeResource> Resources { get; set; } = new HashSet<ScopeResource>();

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Scope() { }
    }
}
