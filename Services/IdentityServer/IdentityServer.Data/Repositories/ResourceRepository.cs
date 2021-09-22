using IdentityServer.Data.Context;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Repositories;
using Infrastructure.Repositories;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Data.Repositories
{
    public class ResourceRepository : Repository<Resource, IdentityContext>, IResourceRepository
    {
        public ResourceRepository(IdentityContext context) : base(context) { }

        public async Task<Maybe<Resource>> FindByNameAsync(string name)
            => await DbSet.FirstOrDefaultAsync(resource => resource.Name.Equals(name));

        public async Task<List<Resource>> FindByScopeAsync(IEnumerable<string> scopeNames)
            => await DbSet.Where(resource => resource.Scopes.Any(scope => scopeNames.Contains(scope.Scope.Name))).ToListAsync();
    }
}
