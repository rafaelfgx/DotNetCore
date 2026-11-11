using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace DotNetCore.Mediator;

public static class Extensions
{
    public static void AddMediator(this IServiceCollection services, string @namespace)
    {
        services.AddScoped<IMediator, Mediator>();

        var assemblies = DependencyContext.Default.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(@namespace)).Select(Assembly.Load);

        foreach (var type in assemblies.SelectMany(assembly => assembly.GetTypes()))
        {
            foreach (var @interface in type.GetInterfaces().Where(IsHandler))
            {
                services.AddScoped(@interface, type);
            }

            if (IsType(type.BaseType, typeof(AbstractValidator<>))) services.AddSingleton(type.BaseType, type);
        }

        static bool IsHandler(Type type) => IsType(type, typeof(IHandler<>)) || IsType(type, typeof(IHandler<,>));

        static bool IsType(Type type, MemberInfo memberInfo) => type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == memberInfo;
    }
}
