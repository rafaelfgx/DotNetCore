using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DotNetCore.Services;

public static class Extensions
{
    public static void AddCsvService(this IServiceCollection services) => services.AddSingleton<ICsvService, CsvService>();

    public static void AddFileCache(this IServiceCollection services) => services.AddSingleton<IFileCache, FileCache>();

    public static void AddJsonStringLocalizer(this IServiceCollection services) => services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
}
