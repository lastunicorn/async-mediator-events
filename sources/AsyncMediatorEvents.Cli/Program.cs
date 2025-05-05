using AsyncMediator;
using AsyncMediatorEvents.Cli.ConsoleCommands;
using AsyncMediator.Extensions.Autofac;
using Autofac;
using System.Reflection;
using AsyncMediatorEvents.Application.UseCases.Demo;

namespace AsyncMediatorEvents.Cli;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        IContainer container = SetupContainer();

        await ExecuteDemoUseCase(container);
    }

    private static IContainer SetupContainer()
    {
        ContainerBuilder containerBuilder = new();

        Assembly useCaseAssembly = typeof(DemoCommand).Assembly;
        containerBuilder.RegisterAsyncMediator(useCaseAssembly);

        return containerBuilder.Build();
    }

    private static async Task ExecuteDemoUseCase(IContainer container)
    {
        DemoConsoleCommand consoleCommand = container.Resolve<DemoConsoleCommand>();
        await consoleCommand.Execute();

        IMediator mediator = container.Resolve<IMediator>();
        await mediator.ExecuteDeferredEvents();
    }
}