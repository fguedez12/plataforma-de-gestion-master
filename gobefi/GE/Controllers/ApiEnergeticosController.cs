using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;

namespace Web.Areas.Consumos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiEnergeticosController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly ApplicationDbContext _context;

        public ApiEnergeticosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Energeticos
        [HttpGet]
        public IEnumerable<Energetico> GetEnergeticos()
        {
            return _context.Energeticos;
        }

        // GET: api/Energeticos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnergetico([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var energetico = await _context.Energeticos.FindAsync(id);

            if (energetico == null)
            {
                return NotFound();
            }

            return Ok(energetico);
        }

        // PUT: api/Energeticos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnergetico([FromRoute] long id, [FromBody] Energetico energetico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != energetico.Id)
            {
                return BadRequest();
            }

            _context.Entry(energetico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnergeticoExists(id))
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

        // POST: api/Energeticos
        [HttpPost]
        public async Task<IActionResult> PostEnergetico([FromBody] Energetico energetico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Energeticos.Add(energetico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnergetico", new { id = energetico.Id }, energetico);
        }

        // DELETE: api/Energeticos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnergetico([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var energetico = await _context.Energeticos.FindAsync(id);
            if (energetico == null)
            {
                return NotFound();
            }

            _context.Energeticos.Remove(energetico);
            await _context.SaveChangesAsync();

            return Ok(energetico);
        }

        private bool EnergeticoExists(long id)
        {
            return _context.Energeticos.Any(e => e.Id == id);
        }
    }
}