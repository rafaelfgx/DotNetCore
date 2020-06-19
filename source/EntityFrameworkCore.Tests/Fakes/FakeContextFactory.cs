using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DotNetCore.EntityFrameworkCore.Tests.Fakes
{
    public sealed class FakeContextFactory : IDesignTimeDbContextFactory<FakeContext>
    {
        public FakeContext CreateDbContext(string[] args)
        {
            const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EntityFrameworkCoreTests;Integrated Security=true;Connection Timeout=5;";

            var builder = new DbContextOptionsBuilder<FakeContext>();

            builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(typeof(FakeContextFactory).Assembly.GetName().Name));

            return new FakeContext(builder.Options);
        }
    }
}
