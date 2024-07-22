using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed.Caching.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    readonly IDistributedCache _distributedCache;

    public ValuesController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    [HttpGet("set")]
    public async Task<IActionResult> Set(string name, string surname)
    {
        _distributedCache.SetString("Name", name, options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });

        _distributedCache.Set("surname", Encoding.UTF8.GetBytes(surname), options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });
        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        var name = await _distributedCache.GetStringAsync("Name");
        var surnameBinary = await _distributedCache.GetAsync("surname");
        var surname = Encoding.UTF8.GetString(surnameBinary);
        return Ok(new
        {
            name,
            surname
        });
    }
}