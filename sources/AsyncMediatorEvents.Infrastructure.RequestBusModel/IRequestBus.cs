using AsyncMediator;

namespace AsyncMediatorEvents.Infrastructure.RequestBusModel;

public interface IRequestBus
{
    Task<TResult> Query<TCriteria, TResult>(TCriteria criteria);

    Task<ICommandWorkflowResult> Send<TCommand>(TCommand command) where TCommand : ICommand;
}