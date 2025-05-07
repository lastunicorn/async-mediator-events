using AsyncMediatorEvents.Cli.Presentation.ConsoleCommands;
using Autofac;

namespace AsyncMediatorEvents.Cli;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("For logs, check the debug output.");

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
    }
}
