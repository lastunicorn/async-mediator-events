using AsyncMediator;

namespace AsyncMediatorEvents.Application.Events.Demo;

public class DemoEvent : IDomainEvent
{
    public string Message { get; set; }
}