using AsyncMediator;
using Autofac;
using AsyncMediatorEvents.Cli.Presentation.ConsoleCommands;

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
        Setup.SetupContainer(containerBuilder);
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
