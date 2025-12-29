namespace DotNetCore.EntityFrameworkCore;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
