using Catalog.Common.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Shared
{
    public class MongoDbBaseRepository
    {
        protected readonly IMongoDatabase Database;

        public MongoDbBaseRepository(IOptions<MongoDbConfiguration> mongoDbConfiguration)
        {
            var client = new MongoClient(mongoDbConfiguration.Value.ConnectionString);
            Database = client.GetDatabase(mongoDbConfiguration.Value.DatabaseName);
        }
    }
}
