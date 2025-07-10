using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoNivelPisoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TIpoNivelPisoController : ControllerBase
    {
        private readonly ITipoNivelPisoService _servicioTipoNivelPiso;


        public TIpoNivelPisoController(ITipoNivelPisoService servicioTipoNivelPiso)
        {
            _servicioTipoNivelPiso = servicioTipoNivelPiso;
        }

        // GET: api/TIpoNivelPiso
        [HttpGet]
        public IActionResult  GetTipoNivelPiso()
        {
            try
            {
                IEnumerable<TipoNivelPisoModel> tiposNivel = _servicioTipoNivelPiso.All();
                if (tiposNivel.Count() == 0)
                {

                    return NotFound("No existen registros de Tipo de Nivel Piso para mostrar");
                }

                return Ok(tiposNivel);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
