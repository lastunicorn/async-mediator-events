using AsyncMediator.Extensions.Autofac;
using AsyncMediatorEvents.Application.UseCases.Demo;
using AsyncMediatorEvents.Cli.Presentation.ConsoleCommands;
using AsyncMediatorEvents.Infrastructure.RequestBusModel;
using AsyncMediatorEvents.Infrastructure.RequestBusModel.AsyncMediator;
using Autofac;
using System.Reflection;

namespace AsyncMediatorEvents.Cli;

internal static class Setup
{
    public static void SetupContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<RequestBus>().As<IRequestBus>();
        containerBuilder.RegisterType<EventBus>().As<IEventBus>();

        containerBuilder.RegisterType<DemoConsoleCommand>().AsSelf();

        Assembly useCaseAssembly = typeof(DemoCommand).Assembly;
        containerBuilder.RegisterAsyncMediator(useCaseAssembly);
    }
}