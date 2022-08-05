using Microsoft.AspNetCore.Mvc;
using RestApi.Common;

namespace RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemController : ControllerBase
{
    private readonly ServiceId _id;

    public SystemController()
    {
        _id = new ServiceId();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(new
        {
            Id = _id.Id,
        });
    }
}
