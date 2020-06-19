using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DotNetCore.Mediator
{
    public static class Extensions
    {
        public static void AddMediator(this IServiceCollection services, Assembly assembly)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddRequestHandlers(assembly);
            services.AddValidators(assembly);
        }

        private static void AddRequestHandlers(this IServiceCollection services, Assembly assembly)
        {
            static bool Expression(Type type) => type.Is(typeof(IRequestHandler<,>)) || type.Is(typeof(IRequestHandler<>));

            assembly
                .GetTypes()
                .Where(type => type.GetInterfaces().Any(Expression))
                .ToList()
                .ForEach(type => type.GetInterfaces().Where(Expression).ToList().ForEach(@interface => services.AddScoped(@interface, type)));
        }

        private static void AddValidators(this IServiceCollection services, Assembly assembly)
        {
            assembly
                .GetTypes()
                .Where(type => type.BaseType.Is(typeof(AbstractValidator<>)))
                .ToList()
                .ForEach(type => services.AddScoped(type.BaseType, type));
        }

        private static bool Is(this Type type, Type typeCompare)
        {
            return type.IsGenericType && (type.Name.Equals(typeCompare.Name) || type.GetGenericTypeDefinition() == typeCompare);
        }
    }
}
