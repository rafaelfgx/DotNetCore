using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace DotNetCore.Mediator;

public static class Extensions
{
    public static void AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.TryAddScoped<IMediator, Mediator>();

        services.AddHandlers(assembly);

        services.AddValidators(assembly);
    }

    private static void AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        static bool IsHandler(Type type) => type.Is(typeof(IHandler<>)) || type.Is(typeof(IHandler<,>));

        assembly
            .GetTypes()
            .Where(type => type.GetInterfaces().Any(IsHandler))
            .ToList()
            .ForEach(type => type.GetInterfaces().Where(IsHandler).ToList().ForEach(@interface => services.TryAddScoped(@interface, type)));
    }

    private static void AddValidators(this IServiceCollection services, Assembly assembly)
    {
        assembly
            .GetTypes()
            .Where(type => type.BaseType.Is(typeof(AbstractValidator<>)))
            .ToList()
            .ForEach(type => services.TryAddSingleton(type.BaseType, type));
    }

    private static bool Is(this Type type, MemberInfo memberInfo)
    {
        return type.IsGenericType && (type.Name.Equals(memberInfo.Name) || type.GetGenericTypeDefinition() == memberInfo);
    }
}
