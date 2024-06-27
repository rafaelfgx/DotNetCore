using DotNetCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotNetCore.EntityFrameworkCore;

public class EFQueryRepository<T> : IQueryRepository<T> where T : class
{
    private readonly DbContext _context;

    public EFQueryRepository(DbContext context) => _context = context;

    public IQueryable<T> Queryable => _context.QuerySet<T>();

    public bool Any() => Queryable.Any();

    public bool Any(Expression<Func<T, bool>> where) => Queryable.Any(where);

    public Task<bool> AnyAsync() => Queryable.AnyAsync();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where) => Queryable.AnyAsync(where);

    public long Count() => Queryable.LongCount();

    public long Count(Expression<Func<T, bool>> where) => Queryable.LongCount(where);

    public Task<long> CountAsync() => Queryable.LongCountAsync();

    public Task<long> CountAsync(Expression<Func<T, bool>> where) => Queryable.LongCountAsync(where);

    public T Get(object key) => _context.DetectChangesLazyLoading(false).Set<T>().Find(key);

    public Task<T> GetAsync(object key) => _context.DetectChangesLazyLoading(false).Set<T>().FindAsync(key).AsTask();

    public IEnumerable<T> List() => Queryable.ToList();

    public async Task<IEnumerable<T>> ListAsync() => await Queryable.ToListAsync().ConfigureAwait(false);
}
