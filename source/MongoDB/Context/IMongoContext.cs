using MongoDB.Driver;

namespace DotNetCore.MongoDB
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
