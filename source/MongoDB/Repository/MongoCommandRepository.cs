using DotNetCore.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCore.MongoDB
{
    public class MongoCommandRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoCommandRepository(IMongoContext context)
        {
            _collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        public void Add(T item)
        {
            _collection.InsertOne(item);
        }

        public Task AddAsync(T item)
        {
            return _collection.InsertOneAsync(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _collection.InsertMany(items);
        }

        public Task AddRangeAsync(IEnumerable<T> items)
        {
            return _collection.InsertManyAsync(items);
        }

        public void Delete(object key)
        {
            _collection.DeleteOne(Filters.Id<T>(key));
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            _collection.DeleteMany(where);
        }

        public Task DeleteAsync(object key)
        {
            return _collection.DeleteOneAsync(Filters.Id<T>(key));
        }

        public Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            return _collection.DeleteManyAsync(where);
        }

        public void Update(object key, T item)
        {
            _collection.ReplaceOne(Filters.Id<T>(key), item);
        }

        public Task UpdateAsync(object key, T item)
        {
            return _collection.ReplaceOneAsync(Filters.Id<T>(key), item);
        }

        public void UpdatePartial(object key, object item)
        {
            _collection.ReplaceOne(Filters.Id<T>(key), item as T);
        }

        public Task UpdatePartialAsync(object key, object item)
        {
            return _collection.ReplaceOneAsync(Filters.Id<T>(key), item as T);
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            _collection.BulkWrite(CreateUpdates(items));
        }

        public Task UpdateRangeAsync(IEnumerable<T> items)
        {
            return _collection.BulkWriteAsync(CreateUpdates(items));
        }

        private static IEnumerable<WriteModel<T>> CreateUpdates(IEnumerable<T> items)
        {
            var updates = new List<WriteModel<T>>();

            foreach (var item in items)
            {
                var id = typeof(T).GetProperty("Id")?.GetValue(item);

                if (id == null) { continue; }

                updates.Add(new ReplaceOneModel<T>(Filters.Id<T>(id), item));
            }

            return updates;
        }
    }
}
