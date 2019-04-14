using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;


        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }


        // POST: api/v1/identity/create?username={string}&password={string}
        // TODO: Replace with a body
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> SignUpAsync(
            [FromQuery, Required]string username = null,
            [FromQuery, Required]string password = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await identityService.CreateUserAsync(username, password);
            }
            catch (ArgumentException)
            {
                return BadRequest(new { Message = "I don't approve this username+password combination." });
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    Message = "It's a 500 but we stll want you to join us! Please, try a bit later."
                });
            }

            return Ok();
        }

        // GET: api/v1/identity/jwt?username={string}&password={string}
        [HttpGet]
        [Route("jwt")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> GetJwtAsync(
            [FromQuery, Required]string username = null,
            [FromQuery, Required]string password = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await identityService.IssueUserJwtAsync(username, password));
            }
            catch (ArgumentException)
            {
                return BadRequest(new { Message = "I don't approve this username+password combination." });
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = "Something went terribly wrong." });
            }
        }
    }
}
