using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace DotNetCore.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsAllowAny(this IApplicationBuilder application)
        {
            application.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        }

        public static void UseEndpoints(this IApplicationBuilder application)
        {
            application.UseEndpoints(builder => builder.MapControllers());
        }

        public static void UseException(this IApplicationBuilder application)
        {
            var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
        }

        public static void UseHttps(this IApplicationBuilder application)
        {
            application.UseHsts();
            application.UseHttpsRedirection();
        }

        public static void UseSpaAngular(this IApplicationBuilder application, string sourcePath, string npmScript, string baseUri)
        {
            try
            {
                WebRequest.Create(baseUri).GetResponse();
                application.UseSpaProxyServer(sourcePath, baseUri);
            }
            catch
            {
                application.UseSpaAngularServer(sourcePath, npmScript);
            }
        }

        public static void UseSpaAngularServer(this IApplicationBuilder application, string sourcePath, string npmScript)
        {
            application.UseSpa(sourcePath, builder => builder.UseAngularCliServer(npmScript));
        }

        public static void UseSpaProxyServer(this IApplicationBuilder application, string sourcePath, string baseUri)
        {
            application.UseSpa(sourcePath, builder => builder.UseProxyToSpaDevelopmentServer(baseUri));
        }

        private static void UseSpa(this IApplicationBuilder application, string sourcePath, Action<ISpaBuilder> spaBuilder)
        {
            var environment = application.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            application.UseSpaStaticFiles();

            application.UseSpa(builder =>
            {
                builder.Options.SourcePath = sourcePath;

                if (environment.IsDevelopment())
                {
                    spaBuilder(builder);
                }
            });
        }
    }
}
