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

    void Update(object key, T item);

    Task UpdateAsync(object key, T item);

    void UpdatePartial(object key, object item);

    Task UpdatePartialAsync(object key, object item);

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

    IQueryable<T> Specify(ISpecification<T> specification);

    Task<IQueryable<T>> SpecifyAsync(ISpecification<T> specification);
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

    public IQueryable<T> Specify(ISpecification<T> specification) { }

    public Task<IQueryable<T>> SpecifyAsync(ISpecification<T> specification) { }

    public void Update(object key, T item) { }

    public Task UpdateAsync(object key, T item) { }

    public void UpdatePartial(object key, object item) { }

    public Task UpdatePartialAsync(object key, object item) { }

    public void UpdateRange(IEnumerable<T> items) { }

    public Task UpdateRangeAsync(IEnumerable<T> items) { }
}
```

## Specification

### ISpecification

```cs
public interface ISpecification<T>
{
    List<Expression<Func<T, object>>> Includes { get; }

    Expression<Func<T, object>> OrderBy { get; }

    Expression<Func<T, object>> OrderByDescending { get; }

    int Skip { get; }

    int Take { get; }

    Expression<Func<T, bool>> Where { get; }

    void AddInclude(Expression<Func<T, object>> include);

    void ApplyOrderBy(Expression<Func<T, object>> orderBy);

    void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending);

    void ApplySkipTake(int skip, int take);

    void ApplyWhere(Expression<Func<T, bool>> where);
}
```

### Specification

```cs
public abstract class Specification<T> : ISpecification<T>
{
    public List<Expression<Func<T, object>>> Includes { get; }

    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public Expression<Func<T, bool>> Where { get; private set; }

    public void AddInclude(Expression<Func<T, object>> include) { }

    public void ApplyOrderBy(Expression<Func<T, object>> orderBy) { }

    public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescending) { }

    public void ApplySkipTake(int skip, int take) { }

    public void ApplyWhere(Expression<Func<T, bool>> where) { }
}
```

### SpecificationExtensions

```cs
public static class SpecificationExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> queryable, ISpecification<T> specification) where T : class { }
}
```
