using AsyncMediator;

namespace AsyncMediatorEvents.Application;

internal abstract class QueryHandlerBase<TQuery, TResponse> : IQuery<TQuery, TResponse>
{
    protected IMediator Mediator { get; }

    public QueryHandlerBase(IMediator mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public Task<TResponse> Query(TQuery criteria)
    {
        return DoHandle(criteria);
    }

    protected abstract Task<TResponse> DoHandle(TQuery criteria);
}