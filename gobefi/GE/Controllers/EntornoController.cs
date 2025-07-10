using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.EntornoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntornoController : ControllerBase
    {
        private readonly IEntornoService _servicioEntorno;


        public EntornoController(IEntornoService servicioEntorno)
        {
            _servicioEntorno = servicioEntorno;
        }

        // GET: api/Entorno
        [HttpGet]
        public async Task<IActionResult> GetEntorno()
        {
            try
            {
                IEnumerable<EntornoModel> entornos = await _servicioEntorno.GetAllAsync();
                if (entornos.Count() == 0)
                {

                    return NotFound("No existen registros de entorno para mostrar");
                }

                return Ok(entornos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
