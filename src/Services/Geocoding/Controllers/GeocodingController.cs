using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Geocoding.Models;
using Geocoding.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Geocoding.Controllers
{
    [Route("api/v1/geocode")]
    public class GeocodingController : ControllerBase
    {
        private readonly IDistributedCache distributedCache;
        private readonly IGeocodingService geocodingService;

        public GeocodingController(
            IDistributedCache distributedCache,
            IGeocodingService geocodingService)
        {
            this.distributedCache = distributedCache;
            this.geocodingService = geocodingService;
        }


        // GET: api/v1/geocode/forward?address={string}
        [HttpGet("forward")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCoordinatesAsync([FromQuery, Required]string address = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await GetFromCacheOrFetchAsync($"Geocoding_{address}",
                async () => await geocodingService.GetLocationByAddressAsync(address));

        }

        // GET: api/v1/geocode/reverse?latitude={double}&longitude={double}
        [HttpGet]
        [Route("reverse")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAddressAsync(
            [FromQuery, Required]double? latitude = null,
            [FromQuery, Required]double? longitude = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await GetFromCacheOrFetchAsync(
                $"Geocoding_Reverse_{latitude},{longitude}",
                async () => await geocodingService.GetLocationByCoordinatesAsync(latitude.Value, longitude.Value));
        }

        public async Task<IActionResult> GetFromCacheOrFetchAsync(string key, Func<Task<Location>> fetchAsync)
        {
            var location = await distributedCache.GetStringAsync(key);
            if (location != null)
            {
                return Ok(location);
            }

            try
            {
                location = JsonConvert.SerializeObject(await fetchAsync());
                await distributedCache.SetStringAsync(key, location, new DistributedCacheEntryOptions
                {
                    // TODO: Move to config.
                    AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30),
                });
                return Ok(location);
            }
            catch
            {
                return NoContent();
            }
        }
    }
}
