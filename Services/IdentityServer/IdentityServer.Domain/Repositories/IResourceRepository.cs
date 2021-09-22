using BaseEntity.Domain.Repositories;
using IdentityServer.Domain.Models;
using Messages.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Repositories
{
    public interface IResourceRepository : IRepository<Resource>
    {
        Task<Maybe<Resource>> FindByNameAsync(string name);

        Task<List<Resource>> FindByScopeAsync(IEnumerable<string> scopeNames);
    }
}
