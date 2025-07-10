using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.ComunaModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComunaController : ControllerBase
    {
        private readonly IComunaService _servComuna;

        public ComunaController(IComunaService servComuna)
        {
            _servComuna = servComuna;
        }

        [HttpGet]
        public async Task<IActionResult> GetComunas()
        {
            IEnumerable<ComunaModel> comunas = await _servComuna.GetAllAsync();

            return Ok(comunas);
        }

        [HttpGet("{comunaId}")]
        public async Task<IActionResult> GetComuna(long comunaId)
        {
            ComunaModel comunas = await _servComuna.GetAsync(comunaId);

            return Ok(comunas);
        }

        // GET: api/Comuna/getByProvinciaId/5
        [HttpGet("getByProvinciaId/{provinciaId}")]
        public async Task<IActionResult> GetByProvinciaId([FromRoute] long provinciaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comunas = await _servComuna.GetByProvinciaId(provinciaId);

            if (comunas == null)
            {
                return NotFound();
            }

            return Ok(comunas);
        }

        [HttpGet("byEdificioId/{edificioId}")]
        public async Task<IActionResult> ByEdificioId([FromRoute] long edificioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<ComunaModel> comunas = await _servComuna.GetByEdificioId(edificioId);

            if (comunas == null)
            {
                return NotFound();
            }

            return Ok(comunas);
        }
    }
}