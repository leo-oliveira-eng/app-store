using BaseEntity.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IdentityServer.Domain.Models
{
    public sealed class Resource : Entity
    {
        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public ICollection<ScopeResource> Scopes { get; set; } = new HashSet<ScopeResource>();

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Resource() { }
    }
}
