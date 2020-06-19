using Microsoft.Extensions.DependencyInjection;
using System;

namespace DotNetCore.Security
{
    public static class Extensions
    {
        public static void AddCryptography(this IServiceCollection services, string key)
        {
            services.AddSingleton<ICryptographyService>(_ => new CryptographyService(key));
        }

        public static void AddHash(this IServiceCollection services, int iterations, int size)
        {
            services.AddSingleton<IHashService>(_ => new HashService(iterations, size));
        }

        public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires)
        {
            services.AddJsonWebToken(new JsonWebTokenSettings(key, expires));
        }

        public static void AddJsonWebToken(this IServiceCollection services, string key, TimeSpan expires, string audience, string issuer)
        {
            services.AddJsonWebToken(new JsonWebTokenSettings(key, expires, audience, issuer));
        }

        public static void AddJsonWebToken(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings)
        {
            services.AddSingleton(_ => jsonWebTokenSettings);
            services.AddSingleton<IJsonWebTokenService, JsonWebTokenService>();
        }
    }
}
