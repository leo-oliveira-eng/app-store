using Catalog.Common.Configurations;
using Catalog.Infrastructure.Context.Contracts;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; }

        public MongoClient MongoClient { get; set; }

        public MongoContext(IOptions<MongoDbConfiguration> mongoDbConfiguration)
        {
            MongoClient = new MongoClient(mongoDbConfiguration.Value.ConnectionString);

            Database = MongoClient.GetDatabase(mongoDbConfiguration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
            => Database.GetCollection<T>(name);
    }
}
