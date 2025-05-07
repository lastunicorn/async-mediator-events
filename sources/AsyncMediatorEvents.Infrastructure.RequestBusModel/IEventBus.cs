using AsyncMediator;

namespace AsyncMediatorEvents.Infrastructure.RequestBusModel;

public interface IEventBus
{
    void DeferEvent<TEvent>(TEvent @event) where TEvent : IDomainEvent;
}