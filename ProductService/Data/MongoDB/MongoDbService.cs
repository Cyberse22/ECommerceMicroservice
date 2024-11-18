using MongoDB.Driver;
using ProductService.Data.DbContext;

namespace ProductService.Data.MongoDB
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;

        public MongoDbService(IConfiguration configuration)
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
