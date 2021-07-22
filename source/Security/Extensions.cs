using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetCore.Security
{
    public static class Extensions
    {
        public static void AddICryptographyService(this IServiceCollection services, string key)
        {
            services.AddSingleton<ICryptographyService>(_ => new CryptographyService(key));
        }

        public static void AddHashService(this IServiceCollection services)
        {
            services.AddSingleton<IHashService, HashService>();
        }

        public static void AddJsonWebTokenService(this IServiceCollection services, string key, TimeSpan expires)
        {
            services.AddJsonWebTokenService(new JsonWebTokenSettings(key, expires));
        }

        public static void AddJsonWebTokenService(this IServiceCollection services, string key, TimeSpan expires, string audience, string issuer)
        {
            services.AddJsonWebTokenService(new JsonWebTokenSettings(key, expires, audience, issuer));
        }

        public static void AddJsonWebTokenService(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings)
        {
            services.AddSingleton(_ => jsonWebTokenSettings);

            services.AddSingleton<IJsonWebTokenService, JsonWebTokenService>();
        }
    }
}
