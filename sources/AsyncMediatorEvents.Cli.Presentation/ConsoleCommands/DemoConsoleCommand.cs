using AsyncMediator;
using AsyncMediatorEvents.Application.UseCases.Demo;

namespace AsyncMediatorEvents.Cli.Presentation.ConsoleCommands;

public class DemoConsoleCommand
{
    private readonly IMediator mediator;

    public DemoConsoleCommand(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Execute()
    {
        DemoCommand command = new()
        {
            Message = "This DemoCommand is called by the main thread."
        };

        await mediator.Send(command);
    }
}