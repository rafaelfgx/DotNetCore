using Microsoft.EntityFrameworkCore;

namespace DotNetCore.EntityFrameworkCore;

public sealed class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public UnitOfWork(TDbContext context) => _context = context;

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
