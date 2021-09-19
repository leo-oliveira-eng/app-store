using IdentityServer.Domain.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Services
{
    public class ResourceStore : IResourceStore
    {
        IResourceRepository ResourceRepository { get; }

        public ResourceStore(IResourceRepository resourceRepository)
        {
            ResourceRepository = resourceRepository ?? throw new ArgumentNullException(nameof(resourceRepository));
        }


        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var resource = await ResourceRepository.FindByNameAsync(name);

            if (!resource.HasValue)
                return new ApiResource();

            return new ApiResource(resource.Value.Name, resource.Value.DisplayName);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var resources = await ResourceRepository.FindByScopeAsync(scopeNames);

            if (resources.Count == 0)
                return new List<ApiResource>();

            return resources.Select(resource => new ApiResource(resource.Name, resource.DisplayName));
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
            => Task.Run<IEnumerable<IdentityResource>>(() => new List<IdentityResource>());

        public async Task<Resources> GetAllResourcesAsync()
        {
            var resources = await ResourceRepository.GetAllAsync();

            return new Resources(new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            },
            resources.Select(resource => new ApiResource(resource.Name, resource.DisplayName)));
        }
    }
}
