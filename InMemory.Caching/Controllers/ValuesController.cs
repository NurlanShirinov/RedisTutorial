using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Caching.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    readonly IMemoryCache _memoryCache;

    public ValuesController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }


    //[HttpGet("set/{name}")]
    //public void SeNamet(string name)
    //{
    //    //Name key - i ile nrln value-sini cache etdik
    //    _memoryCache.Set("name", name);
    //}

    //[HttpGet]
    //public string GetName()
    //{
    //    //verdiyimiz key ile valuesin cache-den cekirik 
    //    return _memoryCache.Get<string>("name");
    //}

    //[HttpGet("tryGetValue")]
    //public string GetTryGetValue()
    //{

    //    if (_memoryCache.TryGetValue<string>("name", out string name))
    //    {
    //        return name.Substring(3);
    //    }
    //    return "";
    //}


    [HttpGet("SetDate")]
    public void SetDate()
    {
        _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });
    }

    [HttpGet("GetDate")]
    public DateTime GetDate()
    {
        return _memoryCache.Get<DateTime>("date");
    }

}