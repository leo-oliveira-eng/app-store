using BaseEntity.Domain.Repositories;
using IdentityServer.Domain.Models;
using Messages.Core;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Maybe<Client>> FindByClientIdAsync(string clientId);
    }
}
