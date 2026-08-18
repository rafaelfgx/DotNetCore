using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCore.AspNetCore;

public static class ApplicationBuilderExtensions
{
    extension(IApplicationBuilder application)
    {
        public void UseCorsAllowAny() => application.UseCors("AllowAny");

        public void UseException()
        {
            var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment()) application.UseDeveloperExceptionPage();
        }

        public void UseLocalization(params string[] cultures) => application.UseRequestLocalization(options => options.AddSupportedCultures(cultures).AddSupportedUICultures(cultures).SetDefaultCulture(cultures.First()));
    }
}
