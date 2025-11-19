using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.EntityFrameworkCore;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public void AddContext<T>(Action<DbContextOptionsBuilder> options) where T : DbContext
        {
            services.AddDbContextPool<T>(options);

            services.BuildServiceProvider().GetRequiredService<T>().Database.Migrate();

            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
        }

        public void AddContextMemory<T>() where T : DbContext
        {
            services.AddDbContextPool<T>(options => options.UseInMemoryDatabase(typeof(T).Name));

            services.BuildServiceProvider().GetRequiredService<T>().Database.EnsureCreated();

            services.AddScoped<IUnitOfWork, UnitOfWork<T>>();
        }
    }

    extension(DbContext context)
    {
        public DbSet<T> CommandSet<T>() where T : class => context.DetectChangesLazyLoading(true).Set<T>();

        public DbContext DetectChangesLazyLoading(bool enabled)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = enabled;
            context.ChangeTracker.LazyLoadingEnabled = enabled;
            context.ChangeTracker.QueryTrackingBehavior = enabled ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;

            return context;
        }

        public IQueryable<T> QuerySet<T>() where T : class => context.DetectChangesLazyLoading(false).Set<T>().AsNoTracking();

        public object[] PrimaryKeyValues<T>(object entity) => context.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(property => entity.GetType().GetProperty(property.Name)?.GetValue(entity, null)).ToArray();
    }
}
