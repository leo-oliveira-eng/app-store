namespace Catalog.Common.Configurations
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public MongoDbCollectionNames CollectionNames { get; set; }
    }
}
