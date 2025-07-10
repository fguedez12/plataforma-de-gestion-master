using GobEfi.Web.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoSombreadoController : ControllerBase 
    {
        private readonly ITipoSombreadoService _service;
        public TipoSombreadoController(ITipoSombreadoService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> Get() {
            try
            {
                var res = await _service.Get();
                return Ok(res);
            }
            catch (Exception ex )
            {

                return BadRequest($"Ha ocurrido un error: {ex.Message}");
            }

        }
    }
}
