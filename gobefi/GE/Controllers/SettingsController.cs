using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetConfigurationValue([FromQuery] string sectionName, [FromQuery] string paramName)
        {
            var parameterValue = _configuration[$"{sectionName}:{paramName}"];
            return Ok(parameterValue);
        }
    }
}
