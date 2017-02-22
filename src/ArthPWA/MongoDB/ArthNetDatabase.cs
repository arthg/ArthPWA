using MongoDB.Driver;

namespace ArthPWA.MongoDB
{
    public interface IMongoDb
    {
       MongoDatabase Database { get; }
    }

    public sealed class ArthNetDatabase : IMongoDb
    {
        private readonly MongoDatabase _database;

        public ArthNetDatabase()
        {
            var connectionString = "mongodb://localhost/ArthNet";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var mongoUrl = MongoUrl.Create(connectionString);
            _database = server.GetDatabase(mongoUrl.DatabaseName);
        }

        public MongoDatabase Database { get { return _database; } }
    }
}