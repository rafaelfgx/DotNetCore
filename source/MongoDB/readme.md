# DotNetCore.MongoDB

## Context

### IMongoContext

```cs
public interface IMongoContext
{
    IMongoDatabase Database { get; }
}
```

### MongoContext

```cs
public abstract class MongoContext : IMongoContext
{
    public MongoContext(string connectionString) { }

    public IMongoDatabase Database { get; }
}
```

## Document

### IDocument

```cs
public interface IDocument
{
    ObjectId Id { get; set; }
}
```

### Document

```cs
public abstract class Document : IDocument
{
    [BsonExtraElements]
    public BsonDocument ExtraElements { get; set; }

    public ObjectId Id { get; set; }
}
```

## MongoCommandRepository

```cs
public class MongoCommandRepository<T> : ICommandRepository<T> where T : class
{
    public MongoCommandRepository(IMongoContext context) { }

    public void Add(T item) { }

    public Task AddAsync(T item) { }

    public void AddRange(IEnumerable<T> items) { }

    public Task AddRangeAsync(IEnumerable<T> items) { }

    public void Delete(object key) { }

    public void Delete(Expression<Func<T, bool>> where) { }

    public Task DeleteAsync(object key) { }

    public Task DeleteAsync(Expression<Func<T, bool>> where) { }

    public void Update(T item) { }

    public Task UpdateAsync(T item) { }

    public void UpdatePartial(object item) { }

    public Task UpdatePartialAsync(object item) { }

    public void UpdateRange(IEnumerable<T> items) { }

    public Task UpdateRangeAsync(IEnumerable<T> items) { }
}
```

## MongoQueryRepository

```cs
public class MongoQueryRepository<T> : IQueryRepository<T> where T : class
{
    public MongoQueryRepository(IMongoContext context) { }

    public IQueryable<T> Queryable { get; };

    public bool Any() { }

    public bool Any(Expression<Func<T, bool>> where) { }

    public Task<bool> AnyAsync() { }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where) { }

    public long Count() { }

    public long Count(Expression<Func<T, bool>> where) { }

    public Task<long> CountAsync() { }

    public Task<long> CountAsync(Expression<Func<T, bool>> where) { }

    public T Get(object key) { }

    public Task<T> GetAsync(object key) { }

    public IEnumerable<T> List() { }

    public async Task<IEnumerable<T>> ListAsync() { }
}
```

## MongoRepository

```cs
public class MongoRepository<T> : Repository<T> where T : class
{
    public MongoRepository(IMongoContext context) : base(new MongoCommandRepository<T>(context), new MongoQueryRepository<T>(context)) { }
}
```
