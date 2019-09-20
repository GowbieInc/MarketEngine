using Microsoft.AspNetCore.Mvc;

namespace MarketEngine.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Pingou!");
        }
    }
}