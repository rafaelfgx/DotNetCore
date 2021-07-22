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

        public void Update(T item)
        {
            _collection.ReplaceOne(Filters.Id<T>(GetKey(item)), item);
        }

        public Task UpdateAsync(T item)
        {
            return _collection.ReplaceOneAsync(Filters.Id<T>(GetKey(item)), item);
        }

        public void UpdatePartial(object item)
        {
            _collection.ReplaceOne(Filters.Id<T>(GetKey(item)), item as T);
        }

        public Task UpdatePartialAsync(object item)
        {
            return _collection.ReplaceOneAsync(Filters.Id<T>(GetKey(item)), item as T);
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
                var key = GetKey(item);

                if (key is null) continue;

                updates.Add(new ReplaceOneModel<T>(Filters.Id<T>(key), item));
            }

            return updates;
        }

        private static object GetKey(object item) => item.GetType().GetProperty("Id")?.GetValue(item, default);
    }
}
