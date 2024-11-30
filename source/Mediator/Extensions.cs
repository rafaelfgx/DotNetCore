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

        var assemblies = DependencyContext.Default!.GetDefaultAssemblyNames().Where(assembly => assembly.FullName.StartsWith(@namespace)).Select(Assembly.Load);

        var types = assemblies.SelectMany(assembly => assembly.GetTypes()).ToList();

        types.Where(type => type.GetInterfaces().Any(IsHandler)).ToList().ForEach(type => type.GetInterfaces().Where(IsHandler).ToList().ForEach(@interface => services.AddScoped(@interface, type)));

        types.Where(type => IsType(type.BaseType, typeof(AbstractValidator<>))).ToList().ForEach(type => services.AddSingleton(type.BaseType!, type));

        return;

        static bool IsHandler(Type type) => IsType(type, typeof(IHandler<>)) || IsType(type, typeof(IHandler<,>));

        static bool IsType(Type type, MemberInfo memberInfo) => type is not null && type.IsGenericType && type.GetGenericTypeDefinition() == memberInfo;
    }
}
