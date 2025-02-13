using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebApiMongoDbDemo.Entities;


namespace WebApiMongoDbDemo.Data
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Customer> _collection;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            // Use the key "MongoDb" to retrieve the connection string.
            var connectionString = _configuration.GetConnectionString("MongoDb");

            // Initialize MongoClient with the connection string
            var mongoClient = new MongoClient(connectionString);

            // Get the database name from configuration or use a default one.
            var databaseName = _configuration["MongoDb:DatabaseName"] ?? "Customer"; // Replace with your default name
            _database = mongoClient.GetDatabase(databaseName);
            _collection = _database.GetCollection<Customer>("customers");

        }

        // Property to access the database
        public IMongoDatabase Database => _database;

        // Optionally, expose a method to get the database.
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}