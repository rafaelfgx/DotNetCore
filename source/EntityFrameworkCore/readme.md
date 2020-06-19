# DotNetCore.EntityFrameworkCore

## Extensions

```cs
public static class Extensions
{
    public static void AddContext<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> options) where T : DbContext { }

    public static void AddContextMemory<T>(this IServiceCollection services) where T : DbContext { }

    public static void AddUnitOfWork<T>(this IServiceCollection services) where T : DbContext { }

    public static DbSet<T> CommandSet<T>(this DbContext context) where T : class { }

    public static DbContext DetectChangesLazyLoading(this DbContext context, bool enabled) { }

    public static IQueryable<T> QuerySet<T>(this DbContext context) where T : class { }
}
```

### EFCommandRepository

```cs
public class EFCommandRepository<T> : ICommandRepository<T> where T : class
{
    public EFCommandRepository(DbContext context) { }

    public void Add(T item) { }

    public Task AddAsync(T item) { }

    public void AddRange(IEnumerable<T> items) { }

    public Task AddRangeAsync(IEnumerable<T> items) { }

    public void Delete(object key) { }

    public void Delete(Expression<Func<T, bool>> where) { }

    public Task DeleteAsync(object key) { }

    public Task DeleteAsync(Expression<Func<T, bool>> where) { }

    public void Update(object key, T item) { }

    public Task UpdateAsync(object key, T item) { }

    public void UpdatePartial(object key, object item) { }

    public Task UpdatePartialAsync(object key, object item) { }

    public void UpdateRange(IEnumerable<T> items) { }

    public Task UpdateRangeAsync(IEnumerable<T> items) { }
}
```

### EFQueryRepository

```cs
public class EFQueryRepository<T> : IQueryRepository<T> where T : class
{
    public EFQueryRepository(DbContext context) { }

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

    public IQueryable<T> Specify(ISpecification<T> specification) { }

    public Task<IQueryable<T>> SpecifyAsync(ISpecification<T> specification) { }
}
```

### EFRepository

```cs
public class EFRepository<T> : Repository<T> where T : class
{
    public EFRepository(DbContext context) : base(new EFCommandRepository<T>(context), new EFQueryRepository<T>(context)) { }
}
```

### IUnitOfWork

```cs
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
```

### UnitOfWork

```cs
public sealed class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    public UnitOfWork(TDbContext context) { }

    public Task<int> SaveChangesAsync() { }
}
```
