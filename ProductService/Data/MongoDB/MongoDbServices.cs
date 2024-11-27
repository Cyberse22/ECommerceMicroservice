using MongoDB.Driver;

namespace ProductService.Data.MongoDB
{
    public class MongoDbServices
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;

        public MongoDbServices(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration["ProductServiceDatabaseSetting:ConnectionString"];
            var databaseName = _configuration["ProductServiceDatabaseSetting:DatabaseName"];

            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(databaseName);
        }
        public IMongoDatabase? Database => _database;
    }
}    