using DotNetCore.Repositories;

namespace DotNetCore.MongoDB
{
    public class MongoRepository<T> : Repository<T> where T : class
    {
        public MongoRepository(IMongoContext context) : base(new MongoCommandRepository<T>(context), new MongoQueryRepository<T>(context)) { }
    }
}
