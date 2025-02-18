using DotNetCore.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DotNetCore.MongoDB;

public class MongoCommandRepository<T>(IMongoContext context) : ICommandRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection = context.Database.GetCollection<T>(typeof(T).Name);

    public void Add(T item) => _collection.InsertOne(item);

    public Task AddAsync(T item) => _collection.InsertOneAsync(item);

    public void AddRange(IEnumerable<T> items) => _collection.InsertMany(items);

    public Task AddRangeAsync(IEnumerable<T> items) => _collection.InsertManyAsync(items);

    public void Delete(object key) => _collection.DeleteOne(Filters.Id<T>(key));

    public void Delete(Expression<Func<T, bool>> where) => _collection.DeleteMany(where);

    public Task DeleteAsync(object key) => _collection.DeleteOneAsync(Filters.Id<T>(key));

    public Task DeleteAsync(Expression<Func<T, bool>> where) => _collection.DeleteManyAsync(where);

    public void Update(T item) => _collection.ReplaceOne(Filters.Id<T>(GetKey(item)), item);

    public Task UpdateAsync(T item) => _collection.ReplaceOneAsync(Filters.Id<T>(GetKey(item)), item);

    public void UpdatePartial(object item) => _collection.ReplaceOne(Filters.Id<T>(GetKey(item)), item as T);

    public Task UpdatePartialAsync(object item) => _collection.ReplaceOneAsync(Filters.Id<T>(GetKey(item)), item as T);

    public void UpdateRange(IEnumerable<T> items) => _collection.BulkWrite(WriteModels(items));

    public Task UpdateRangeAsync(IEnumerable<T> items) => _collection.BulkWriteAsync(WriteModels(items));

    private static IEnumerable<WriteModel<T>> WriteModels(IEnumerable<T> items) => items.Select(item => new ReplaceOneModel<T>(Filters.Id<T>(GetKey(item)), item) { IsUpsert = true });

    private static object GetKey(object item) => item.GetType().GetProperty("Id")?.GetValue(item, default);
}
