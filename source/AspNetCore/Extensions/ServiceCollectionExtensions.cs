using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace DotNetCore.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddAuthorizationPolicy(this IMvcBuilder builder) => builder.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())));

    public static IServiceCollection AddCorsAllowAny(this IServiceCollection services) => services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

    public static IServiceCollection AddFileExtensionContentTypeProvider(this IServiceCollection services) => services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

    public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder)
    {
        return builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new JsonStringBoolConverter());
        });
    }

    public static void AddSwaggerDefault(this IServiceCollection services) => services.AddSwaggerGen(ConfigureSwaggerGenOptions);

    public static IServiceCollection ConfigureFormOptionsMaxLengthLimit(this IServiceCollection services)
    {
        return services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
        });
    }

    private static void ConfigureSwaggerGenOptions(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
            Name = "Authorization"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        In = ParameterLocation.Header,
                        Scheme = SecuritySchemeType.OAuth2.ToString(),
                        Name = JwtBearerDefaults.AuthenticationScheme
                    },
                    new List<string>()
                }
            });
    }
}
