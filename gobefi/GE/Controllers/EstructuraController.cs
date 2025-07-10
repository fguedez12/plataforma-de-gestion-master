using GobEfi.Web.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstructuraController : ControllerBase
    {
        private readonly IEstructuraService _service;

        public EstructuraController(IEstructuraService service )
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> Get() 
        {
            try
            {
                return  Ok(await _service.GetAll());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }    
        }

        [HttpGet("espesores")]
        public async Task<ActionResult> GetEspesores()
        {
            try
            {
                return Ok(await _service.GetEspesores());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
