using Microsoft.AspNetCore.Mvc;

namespace Geocoding.Controllers
{
    [Route("api/v1/[controller]")]
    public class HealthController : Controller
    {
        [HttpGet("status")]
        public IActionResult Status() => Ok();
    }
}
