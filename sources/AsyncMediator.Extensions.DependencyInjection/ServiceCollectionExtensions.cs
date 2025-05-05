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

        containerBuilder.AddScoped<IMediator, Mediator>();

        containerBuilder.AddAllTypes(typeof(IEventHandler<>), assembly);
        containerBuilder.AddAllTypes(typeof(ICommandHandler<>), assembly);
        containerBuilder.AddAllTypes(typeof(IQuery<,>), assembly);
        containerBuilder.AddAllTypes(typeof(ILookupQuery<>), assembly);

        return containerBuilder;
    }

    private static IServiceCollection AddAllTypes(this IServiceCollection services, Type serviceType, params Assembly[] assemblies)
    {
        IEnumerable<TypeInheritanceAnalysis> analyses = assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract)
            .Select(x => new TypeInheritanceAnalysis(x, serviceType))
            .Where(x => x.InheritedTypes.Count > 0);

        foreach (TypeInheritanceAnalysis analysis in analyses)
        {
            foreach (Type type in analysis.InheritedTypes)
            {
                ServiceDescriptor serviceDescriptor = new(type, analysis.DerivedType, ServiceLifetime.Transient);
                services.Add(serviceDescriptor);
            }
        }

        return services;
    }
}