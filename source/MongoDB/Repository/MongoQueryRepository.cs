using DotNetCore.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace DotNetCore.MongoDB;

public class MongoQueryRepository<T> : IQueryRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private readonly IMongoQueryable<T> _queryable;

    public MongoQueryRepository(IMongoContext context)
    {
        _collection = context.Database.GetCollection<T>(typeof(T).Name);
        _queryable = _collection.AsQueryable();
    }

    public IQueryable<T> Queryable => _collection.AsQueryable();

    public bool Any() => _queryable.Any();

    public bool Any(Expression<Func<T, bool>> where) => _queryable.Where(where).Any();

    public Task<bool> AnyAsync() => _queryable.AnyAsync();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where) => _queryable.Where(where).AnyAsync();

    public long Count() => _queryable.LongCount();

    public long Count(Expression<Func<T, bool>> where) => _queryable.Where(where).LongCount();

    public Task<long> CountAsync() => _queryable.LongCountAsync();

    public Task<long> CountAsync(Expression<Func<T, bool>> where) => _queryable.Where(where).LongCountAsync();

    public T Get(object key) => _collection.Find(Filters.Id<T>(key)).SingleOrDefault();

    public Task<T> GetAsync(object key) => _collection.Find(Filters.Id<T>(key)).SingleOrDefaultAsync();

    public IEnumerable<T> List() => _queryable.ToList();

    public async Task<IEnumerable<T>> ListAsync() => await _queryable.ToListAsync().ConfigureAwait(false);
}
