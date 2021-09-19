using BaseEntity.Domain.Entities;
using System;

namespace IdentityServer.Domain.Models
{
    public sealed class ScopeResource : Entity
    {
        public long ScopeId { get; private set; }

        public Scope Scope { get; private set; }

        public long ResourceId { get; private set; }

        public Resource Resource { get; private set; }

        [Obsolete(ConstructorObsoleteMessage, true)]
        private ScopeResource() { }
    }
}
