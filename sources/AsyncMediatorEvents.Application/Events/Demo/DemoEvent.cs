using AsyncMediator;

namespace AsyncMediatorEvents.Business.Events.Demo;

public class DemoEvent : IDomainEvent
{
    public string Message { get; set; }
}