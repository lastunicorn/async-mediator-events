using AsyncMediator;

namespace AsyncMediatorEvents.Application.UseCases.Demo;

public class DemoCommand : ICommand
{
    public string Message { get; set; }
}
