using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace DotNetCore.Services;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        public void AddCsvService() => services.AddSingleton<ICsvService, CsvService>();

        public void AddFileCache() => services.AddSingleton<IFileCache, FileCache>();

        public void AddJsonStringLocalizer() => services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
    }
}
