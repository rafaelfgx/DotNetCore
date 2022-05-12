using DotNetCore.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DotNetCore.Logging;

public static class Extensions
{
    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder().Configuration();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        SerilogHostBuilderExtensions.UseSerilog(builder);

        return builder;
    }
}
