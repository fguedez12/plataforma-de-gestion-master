using System.Collections.Generic;
using System.Threading.Tasks;
using GE.Models.NumeroClienteModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumClientesController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly ApplicationDbContext _ctx;
        private readonly INumeroClienteService _servNumCliente;

        public NumClientesController(ApplicationDbContext ctx, INumeroClienteService servNumCliente)
        {
            _ctx = ctx;
            _servNumCliente = servNumCliente;
        }

        // GET: api/NumClientes
        [HttpGet]
        public IEnumerable<NumeroCliente> GetNumClientes()
        {
            return _ctx.NumeroClientes;
        }

        // GET: api/NumClientes/ByDivision/5
        [HttpGet("byDivisionId/{divisionId}/byEnergeticoId/{energeticoId}")]
        public async Task<IActionResult> GetByDivision([FromRoute] long divisionId, [FromRoute] long energeticoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<NumClienteToDDL> numClienteToDDL = await _servNumCliente.ByDivision(divisionId, energeticoId);

            if (numClienteToDDL == null)
            {
                return NotFound();
            }

            return Ok(numClienteToDDL);
        }

        [HttpGet("validaNumeroByMedidor/{divisionId}/byEnergetico/{energeticoId}/byNumeroCliente/{numeroClienteId}")]
        public async Task<bool> validaNumeroByMedidor([FromRoute] long divisionId, [FromRoute] long energeticoId, [FromRoute] long numeroClienteId)
        {
            bool result = false;

            await Task.Run(() =>

            result = _servNumCliente.validaNumeroByMedidor(divisionId, energeticoId, numeroClienteId)

            );

            return result;
        }

        // GET: api/NumClientes/ByDivision/5
        [HttpGet("byEdificioId/{edificioId}/byEnergeticoId/{energeticoId}")]
        public async Task<IActionResult> GetByEdificio([FromRoute] long edificioId, [FromRoute] long energeticoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<NumClienteToDDL> numClienteToDDL = await _servNumCliente.ByEdificioId(edificioId, energeticoId);

            if (numClienteToDDL == null)
            {
                return NotFound();
            }

            return Ok(numClienteToDDL);
        }
    }
}