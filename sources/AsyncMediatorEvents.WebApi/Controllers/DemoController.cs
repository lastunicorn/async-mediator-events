using AsyncMediator;
using AsyncMediatorEvents.Business.UseCases.Demo;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMediatorEvents.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly IMediator mediator;

    public DemoController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(string message)
    {
        DemoCommand command = new()
        {
            Message = message
        };
        ICommandWorkflowResult result = await mediator.Send(command);

        return Ok(result);
    }
}
