using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.NumeroPisoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroPisoController : ControllerBase
    {
        private readonly INumeroPisoService _servicioNumPiso;

        public NumeroPisoController(INumeroPisoService servicioNumPiso)
        {
            _servicioNumPiso = servicioNumPiso;
        }

        // GET: api/NumeroPiso
        [HttpGet]
        public async Task<IActionResult> GetNumeroPiso()
        {
            try
            {
                IEnumerable<NumeroPisoModel> entornos = await _servicioNumPiso.GetAllAsync();
                if (entornos.Count() == 0)
                {

                    return NotFound("No existen registros de numero de piso");
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
