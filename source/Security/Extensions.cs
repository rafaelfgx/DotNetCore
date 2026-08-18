using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.Security;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public void AddCryptographyService(string password) => services.AddSingleton<ICryptographyService>(_ => new CryptographyService(password));

        public void AddHashService() => services.AddSingleton<IHashService, HashService>();
    }
}
