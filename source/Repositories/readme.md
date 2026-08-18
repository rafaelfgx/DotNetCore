# DotNetCore.Repositories

## Repository

### ICommandRepository

```cs
public interface ICommandRepository<T> where T : class
{
    void Add(T item);

    Task AddAsync(T item);

    void AddRange(IEnumerable<T> items);

    Task AddRangeAsync(IEnumerable<T> items);

    void Delete(object key);

    void Delete(Expression<Func<T, bool>> where);

    Task DeleteAsync(object key);

    Task DeleteAsync(Expression<Func<T, bool>> where);

    void Update(T item);

    Task UpdateAsync(T item);

    void UpdatePartial(object item);

    Task UpdatePartialAsync(object item);

    void UpdateRange(IEnumerable<T> items);

    Task UpdateRangeAsync(IEnumerable<T> items);
}
```

### IQueryRepository

```cs
public interface IQueryRepository<T> where T : class
{
    IQueryable<T> Queryable { get; }

    bool Any();

    bool Any(Expression<Func<T, bool>> where);

    Task<bool> AnyAsync();

    Task<bool> AnyAsync(Expression<Func<T, bool>> where);

    long Count();

    long Count(Expression<Func<T, bool>> where);

    Task<long> CountAsync();

    Task<long> CountAsync(Expression<Func<T, bool>> where);

    T Get(object key);

    Task<T> GetAsync(object key);

    IEnumerable<T> List();

    Task<IEnumerable<T>> ListAsync();
}
```

### IRepository

```cs
public interface IRepository<T> : ICommandRepository<T>, IQueryRepository<T> where T : class { }
```

### Repository

```cs
public abstract class Repository<T> : IRepository<T> where T : class
{
    protected Repository(ICommandRepository<T> commandRepository, IQueryRepository<T> queryRepository) { }

    public IQueryable<T> Queryable { get; };

    public void Add(T item) { }

    public Task AddAsync(T item) { }

    public void AddRange(IEnumerable<T> items) { }

    public Task AddRangeAsync(IEnumerable<T> items) { }

    public bool Any() { }

    public bool Any(Expression<Func<T, bool>> where) { }

    public Task<bool> AnyAsync() { }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where) { }

    public long Count() { }

    public long Count(Expression<Func<T, bool>> where) { }

    public Task<long> CountAsync() { }

    public Task<long> CountAsync(Expression<Func<T, bool>> where) { }

    public void Delete(object key) { }

    public void Delete(Expression<Func<T, bool>> where) { }

    public Task DeleteAsync(object key) { }

    public Task DeleteAsync(Expression<Func<T, bool>> where) { }

    public T Get(object key) { }

    public Task<T> GetAsync(object key) { }

    public IEnumerable<T> List() { }

    public Task<IEnumerable<T>> ListAsync() { }

    public void Update(T item) { }

    public Task UpdateAsync(T item) { }

    public void UpdatePartial(object item) { }

    public Task UpdatePartialAsync(object item) { }

    public void UpdateRange(IEnumerable<T> items) { }

    public Task UpdateRangeAsync(IEnumerable<T> items) { }
}
```
