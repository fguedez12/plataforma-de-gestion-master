using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GE.Models.ParametrosMedicionModels;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroMedicionController : ControllerBase
    {
        private readonly IParametroMedicionService _servParametroMedicions;

        public ParametroMedicionController(IParametroMedicionService servParametroMedicions)
        {
            _servParametroMedicions = servParametroMedicions;
        }

        [HttpGet]
        public async Task<IActionResult> GetParametrosMedicion()
        {
            IEnumerable<ParametrosMedicionModel> result = await _servParametroMedicions.AllAsync();

            return Ok(result);
        }

    }
}