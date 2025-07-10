using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DemoApiRateLimiter
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("slidingWindow")] // mention the group of rate limiter being used
    public class Tester2Controller : ControllerBase
    {
            [HttpGet]
            public IActionResult Get()
            {
                return Ok("Hello from test2 controller");
            }

    }
}
