using MongoDB.Driver;

namespace Catalog.Infrastructure.Context.Contracts
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
