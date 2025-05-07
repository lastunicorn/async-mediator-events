using AsyncMediator;
using AsyncMediatorEvents.Application.Events.Demo;
using AsyncMediatorEvents.Infrastructure.RequestBusModel;
using System.Diagnostics;

namespace AsyncMediatorEvents.Application.UseCases.Demo;

internal class DemoCommandHandler : ICommandHandler<DemoCommand>
{
    private readonly IEventBus eventBus;

    public DemoCommandHandler(IEventBus eventBus)
    {
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
    }

    public Task<ICommandWorkflowResult> Handle(DemoCommand command)
    {
        Debug.WriteLine($"[DemoCommandHandler] start");

        try
        {
            LogMessage(command);
            RaiseDemoEvent();

            CommandWorkflowResult<DemoResponse> response = new(new DemoResponse
            {
                Message = "For logs, check the debug output."
            });

            return Task.FromResult<ICommandWorkflowResult>(response);
        }
        finally
        {
            Debug.WriteLine($"[DemoCommandHandler] end");
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

        eventBus.DeferEvent(@event);
    }
}
