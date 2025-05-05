using AsyncMediator;

namespace AsyncMediatorEvents.Infrastructure.RequestBusModel;

public class EventBus
{
    private readonly IMediator mediator;

    public EventBus(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public void DeferEvent<TEvent>(TEvent @event)
        where TEvent : IDomainEvent
    {
        mediator.DeferEvent(@event);
    }
}
