using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCore.AspNetCore;

public static class ApplicationBuilderExtensions
{
    public static void UseCorsAllowAny(this IApplicationBuilder application) => application.UseCors("AllowAny");

    public static void UseException(this IApplicationBuilder application)
    {
        var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (environment.IsDevelopment()) application.UseDeveloperExceptionPage();
    }

    public static void UseLocalization(this IApplicationBuilder application, params string[] cultures) => application.UseRequestLocalization(options => options.AddSupportedCultures(cultures).AddSupportedUICultures(cultures).SetDefaultCulture(cultures.First()));
}
