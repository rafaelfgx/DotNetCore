using Microsoft.EntityFrameworkCore;

namespace DotNetCore.EntityFrameworkCore;

public sealed class UnitOfWork<TDbContext>(TDbContext context) : IUnitOfWork where TDbContext : DbContext
{
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
