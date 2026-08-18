using DotNetCore.Repositories;

namespace DotNetCore.MongoDB;

public class MongoRepository<T>(IMongoContext context) : Repository<T>(new MongoCommandRepository<T>(context), new MongoQueryRepository<T>(context)) where T : class;
