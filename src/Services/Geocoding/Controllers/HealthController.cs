using Microsoft.AspNetCore.Mvc;

namespace Geocoding.Controllers
{
    [Route("api/v1/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Status() => Ok();
    }
}
