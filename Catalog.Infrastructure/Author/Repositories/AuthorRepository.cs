using Catalog.Common.Configurations;
using Catalog.Domain.Authors.Repositories;
using Catalog.Infrastructure.Author.Documents;
using Catalog.Infrastructure.Shared;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using Model = Catalog.Domain.Authors.Models;

namespace Catalog.Infrastructure.Author.Repositories
{
    public class AuthorRepository : MongoDbBaseRepository, IAuthorRepository
    {
        private readonly IMongoCollection<AuthorDocument> _colection;

        public AuthorRepository(IOptions<MongoDbConfiguration> mongoDbConfiguration)
            : base(mongoDbConfiguration)
        {
            _colection = Database.GetCollection<AuthorDocument>(mongoDbConfiguration.Value.CollectionNames.Author);
        }

        public async Task AddAsync(Model.Author author)
            => await _colection.InsertOneAsync(author.Adapt<AuthorDocument>());
    }
}
