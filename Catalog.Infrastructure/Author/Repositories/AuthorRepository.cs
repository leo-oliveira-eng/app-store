using Catalog.Common.Configurations;
using Catalog.Domain.Authors.Repositories;
using Catalog.Infrastructure.Shared;
using Microsoft.Extensions.Options;

namespace Catalog.Infrastructure.Author.Repositories
{
    public class AuthorRepository : MongoDbBaseRepository, IAuthorRepository
    {
        public AuthorRepository(IOptions<MongoDbConfiguration> mongoDbConfiguration) : base(mongoDbConfiguration) { }
    }
}
