using IdentityServer.Domain.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Services
{
    public class ClientStore : IClientStore
    {
        private IClientRepository ClientRepository { get; }

        public ClientStore(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await ClientRepository.FindByClientIdAsync(clientId);

            if (!client.HasValue)
                return new Client();

            return new Client
            {
                ClientId = client.Value.ClientId,
                AccessTokenLifetime = 2592000,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                ClientSecrets = { new Secret(client.Value.ClientSecret.Sha256()) },
                AllowOfflineAccess = true,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AllowedScopes = client.Value.Scopes.Select(clientScope => clientScope.Scope)
                    .Select(scope => scope.Name)
                    .ToList(),
                Claims = client.Value.Claims.Select(clientClaim => clientClaim.Claim)
                    .Select(claim => new System.Security.Claims.Claim(claim.Type, claim.Value))
                    .ToList(),
                ClientClaimsPrefix = ""
            };
        }
    }
}
