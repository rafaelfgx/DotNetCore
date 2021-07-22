using DotNetCore.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCore.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddAuthenticationJwtBearer(this IServiceCollection services)
        {
            var tokenValidationParameters = services.BuildServiceProvider().GetRequiredService<JsonWebTokenSettings>().TokenValidationParameters();

            return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = tokenValidationParameters);
        }

        public static IMvcBuilder AddControllersWithJsonOptionsAndAuthorizationPolicy(this IServiceCollection services)
        {
            return services.AddControllers().AddJsonOptions().AddAuthorizationPolicy();
        }

        public static IMvcBuilder AddControllersWithJsonOptions(this IServiceCollection services)
        {
            return services.AddControllers().AddJsonOptions();
        }

        public static IMvcBuilder AddAuthorizationPolicy(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())));
        }

        public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder)
        {
            return builder.AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        public static IServiceCollection AddCorsAllowAny(this IServiceCollection services)
        {
            return services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        public static IServiceCollection AddFileExtensionContentTypeProvider(this IServiceCollection services)
        {
            return services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
        }

        public static IServiceCollection AddSpaStaticFiles(this IServiceCollection services, string rootPath)
        {
            services.AddSpaStaticFiles(options => options.RootPath = rootPath);

            return services;
        }

        public static IServiceCollection ConfigureFormOptionsMaxLengthLimit(this IServiceCollection services)
        {
            return services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue;
            });
        }
    }
}
