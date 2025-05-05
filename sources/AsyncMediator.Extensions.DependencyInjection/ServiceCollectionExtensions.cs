using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;

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
