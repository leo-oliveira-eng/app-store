using BaseEntity.Domain.Entities;
using System;

namespace IdentityServer.Domain.Models
{
    public sealed class ClientClaim : Entity
    {
        public long ClientId { get; private set; }

        public Client Client { get; private set; }

        public long ClaimId { get; private set; }

        public Claim Claim { get; private set; }

        [Obsolete(ConstructorObsoleteMessage, true)]
        ClientClaim() { }
    }
}
