using AsyncMediator;
using AsyncMediatorEvents.Application.UseCases.Demo;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMediatorEvents.WebApi.Presentation.Controllers;

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

        return result.Process<DemoResponse>(response =>
        {
            return Ok(response.Message);
        });
    }
}
