using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carsharing.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Carsharing.Controllers
{
    [Route("api/v1/[controller]")]
    public class CarsharingController : Controller
    {
        // GET: api/v1/carsharing/cars?latitude={double}&longitude={double}&address={string}&radius={double}
        [HttpGet]
        [Route("cars")]
        [ProducesResponseType(typeof(IEnumerable<Car>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<Car>> GetAsync(
            [FromQuery] double latitude = 0,
            [FromQuery] double longitude = 0,
            [FromQuery] string address = null,
            [FromQuery] double radius = 0)
        {


            throw new NotImplementedException();
        }
        
        // POST api/v1/book
        [HttpPost]
        [Route("book")]
        [ProducesResponseType(typeof(BookResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<BookResult> BookAsync([FromBody]BookRequest request)
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
        public Task<ReturnResult> ReturnAsync([FromBody]ReturnRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
