using DotNetCore.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCore.MongoDB
{
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

        public bool Any()
        {
            return _queryable.Any();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return _queryable.Where(where).Any();
        }

        public Task<bool> AnyAsync()
        {
            return _queryable.AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return _queryable.Where(where).AnyAsync();
        }

        public long Count()
        {
            return _queryable.LongCount();
        }

        public long Count(Expression<Func<T, bool>> where)
        {
            return _queryable.Where(where).LongCount();
        }

        public Task<long> CountAsync()
        {
            return _queryable.LongCountAsync();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> where)
        {
            return _queryable.Where(where).LongCountAsync();
        }

        public T Get(object key)
        {
            return _collection.Find(Filters.Id<T>(key)).SingleOrDefault();
        }

        public Task<T> GetAsync(object key)
        {
            return _collection.Find(Filters.Id<T>(key)).SingleOrDefaultAsync();
        }

        public IEnumerable<T> List()
        {
            return _queryable.ToList();
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _queryable.ToListAsync().ConfigureAwait(false);
        }
    }
}
