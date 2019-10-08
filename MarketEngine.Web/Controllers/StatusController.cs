using MarketEngine.Domain.Services.Status.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketEngine.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService statusService;
        public StatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(statusService.TestService());
        }
    }
}