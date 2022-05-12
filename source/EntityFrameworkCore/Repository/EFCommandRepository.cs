using DotNetCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotNetCore.EntityFrameworkCore;

public class EFCommandRepository<T> : ICommandRepository<T> where T : class
{
    private readonly DbContext _context;

    public EFCommandRepository(DbContext context)
    {
        _context = context;
    }

    private DbSet<T> Set => _context.CommandSet<T>();

    public void Add(T item)
    {
        Set.Add(item);
    }

    public Task AddAsync(T item)
    {
        return Set.AddAsync(item).AsTask();
    }

    public void AddRange(IEnumerable<T> items)
    {
        Set.AddRange(items);
    }

    public Task AddRangeAsync(IEnumerable<T> items)
    {
        return Set.AddRangeAsync(items);
    }

    public void Delete(object key)
    {
        var item = Set.Find(key);

        if (item is null) return;

        Set.Remove(item);
    }

    public void Delete(Expression<Func<T, bool>> where)
    {
        var items = Set.Where(where);

        if (!items.Any()) return;

        Set.RemoveRange(items);
    }

    public Task DeleteAsync(object key)
    {
        return Task.Run(() => Delete(key));
    }

    public Task DeleteAsync(Expression<Func<T, bool>> where)
    {
        return Task.Run(() => Delete(where));
    }

    public void Update(T item)
    {
        var primaryKeyValues = _context.PrimaryKeyValues<T>(item);

        var entity = Set.Find(primaryKeyValues);

        if (entity is null) return;

        _context.Entry(entity).State = EntityState.Detached;

        _context.Update(item);
    }

    public Task UpdateAsync(T item)
    {
        return Task.Run(() => Update(item));
    }

    public void UpdatePartial(object item)
    {
        var primaryKeyValues = _context.PrimaryKeyValues<T>(item);

        var entity = Set.Find(primaryKeyValues);

        if (entity is null) return;

        var entry = _context.Entry(entity);

        entry.CurrentValues.SetValues(item);

        foreach (var navigation in entry.Metadata.GetNavigations())
        {
            if (navigation.IsOnDependent || navigation.IsCollection || !navigation.ForeignKey.IsOwnership) continue;

            var property = item.GetType().GetProperty(navigation.Name);

            if (property is null) continue;

            var value = property.GetValue(item, default);

            entry.Reference(navigation.Name).TargetEntry.CurrentValues.SetValues(value);
        }
    }

    public Task UpdatePartialAsync(object item)
    {
        return Task.Run(() => UpdatePartial(item));
    }

    public void UpdateRange(IEnumerable<T> items)
    {
        Set.UpdateRange(items);
    }

    public Task UpdateRangeAsync(IEnumerable<T> items)
    {
        return Task.Run(() => UpdateRange(items));
    }
}
