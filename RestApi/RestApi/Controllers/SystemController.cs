using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApi.Application.Settings.Queries.Get;
using RestApi.Common;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    private readonly ServiceId _id;
    private readonly IMediator _mediator;

    public SystemController(IMediator mediator)
    {
        _id = new ServiceId();
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var settings = await _mediator.Send(new GetSettingsQuery());

        return Ok(new
        {
            Id = _id.Id,
            Settings = settings
        });
    }
}
