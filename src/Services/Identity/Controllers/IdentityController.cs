using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Identity.Controllers
{
    [Route("api/v1/[controller]")]
    public class IdentityController : ControllerBase
    {
        public async Task<ActionResult> RegisterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> LoginAsync()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public async Task<ActionResult> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
