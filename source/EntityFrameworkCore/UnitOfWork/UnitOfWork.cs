using Microsoft.EntityFrameworkCore;

namespace DotNetCore.EntityFrameworkCore;

public sealed class UnitOfWork<TDbContext>(TDbContext context) : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _context = context;

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
