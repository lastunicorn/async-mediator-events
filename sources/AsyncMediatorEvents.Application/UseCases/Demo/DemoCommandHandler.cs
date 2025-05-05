using AsyncMediator;
using AsyncMediatorEvents.Application.Events.Demo;
using System.Diagnostics;

namespace AsyncMediatorEvents.Application.UseCases.Demo;

internal class DemoCommandHandler : ICommandHandler<DemoCommand>
{
    private readonly IMediator mediator;

    public DemoCommandHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public Task<ICommandWorkflowResult> Handle(DemoCommand command)
    {
        Debug.WriteLine($"[DemoCommandHandler] executing");

        try
        {
            LogMessage(command);
            RaiseDemoEvent();

            return Task.FromResult<ICommandWorkflowResult>(CommandWorkflowResult.Ok());
        }
        finally
        {
            Debug.WriteLine($"[DemoCommandHandler] finished");
        }
    }

    private static void LogMessage(DemoCommand command)
    {
        Debug.WriteLine($"[DemoCommandHandler] Message: {command.Message}");
    }

    private void RaiseDemoEvent()
    {
        DemoEvent @event = new()
        {
            Message = "This is an event raised by DemoCommand."
        };

        mediator.DeferEvent(@event);
    }
}