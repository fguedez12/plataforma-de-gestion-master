using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUnidadMedidasController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly ApplicationDbContext _context;

        public ApiUnidadMedidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnidadMedidas
        [HttpGet]
        public IEnumerable<UnidadMedida> GetUnidadesMedidas()
        {
            return _context.UnidadesMedida;
        }

        // GET: api/UnidadMedidas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnidadMedida([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);

            if (unidadMedida == null)
            {
                return NotFound();
            }

            return Ok(unidadMedida);
        }

        // PUT: api/UnidadMedidas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadMedida([FromRoute] long id, [FromBody] UnidadMedida unidadMedida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unidadMedida.Id)
            {
                return BadRequest();
            }

            _context.Entry(unidadMedida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadMedidaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UnidadMedidas
        [HttpPost]
        public async Task<IActionResult> PostUnidadMedida([FromBody] UnidadMedida unidadMedida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UnidadesMedida.Add(unidadMedida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnidadMedida", new { id = unidadMedida.Id }, unidadMedida);
        }

        // DELETE: api/UnidadMedidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidadMedida([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida == null)
            {
                return NotFound();
            }

            _context.UnidadesMedida.Remove(unidadMedida);
            await _context.SaveChangesAsync();

            return Ok(unidadMedida);
        }

        private bool UnidadMedidaExists(long id)
        {
            return _context.UnidadesMedida.Any(e => e.Id == id);
        }
    }
}