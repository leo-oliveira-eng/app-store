using BaseEntity.Domain.Entities;
using System;

namespace IdentityServer.Domain.Models
{
    public sealed class ClientScope : Entity
    {
        public long ClientId { get; private set; }

        public Client Client { get; private set; }

        public long ScopeId { get; private set; }

        public Scope Scope { get; private set; }

        [Obsolete(ConstructorObsoleteMessage, true)]
        private ClientScope() { }
    }
}
