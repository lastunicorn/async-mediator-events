using AsyncMediator;
using AsyncMediatorEvents.Application.UseCases.Demo;
using AsyncMediatorEvents.Infrastructure.RequestBusModel;

namespace AsyncMediatorEvents.Cli.Presentation.ConsoleCommands;

public class DemoConsoleCommand
{
    private readonly IRequestBus requestBus;

    public DemoConsoleCommand(IRequestBus requestBus)
    {
        this.requestBus = requestBus;
    }

    public async Task Execute()
    {
        DemoCommand command = new()
        {
            Message = "This DemoCommand is called by the main thread."
        };

        await requestBus.Send(command);
    }
}