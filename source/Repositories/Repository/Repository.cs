using System.Linq.Expressions;

namespace DotNetCore.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly ICommandRepository<T> _commandRepository;
    private readonly IQueryRepository<T> _queryRepository;

    protected Repository
    (
        ICommandRepository<T> commandRepository,
        IQueryRepository<T> queryRepository
    )
    {
        _commandRepository = commandRepository;
        _queryRepository = queryRepository;
    }

    public IQueryable<T> Queryable => _queryRepository.Queryable;

    public void Add(T item)
    {
        _commandRepository.Add(item);
    }

    public Task AddAsync(T item)
    {
        return _commandRepository.AddAsync(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        _commandRepository.AddRange(items);
    }

    public Task AddRangeAsync(IEnumerable<T> items)
    {
        return _commandRepository.AddRangeAsync(items);
    }

    public bool Any()
    {
        return _queryRepository.Any();
    }

    public bool Any(Expression<Func<T, bool>> where)
    {
        return _queryRepository.Any(where);
    }

    public Task<bool> AnyAsync()
    {
        return _queryRepository.AnyAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
    {
        return _queryRepository.AnyAsync(where);
    }

    public long Count()
    {
        return _queryRepository.Count();
    }

    public long Count(Expression<Func<T, bool>> where)
    {
        return _queryRepository.Count(where);
    }

    public Task<long> CountAsync()
    {
        return _queryRepository.CountAsync();
    }

    public Task<long> CountAsync(Expression<Func<T, bool>> where)
    {
        return _queryRepository.CountAsync(where);
    }

    public void Delete(object key)
    {
        _commandRepository.Delete(key);
    }

    public void Delete(Expression<Func<T, bool>> where)
    {
        _commandRepository.Delete(where);
    }

    public Task DeleteAsync(object key)
    {
        return _commandRepository.DeleteAsync(key);
    }

    public Task DeleteAsync(Expression<Func<T, bool>> where)
    {
        return _commandRepository.DeleteAsync(where);
    }

    public T Get(object key)
    {
        return _queryRepository.Get(key);
    }

    public Task<T> GetAsync(object key)
    {
        return _queryRepository.GetAsync(key);
    }

    public IEnumerable<T> List()
    {
        return _queryRepository.List();
    }

    public Task<IEnumerable<T>> ListAsync()
    {
        return _queryRepository.ListAsync();
    }

    public void Update(T item)
    {
        _commandRepository.Update(item);
    }

    public Task UpdateAsync(T item)
    {
        return _commandRepository.UpdateAsync(item);
    }

    public void UpdatePartial(object item)
    {
        _commandRepository.UpdatePartial(item);
    }

    public Task UpdatePartialAsync(object item)
    {
        return _commandRepository.UpdatePartialAsync(item);
    }

    public void UpdateRange(IEnumerable<T> items)
    {
        _commandRepository.UpdateRange(items);
    }

    public Task UpdateRangeAsync(IEnumerable<T> items)
    {
        return _commandRepository.UpdateRangeAsync(items);
    }
}
