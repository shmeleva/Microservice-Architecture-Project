using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Carsharing.Models;
using Carsharing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carsharing.Controllers
{
    [Authorize]
    [Route("api/v1/cars")]
    public class CarsharingController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IIdentityService _identityService;


        public CarsharingController(
            IStorageService storageService,
            IIdentityService identityService)
        {
            _storageService = storageService;
            _identityService = identityService;
        }


        // GET: api/v1/cars?latitude={double}&longitude={double}&radius={double}
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Car>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Car>> GetAvailableCarsAsync(
            [FromQuery, Required]double? latitude = null,
            [FromQuery, Required]double? longitude = null,
            [FromQuery]double radius = 500)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.Identity.Name;

            //var cars = await _storageService.GetAvailableCarsAsync(latitude.Value, longitude.Value, radius);
            return Ok(new List<Car> { new Car { Id = Guid.NewGuid() } });
        }

        // POST api/v1/cars/book
        [HttpPost]
        [Route("book")]
        [ProducesResponseType(typeof(BookResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<BookResult>> BookCarAsync([FromBody]BookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            throw new NotImplementedException();
        }

        // POST api/v1/cars/return
        [HttpPost]
        [Route("return")]
        [ProducesResponseType(typeof(BookResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ReturnResult>> ReturnCarAsync([FromBody]ReturnRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            throw new NotImplementedException();
        }
    }
}
