using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Carsharing.Models;
using Carsharing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing.Controllers
{
    [Authorize]
    [Route("api/v1/cars")]
    public class CarsharingController : ControllerBase
    {
        private readonly IStorageService storageService;


        public CarsharingController(IStorageService storageService)
        {
            this.storageService = storageService;
        }


        // GET: api/v1/cars/available?latitude={double}&longitude={double}&radius={double}
        [HttpGet("available")]
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

            return Ok(await storageService.FindAvailableCarsAsync(latitude.Value, longitude.Value, radius));
        }

        // POST api/v1/cars/book
        [HttpPost]
        [Route("book")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> BookCarAsync([FromBody]BookRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await storageService.FindCarAsync(request.CarId);

            if (!string.IsNullOrEmpty(car.Username))
            {
                return BadRequest(new { Message = $"Our condolences, you've been slow and car {request.CarId} has beed taken." });
            }

            car.Username = User.Identity.Name;
            await storageService.UpdateCarAsync(request.CarId, car);

            return Ok(new { Message = $"Car {request.CarId} is all yours. Have a nice ride!" });
        }

        // POST api/v1/cars/return
        [HttpPost]
        [Route("return")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> ReturnCarAsync([FromBody]ReturnRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await storageService.FindCarAsync(request.CarId);

            if (car.Username != User.Identity.Name)
            {
                return BadRequest(new { Message = $"Car {request.CarId} is not yours to return!" });
            }

            await storageService.UpdateCarAsync(request.CarId, new Car
            {
                Id = request.CarId,
                Latitude = request.Latitude,
                Longitude = request.Longitude
            });

            return Ok(new { Message = $"{request.CarId}! It's nice to have it back in ({request.Latitude},{request.Longitude})." });
        }
    }
}
