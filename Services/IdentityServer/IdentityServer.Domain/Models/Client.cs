using BaseEntity.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IdentityServer.Domain.Models
{
    public sealed class Client : Entity
    {
        public string ClientId { get; private set; }

        public string ClientSecret { get; private set; }

        public ICollection<ClientScope> Scopes { get; private set; } = new HashSet<ClientScope>();

        public ICollection<ClientClaim> Claims { get; private set; } = new HashSet<ClientClaim>();

        [Obsolete(ConstructorObsoleteMessage, true)]
        private Client() { }
    }
}
