using MongoDB.Driver;

namespace DotNetCore.MongoDB;

public abstract class MongoContext(string connectionString) : IMongoContext
{
    public IMongoDatabase Database { get; } = new MongoClient(connectionString).GetDatabase(new MongoUrl(connectionString).DatabaseName);
}
