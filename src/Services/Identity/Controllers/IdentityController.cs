using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers
{
    [Route("api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        // POST: api/v1/identity/create
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateIdentityAsync()
        {
            throw new NotImplementedException();
        }

        // GET: api/v1/identity/jwt?username={string}&password={string}
        [HttpGet]
        [Route("jwt")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> GetJwtAsync(
            [FromQuery]string username = null,
            [FromQuery]string password = null)
        {
            throw new NotImplementedException();
        }
    }
}
