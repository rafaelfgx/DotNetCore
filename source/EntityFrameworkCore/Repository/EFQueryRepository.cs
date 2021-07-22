using DotNetCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNetCore.EntityFrameworkCore
{
    public class EFQueryRepository<T> : IQueryRepository<T> where T : class
    {
        private readonly DbContext _context;

        public EFQueryRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Queryable => _context.QuerySet<T>();

        public bool Any()
        {
            return Queryable.Any();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return Queryable.Any(where);
        }

        public Task<bool> AnyAsync()
        {
            return Queryable.AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return Queryable.AnyAsync(where);
        }

        public long Count()
        {
            return Queryable.LongCount();
        }

        public long Count(Expression<Func<T, bool>> where)
        {
            return Queryable.LongCount(where);
        }

        public Task<long> CountAsync()
        {
            return Queryable.LongCountAsync();
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> where)
        {
            return Queryable.LongCountAsync(where);
        }

        public T Get(object key)
        {
            return _context.DetectChangesLazyLoading(false).Set<T>().Find(key);
        }

        public Task<T> GetAsync(object key)
        {
            return _context.DetectChangesLazyLoading(false).Set<T>().FindAsync(key).AsTask();
        }

        public IEnumerable<T> List()
        {
            return Queryable.ToList();
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await Queryable.ToListAsync().ConfigureAwait(false);
        }
    }
}
