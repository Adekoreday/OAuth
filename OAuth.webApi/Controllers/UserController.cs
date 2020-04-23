using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using  OAuth.Data;
using OAuth.Domain;

namespace OAuth.webApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;


        pubic UserController(ILogger<UserController> logger) {
         _logger = logger;
        }

         [HttpGet]
         public async Task<IActionResult> Get() {
            return ok();
         }
    }
}
