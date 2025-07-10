using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.UnidadMedidaModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly IUnidadMedidaService _servUnidadMedida;

        public UnidadMedidaController(IUnidadMedidaService servUnidadMedida)
        {
            _servUnidadMedida = servUnidadMedida;
        }

        // GET: api/UnidadMedida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedidaModel>>> GetUnidadMedidas()
        {
            IEnumerable<UnidadMedidaModel> unidadesMedidas = await _servUnidadMedida.GetAll();

            if (unidadesMedidas == null)
            {
                return NotFound();
            }

            return Ok(unidadesMedidas);
        }

        // GET: api/UnidadMedida/asociadosByEnergeticoId
        [HttpGet("asociadosByEnergeticoId/{energeticoId}")]
        public async Task<ActionResult<IEnumerable<UnidadMedidaModel>>> AsociadosByEnergeticoId(long energeticoId)
        {
            IEnumerable<UnidadMedidaModel> unidadesMedidas = await _servUnidadMedida.GetAsociadosByEnergeticoId(energeticoId);

            if (unidadesMedidas == null)
            {
                return NotFound();
            }

            return Ok(unidadesMedidas);
        }

        // GET: api/UnidadMedida/noAsociadosByEnergeticoId
        [HttpGet("noAsociadosByEnergeticoId/{energeticoId}")]
        public async Task<ActionResult<IEnumerable<UnidadMedidaModel>>> NoAsociadosByEnergeticoId(long energeticoId)
        {
            IEnumerable<UnidadMedidaModel> unidadesMedidas = await _servUnidadMedida.GetNoAsociadosByEnergeticoId(energeticoId);

            if (unidadesMedidas == null)
            {
                return NotFound();
            }

            return Ok(unidadesMedidas);
        }
    }
}