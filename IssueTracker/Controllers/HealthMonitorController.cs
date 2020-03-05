using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class HealthMonitorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("API is working");
        }
    }
}