using AsyncMediator.Extensions.Autofac;
using Autofac;
using System.Reflection;
using AsyncMediatorEvents.Application.UseCases.Demo;
using AsyncMediatorEvents.Cli.Presentation.ConsoleCommands;

namespace AsyncMediatorEvents.Cli;

internal static class Setup
{
    public static void SetupContainer(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<DemoConsoleCommand>().AsSelf();

        Assembly useCaseAssembly = typeof(DemoCommand).Assembly;
        containerBuilder.RegisterAsyncMediator(useCaseAssembly);
    }
}