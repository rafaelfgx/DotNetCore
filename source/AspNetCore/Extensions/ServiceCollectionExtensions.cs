using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

namespace DotNetCore.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddAuthorizationPolicy(this IMvcBuilder builder) => builder.AddMvcOptions(options => options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())));

    extension(IServiceCollection services)
    {
        public IServiceCollection AddCorsAllowAny() => services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        public IServiceCollection AddFileExtensionContentTypeProvider() => services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

        public void AddSwaggerDefault() => services.AddSwaggerGen(ConfigureSwaggerGenOptions);

        public IServiceCollection ConfigureFormOptionsMaxLengthLimit() => services.Configure<FormOptions>(options =>
        {
            options.ValueLengthLimit = int.MaxValue;
            options.MultipartBodyLengthLimit = int.MaxValue;
        });
    }

    public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder) => builder.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new JsonStringBoolConverter());
    });

    private static void ConfigureSwaggerGenOptions(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower(),
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization"
        });

        options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme.ToLower(), document)] = []
        });
    }
}
