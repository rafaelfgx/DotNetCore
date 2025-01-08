using DotNetCore.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace DotNetCore.MongoDB;

public class MongoQueryRepository<T>(IMongoContext context) : IQueryRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection = context.Database.GetCollection<T>(typeof(T).Name);

    public IQueryable<T> Queryable => _collection.AsQueryable();

    public bool Any() => Queryable.Any();

    public bool Any(Expression<Func<T, bool>> where) => Queryable.Where(where).Any();

    public Task<bool> AnyAsync() => Queryable.AnyAsync();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where) => Queryable.Where(where).AnyAsync();

    public long Count() => Queryable.LongCount();

    public long Count(Expression<Func<T, bool>> where) => Queryable.Where(where).LongCount();

    public Task<long> CountAsync() => Queryable.LongCountAsync();

    public Task<long> CountAsync(Expression<Func<T, bool>> where) => Queryable.Where(where).LongCountAsync();

    public T Get(object key) => _collection.Find(Filters.Id<T>(key)).SingleOrDefault();

    public Task<T> GetAsync(object key) => _collection.Find(Filters.Id<T>(key)).SingleOrDefaultAsync();

    public IEnumerable<T> List() => Queryable.ToList();

    public async Task<IEnumerable<T>> ListAsync() => await Queryable.ToListAsync().ConfigureAwait(false);
}
