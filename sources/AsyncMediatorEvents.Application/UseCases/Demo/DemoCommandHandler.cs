using AsyncMediator;
using AsyncMediatorEvents.Application.Events.Demo;
using System.Diagnostics;

namespace AsyncMediatorEvents.Application.UseCases.Demo;

internal class DemoCommandHandler : CommandHandlerBase<DemoCommand>
{
    public DemoCommandHandler(IMediator mediator)
        : base(mediator)
    {
    }

    protected override Task<ICommandWorkflowResult> DoHandle(DemoCommand command)
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

        Mediator.DeferEvent(@event);
    }
}

public class DemoResponse
{
    public string Message { get; set; }
}