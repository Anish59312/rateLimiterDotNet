using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DemoApiRateLimiter
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class TesterController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from test controller");
        }

        //[DisableRateLimiting("fixed")];

    }
}
