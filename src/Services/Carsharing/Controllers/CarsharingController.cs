using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carsharing.Models;
using Carsharing.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carsharing.Controllers
{
    [Route("api/v1/[controller]")]
    public class CarsharingController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IIdentityService _identityService;
        private readonly IGeocodingService _geocodingService;


        public CarsharingController(
            IStorageService storageService,
            IIdentityService identityService,
            IGeocodingService geocodingService)
        {
            _storageService = storageService;
            _identityService = identityService;
            _geocodingService = geocodingService;
        }


        // GET: api/v1/carsharing/cars?latitude={double}&longitude={double}&address={string}&radius={double}
        [HttpGet]
        [Route("cars")]
        [ProducesResponseType(typeof(IEnumerable<Car>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Car>> GetAsync(
            [FromQuery]double? latitude = null,
            [FromQuery]double? longitude = null,
            [FromQuery]double radius = 500)
        {
            if (latitude == null || longitude == null)
            {
                return BadRequest();
            }

            var cars = await _storageService.GetAvailableCarsAsync(latitude.Value, longitude.Value, radius);
            return Ok(cars);
        }
        
        // POST api/v1/book
        [HttpPost]
        [Route("book")]
        [ProducesResponseType(typeof(BookResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookResult>> BookAsync([FromBody]BookRequest request)
        {
            throw new NotImplementedException();
        }

        // POST api/v1/return
        [HttpPost]
        [Route("return")]
        [ProducesResponseType(typeof(BookResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public Task<ActionResult<ReturnResult>> ReturnAsync([FromBody]ReturnRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
