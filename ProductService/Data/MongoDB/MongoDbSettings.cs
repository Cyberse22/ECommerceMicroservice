namespace ProductService.Data.DbContext
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ProductServiceCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }

    public interface IMongoDbSettings
    {
        string ProductServiceCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
