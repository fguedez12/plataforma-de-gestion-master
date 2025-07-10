using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.TipoUsoModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Core.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsoController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITipoUsoService _service;
        private readonly ITipoUsoRepository _repository;
        private readonly ILogger _logger;

        public TipoUsoController(
            ApplicationDbContext context,
            ITipoUsoService service,
            ITipoUsoRepository repository,
            ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<TipoUsoController>();
            _service = service;
            _repository = repository;
            _context = context;
        }

        // GET: api/tipouso
        [HttpGet]
        public async Task<IActionResult> GetTipoUsoModel()
        {
            return Ok(_service.All());
        }

        // GET: api/tipouso/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoUsoModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoUsoModel = _service.Get(id);

            if (tipoUsoModel == null)
            {
                return NotFound();
            }

            return Ok(tipoUsoModel);
        }

        // PUT: api/tipouso/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoUsoModel([FromRoute] long id, [FromBody] TipoUsoModel tipoUsoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoUsoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoUsoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUsoModelExists(id))
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

        // POST: api/tipouso
        [HttpPost]
        public async Task<IActionResult> PostTipoUsoModel([FromBody] TipoUsoModel tipoUsoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Insert(tipoUsoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoUsoModel", new { id = tipoUsoModel.Id }, tipoUsoModel);
        }

        // DELETE: api/tipouso/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoUsoModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoUsoModel = _service.Get(id);
            if (tipoUsoModel == null)
            {
                return NotFound();
            }

            _service.Delete(tipoUsoModel.Id);
            await _context.SaveChangesAsync();

            return Ok(tipoUsoModel);
        }

        private bool TipoUsoModelExists(long id)
        {
            return (_service.Get(id) == null) ? false : true;
        }
    }
}