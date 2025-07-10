using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoAgrupamientoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAgrupamientoController : ControllerBase
    {
        private readonly ITipoAgrupamientoService _tipoAgrupamientoService;

        public TipoAgrupamientoController(ITipoAgrupamientoService tipoAgrupamientoService) {

            _tipoAgrupamientoService = tipoAgrupamientoService;
        }

        // GET: api/TipoAgrupamiento
        [HttpGet]
        public async Task<IActionResult> GetTipoAgrupamiento()
        {
            try
            {
                IEnumerable<TipoAgrupamientoModel> agrupamientos = await _tipoAgrupamientoService.GetAllAsync();
                if (agrupamientos.Count() == 0) {

                    return NotFound("No existen registros de Tipo de Agrupamiento para mostrar");
                }

                return Ok(agrupamientos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
