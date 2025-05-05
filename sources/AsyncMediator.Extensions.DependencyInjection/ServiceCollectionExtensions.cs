using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AsyncMediator.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAsyncMediator(this IServiceCollection containerBuilder, params Assembly[] assembly)
    {
        containerBuilder.AddTransient<MultiInstanceFactory>(context =>
        {
            return type => context.GetService(typeof(IEnumerable<>).MakeGenericType(type)) as IEnumerable<object>;
        });

        containerBuilder.AddTransient<SingleInstanceFactory>(context =>
        {
            return type => context.GetService(type);
        });

        containerBuilder.AddSingleton<IMediator, Mediator>();

        containerBuilder.RegisterAssemblyTypes(typeof(IEventHandler<>), assembly);
        containerBuilder.RegisterAssemblyTypes(typeof(ICommandHandler<>), assembly);
        containerBuilder.RegisterAssemblyTypes(typeof(IQuery<,>), assembly);
        containerBuilder.RegisterAssemblyTypes(typeof(ILookupQuery<>), assembly);

        return containerBuilder;
    }

    public static IServiceCollection RegisterAssemblyTypes(this IServiceCollection services, Type serviceType, params Assembly[] assemblies)
    {
        IEnumerable<Type> types = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableFrom2(serviceType));

        foreach (Type typeFromAssembly in types)
            services.AddTransient(serviceType, typeFromAssembly);

        return services;
    }

    private static bool IsAssignableFrom2(this Type type, Type baseType)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (baseType == null)
            throw new ArgumentNullException(nameof(baseType));

        if (baseType.IsGenericType)
        {
            Type genericBaseType = baseType.GetGenericTypeDefinition();
            return genericBaseType.IsAssignableFrom(type);
        }
        else
        {
            return baseType.IsAssignableFrom(type);
        }
    }
}