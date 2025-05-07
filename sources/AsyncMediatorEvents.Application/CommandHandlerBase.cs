using AsyncMediator;

namespace AsyncMediatorEvents.Application;

internal abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    protected IMediator Mediator { get; }

    public CommandHandlerBase(IMediator mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public async Task<ICommandWorkflowResult> Handle(TCommand command)
    {
        ICommandWorkflowResult result = await DoHandle(command);
        await Mediator.ExecuteDeferredEvents();

        return result;
    }

    protected abstract Task<ICommandWorkflowResult> DoHandle(TCommand command);
}