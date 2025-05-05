using AsyncMediator;

namespace AsyncMediatorEvents.Infrastructure.RequestBusModel;

public class RequestBus
{
    private readonly IMediator mediator;

    public RequestBus(IMediator mediator)
    {
        this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<TResult> Query<TCriteria, TResult>(TCriteria criteria)
    {
        TResult response = await mediator.Query<TCriteria, TResult>(criteria);
        await mediator.ExecuteDeferredEvents();

        return response;
    }

    public async Task<ICommandWorkflowResult> Send<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        ICommandWorkflowResult response = await mediator.Send(command);
        await mediator.ExecuteDeferredEvents();

        return response;
    }
}
