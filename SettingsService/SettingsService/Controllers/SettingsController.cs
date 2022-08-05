using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace SettingsService.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : ControllerBase
{
    private static ConcurrentDictionary<string, string> _settings = new();

    public SettingsController()
    {
        _settings.TryAdd("setting", "someValue");
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_settings);
    }
}
