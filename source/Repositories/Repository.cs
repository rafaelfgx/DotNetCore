using System.Linq.Expressions;

namespace DotNetCore.Repositories;

public abstract class Repository<T>(ICommandRepository<T> commandRepository, IQueryRepository<T> queryRepository) : IRepository<T> where T : class
{
    public IQueryable<T> Queryable => queryRepository.Queryable;

    public void Add(T item) => commandRepository.Add(item);

    public Task AddAsync(T item) => commandRepository.AddAsync(item);

    public void AddRange(IEnumerable<T> items) => commandRepository.AddRange(items);

    public Task AddRangeAsync(IEnumerable<T> items) => commandRepository.AddRangeAsync(items);

    public bool Any() => queryRepository.Any();

    public bool Any(Expression<Func<T, bool>> where) => queryRepository.Any(where);

    public Task<bool> AnyAsync() => queryRepository.AnyAsync();

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where) => queryRepository.AnyAsync(where);

    public long Count() => queryRepository.Count();

    public long Count(Expression<Func<T, bool>> where) => queryRepository.Count(where);

    public Task<long> CountAsync() => queryRepository.CountAsync();

    public Task<long> CountAsync(Expression<Func<T, bool>> where) => queryRepository.CountAsync(where);

    public void Delete(object key) => commandRepository.Delete(key);

    public void Delete(Expression<Func<T, bool>> where) => commandRepository.Delete(where);

    public Task DeleteAsync(object key) => commandRepository.DeleteAsync(key);

    public Task DeleteAsync(Expression<Func<T, bool>> where) => commandRepository.DeleteAsync(where);

    public T Get(object key) => queryRepository.Get(key);

    public Task<T> GetAsync(object key) => queryRepository.GetAsync(key);

    public IEnumerable<T> List() => queryRepository.List();

    public Task<IEnumerable<T>> ListAsync() => queryRepository.ListAsync();

    public void Update(T item) => commandRepository.Update(item);

    public Task UpdateAsync(T item) => commandRepository.UpdateAsync(item);

    public void UpdatePartial(object item) => commandRepository.UpdatePartial(item);

    public Task UpdatePartialAsync(object item) => commandRepository.UpdatePartialAsync(item);

    public void UpdateRange(IEnumerable<T> items) => commandRepository.UpdateRange(items);

    public Task UpdateRangeAsync(IEnumerable<T> items) => commandRepository.UpdateRangeAsync(items);
}
