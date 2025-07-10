using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DemoApiRateLimiter
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("tokenBucket")] // mention the group of rate limiter being used
    public class Tester3Controller : ControllerBase
    {
            [HttpGet]
            public IActionResult Get()
            {
                return Ok("Hello from test3 controller");
            }

    }
}
