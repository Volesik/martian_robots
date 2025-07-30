using Microsoft.AspNetCore.Mvc;

namespace MartianRobots.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoverController : ControllerBase
    {
        public RoverController()
        {
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }
    }
}
