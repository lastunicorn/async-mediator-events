using AsyncMediator;
using System.Diagnostics;

namespace AsyncMediatorEvents.Business.Events.Demo;

public class DemoEventHandler : IEventHandler<DemoEvent>
{
    public Task Handle(DemoEvent @event)
    {
        Debug.WriteLine($"[DemoCommandHandler] executing");

        try
        {
            LogMessage(@event);

            return Task.CompletedTask;
        }
        finally
        {
            Debug.WriteLine($"[DemoCommandHandler] finished");
        }
    }

    private static void LogMessage(DemoEvent @event)
    {
        Debug.WriteLine($"[DemoEventHandler] Message: {@event.Message}");
    }
}
