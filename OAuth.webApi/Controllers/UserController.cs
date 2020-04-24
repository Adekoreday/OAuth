using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using  OAuth.Data;
using OAuth.Domain;
using Microsoft.AspNetCore.Authorization;

namespace OAuth.webApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;


        public UserController(ILogger<UserController> logger) {
         _logger = logger;
        }
         [Authorize]
         [HttpGet]
         public ActionResult GetUsers() {
            return Ok(new {
               message = "app up" 
           });
         }
    }
}
