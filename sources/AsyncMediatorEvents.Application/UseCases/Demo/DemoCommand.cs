using AsyncMediator;

namespace AsyncMediatorEvents.Business.UseCases.Demo;

public class DemoCommand : ICommand
{
    public string Message { get; set; }
}
