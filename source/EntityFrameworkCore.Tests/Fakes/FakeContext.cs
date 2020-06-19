using Microsoft.EntityFrameworkCore;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public sealed class FakeContext : DbContext
    {
        public FakeContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FakeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new FakeEntityChildConfiguration());
        }
    }
}
