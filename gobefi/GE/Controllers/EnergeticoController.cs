using System.Collections.Generic;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoDivisionModels;
using GobEfi.Web.Models.EnergeticoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergeticoController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly IEnergeticoService _servEnergetico;
        private readonly ICompraService _servCompra;
        private readonly ApplicationDbContext _ctx;

        public EnergeticoController(IEnergeticoService servEnergetico, ICompraService servCompra, ApplicationDbContext context)
        {
            _servEnergetico = servEnergetico;
            _servCompra = servCompra;
            _ctx = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnergeticos()
        {
            IEnumerable<EnergeticoModel> energeticos = await _servEnergetico.GetAllAsync();

            if (energeticos == null)
            {
                return NotFound();
            }

            return Ok(energeticos);
        }

        [HttpGet]
        [Route("[action]/{divisionId}")]
        public async Task<IActionResult> GetByDivisionId(long divisionId)
        {
            IEnumerable<EnergeticoDivisionModel> energeticoDivision = await _servEnergetico.GetByDivisionId(divisionId);

            if (energeticoDivision == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("DivisionId", divisionId.ToString());

            return Ok(energeticoDivision);
        }

        [HttpGet("GetEnergeticosActivos/{divisionId}")]
        public async Task<IActionResult> GetEnergeticosActivos([FromRoute] long divisionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool tienePermisos = await _servCompra.ValidaPermiso(divisionId);

            if (!tienePermisos)
                return Unauthorized();

            //var energeticoForConsumos = _servEnergetico.ByDivision(divisionId);
            var energeticoForConsumos = _servEnergetico.GetActivosByDivision(divisionId);

            if (energeticoForConsumos == null)
            {
                return NotFound();
            }

            return Ok(energeticoForConsumos);
        }

        [HttpGet("GetByEdificioId/{edificioId}")]
        public async Task<IActionResult> GetByEdificioId([FromRoute] long edificioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<EnergeticoActivoModel> energeticos = await _servEnergetico.GetByEdificioId(edificioId);

            if (energeticos == null)
            {
                return NotFound();
            }

            return Ok(energeticos);
        }
    }
}