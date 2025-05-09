using AsyncMediator;
using System.Diagnostics;

namespace AsyncMediatorEvents.Application.Events.Demo;

internal class DemoEventHandler : IEventHandler<DemoEvent>
{
    public Task Handle(DemoEvent @event)
    {
        Debug.WriteLine($"[DemoEventHandler] start");

        try
        {
            LogMessage(@event);

            return Task.CompletedTask;
        }
        finally
        {
            Debug.WriteLine($"[DemoEventHandler] end");
        }
    }

    private static void LogMessage(DemoEvent @event)
    {
        Debug.WriteLine($"[DemoEventHandler] Message: {@event.Message}");
    }
}