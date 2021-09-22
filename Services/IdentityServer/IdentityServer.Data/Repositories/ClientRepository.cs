using IdentityServer.Data.Context;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Repositories;
using Infrastructure.Repositories;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IdentityServer.Data.Repositories
{
    public class ClientRepository : Repository<Client, IdentityContext>, IClientRepository
    {
        public ClientRepository(IdentityContext context) : base(context) { }

        public async Task<Maybe<Client>> FindByClientIdAsync(string clientId)
            => await DbSet.AsNoTracking()
                .Include(x => x.Claims)
                    .ThenInclude(x => x.Claim)
                .Include(x => x.Scopes)
                    .ThenInclude(x => x.Scope)
                .FirstOrDefaultAsync(c => c.ClientId.Equals(clientId));
    }
}
