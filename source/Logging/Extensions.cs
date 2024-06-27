using DotNetCore.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DotNetCore.Logging;

public static class Extensions
{
    public static void Log(this Microsoft.Extensions.Logging.ILogger logger, HttpResponseMessage response)
    {
        logger.Log
        (
            response.IsSuccessStatusCode ? LogLevel.Information : LogLevel.Error,
            "{Url} {Method} {Request} {Response}",
            response.RequestMessage?.RequestUri,
            response.RequestMessage?.Method,
            response.RequestMessage?.Content?.ReadAsStringAsync().Result,
            response.Content.ReadAsStringAsync().Result
        );
    }

    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder().Configuration();

        Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

        SerilogHostBuilderExtensions.UseSerilog(builder);

        return builder;
    }
}
