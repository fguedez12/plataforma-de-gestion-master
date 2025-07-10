using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Helper;
using GobEfi.Web.Models.EdificioModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdificioController : ControllerBase
    {
        private readonly IEdificioService _servEdificio;

        public EdificioController(IEdificioService servEdificio)
        {
            _servEdificio = servEdificio;
        }

        // GET: api/Edificio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EdificioModel>> GetEdificio(long id)
        {
            var edificioItem = _servEdificio.Get(id);

            if (edificioItem == null)
            {
                return NotFound();
            }

            return edificioItem;
        }

        [HttpGet("getForSelect/{edificioId}")]
        public async Task<ActionResult<EdificioSelectModel>> GetEdificioForSelect(long edificioId)
        {
            EdificioSelectModel edificioItem = await _servEdificio.GetForSelect(edificioId);

            if (edificioItem == null)
            {
                return NotFound();
            }

            return edificioItem;
        }

        // POST: api/Edificio
        [HttpPost]
        public async Task<ActionResult<EdificioModel>> PostEdificio(EdificioCreateModel item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var newId = _servEdificio.Insert(item);
            item.Id = newId;

            return CreatedAtAction(nameof(GetEdificio), new { id = newId }, item);
        }

        // GET: api/Edificio/getByComunaId/5
        [HttpGet("getByComunaId/{comunaId}")]
        public async Task<IActionResult> GetByComunaId([FromRoute] long comunaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var edificios = await _servEdificio.getByComunaId(comunaId);

            if (edificios == null)
            {
                return NotFound();
            }

            return Ok(edificios);
        }

        [HttpGet("getByRegionId/{regionId}")]
        public async Task<IActionResult> GetByRegionId([FromRoute] long regionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<EdificioSelectModel> edificios = await _servEdificio.getByRegionId(regionId);

            if (edificios == null)
            {
                return NotFound();
            }

            return Ok(edificios);
        }

    }
}