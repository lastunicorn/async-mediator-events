using AsyncMediator;
using AsyncMediatorEvents.Application.UseCases.Demo;
using AsyncMediatorEvents.Infrastructure.RequestBusModel;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMediatorEvents.WebApi.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly IRequestBus requestBus;

    public DemoController(IRequestBus requestBus)
    {
        this.requestBus = requestBus;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string message)
    {
        DemoCommand command = new()
        {
            Message = message
        };
        ICommandWorkflowResult result = await requestBus.Send(command);

        return result.Process<DemoResponse>(response =>
        {
            return Ok(response.Message);
        });
    }
}
