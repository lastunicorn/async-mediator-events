using AsyncMediator;
using System.Diagnostics;

namespace AsyncMediatorEvents.Application.Events.Demo;

public class DemoEventHandler : IEventHandler<DemoEvent>
{
    public Task Handle(DemoEvent @event)
    {
        Debug.WriteLine($"[DemoEventHandler] executing");

        try
        {
            LogMessage(@event);

            return Task.CompletedTask;
        }
        finally
        {
            Debug.WriteLine($"[DemoEventHandler] finished");
        }
    }

    private static void LogMessage(DemoEvent @event)
    {
        Debug.WriteLine($"[DemoEventHandler] Message: {@event.Message}");
    }
}
