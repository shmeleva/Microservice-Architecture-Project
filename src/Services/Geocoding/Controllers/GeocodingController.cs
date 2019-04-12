using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Geocoding.Controllers
{
    [Route("api/v1/[controller]")]
    public class GeocodingController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;


        public GeocodingController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }


        // GET: api/v1/geocoding/coordinates?address={string}
        [HttpGet]
        [Route("coordinates")]
        [ProducesResponseType(typeof((double latitude, double longitude)), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<(double Latitude, double Longitude)>> GetCoordinatesAsync([FromQuery]string address = null)
        {
            // Key: ByAddress_{address}

            if (string.IsNullOrEmpty(address))
            {
                return BadRequest();
            }

            await distributedCache.SetAsync("333", new byte[] { });

            await Task.Delay(0);

            using (var httpClient = new HttpClient())
            {
                return Ok((new Random().Next(), new Random().Next()));
            }
        }

        // GET: api/v1/geocoding/address?latitude={double}&longitude={double}
        [HttpGet]
        [Route("address")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ResponseCache(Duration = 30)]
        public async Task<ActionResult<string>> GetAddressAsync(
            [FromQuery]double? latitude = null,
            [FromQuery]double? longitude = null)
        {
            // Key: ByCoordinate_{address}

            if (latitude == null || longitude == null)
            {
                return BadRequest();
            }

            await Task.Delay(0);

            using (var httpClient = new HttpClient())
            {
                return Ok("");
            }
        }
    }
}
