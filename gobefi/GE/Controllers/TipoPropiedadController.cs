using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.TipoPropiedadModels;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TipoPropiedadController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITipoPropiedadRepository _repository;
        private readonly ITipoPropiedadService _service;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TipoPropiedadController(
            ApplicationDbContext context,
            ITipoPropiedadRepository repository,
            ITipoPropiedadService service,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _service = service;
            _logger = loggerFactory.CreateLogger<TipoPropiedadController>();
            _mapper = mapper;
        }

        // GET: api/tipopropiedad
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTipoPropiedadModel()
        {
            var tipos = _service.All();
            return Ok(tipos);
        }

        // GET: api/tipopropiedad/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoPropiedadModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoPropiedadModel = _service.Get(id);

            if (tipoPropiedadModel == null)
            {
                return NotFound();
            }

            return Ok(tipoPropiedadModel);
        }

        // PUT: api/tipopropiedad/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPropiedadModel([FromRoute] long id, [FromBody] TipoPropiedadModel tipoPropiedadModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoPropiedadModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoPropiedadModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPropiedadModelExists(id))
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

        // POST: api/tipopropiedad
        [HttpPost]
        public async Task<IActionResult> PostTipoPropiedadModel([FromBody] TipoPropiedadModel tipoPropiedadModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _service.Insert(tipoPropiedadModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoPropiedadModel", new { id = tipoPropiedadModel.Id }, tipoPropiedadModel);
        }

        // DELETE: api/tipopropiedad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPropiedadModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoPropiedadModel = _service.Get(id);
            if (tipoPropiedadModel == null)
            {
                return NotFound();
            }

            _service.Delete(tipoPropiedadModel.Id);
            await _context.SaveChangesAsync();

            return Ok(tipoPropiedadModel);
        }

        private bool TipoPropiedadModelExists(long id)
        {
            return (_service.Get(id) == null) ? false : true; ;
        }
    }
}