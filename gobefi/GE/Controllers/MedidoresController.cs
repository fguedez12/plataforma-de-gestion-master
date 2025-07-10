using System.Collections.Generic;
using System.Threading.Tasks;
using GE.Models.MedidorModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.MedidorModels;
using Microsoft.AspNetCore.Mvc;

namespace GE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedidoresController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IMedidorService _servMedidores;

        public MedidoresController(ApplicationDbContext ctx, IMedidorService servMedidores)
        {
            _ctx = ctx;
            _servMedidores = servMedidores;
        }

        // GET: api/Medidores
        [HttpGet]
        public IEnumerable<Medidor> GetMedidores()
        {
            return _ctx.Medidores;
        }

        // GET: api/Medidores/ByNumeroCliente/5
        [HttpGet("ByNumeroCliente/{numClienteId}")]
        public async Task<IActionResult> GetByNumeroCliente([FromRoute] long numClienteId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<MedidorToDDL> numClienteToDDL = await _servMedidores.ByNumeroCliente(numClienteId);

            if (numClienteToDDL == null)
            {
                return NotFound();
            }

            return Ok(numClienteToDDL);
        }

        // GET: api/Medidores/ByNumeroCliente/5
        [HttpGet("ParaCompra/ByNumClienteId/{numClienteId}/ByDivisionId/{divisionId}")]
        public async Task<IActionResult> GetByNumClienteIdDivisionId([FromRoute] long numClienteId, [FromRoute] long divisionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<MedidorToDDL> numClienteToDDL = await _servMedidores.ByNumClienteIdDivisionId(numClienteId, divisionId);

            if (numClienteToDDL == null)
            {
                return NotFound();
            }

            return Ok(numClienteToDDL);
        }

        [HttpGet("ByCompraId/{compraId}")]
        public async Task<IActionResult> GetByCompraId([FromRoute] long compraId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<MedidorToDDL> numClienteToDDL = await _servMedidores.GetByCompraId(compraId);

            if (numClienteToDDL == null)
            {
                return NotFound();
            }

            return Ok(numClienteToDDL);
        }

        [HttpPost]
        [Route("CheckExistMedidor")]
        public async Task<IActionResult> GetByNumClienteIdAndNumMedidor([FromBody] MedidorParametrosModel parametroMedidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MedidorModel medidor = await _servMedidores.GetByNumClienteIdAndNumMedidor(parametroMedidor);

            if (medidor == null)
            {
                return NotFound(medidor);
            }

            return Ok(medidor);
        }

    }
}