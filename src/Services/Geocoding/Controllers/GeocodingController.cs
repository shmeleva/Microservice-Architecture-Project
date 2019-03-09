using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Geocoding.Controllers
{
    [Route("api/v1/[controller]")]
    public class GeocodingController : ControllerBase
    {
        // GET: api/v1/geocoding/coordinates?address={string}
        [HttpGet]
        [Route("coordinates")]
        [ProducesResponseType(typeof((double latitude, double longitude)), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<(double latitude, double longitude)>> GetCoordinatesAsync([FromQuery]string address = null)
        {
            if (string.IsNullOrEmpty(address))
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                return Ok((0, 0));
            }
        }

        // GET: api/v1/geocoding/address?latitude={double}&longitude={double}
        [HttpGet]
        [Route("address")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> GetAddressAsync(
            [FromQuery]double? latitude = null,
            [FromQuery]double? longitude = null)
        {
            if (latitude == null || longitude == null)
            {
                return BadRequest();
            }

            using (var httpClient = new HttpClient())
            {
                return Ok("");
            }
        }
    }
}
