using AutoMapper;
using GobEfi.Web.Data;
using GobEfi.Web.DTOs.AjustesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AjustesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AjustesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AjustesDTO>> Get()
        {
            var ajusteFromDB = await _context.Ajustes.FirstOrDefaultAsync();
            return _mapper.Map<AjustesDTO>(ajusteFromDB);

        }

        [HttpPost("editUnidadPMG")]
        public async Task<ActionResult> EditUnidadPMG([FromBody] bool value)
        {
            var ajusteFromDb = await _context.Ajustes.FirstOrDefaultAsync();
            ajusteFromDb.EditUnidadPMG = value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("deleteUnidadPMG")]
        public async Task<ActionResult> DeleteUnidadPMG([FromBody] bool value)
        {
            var ajusteFromDb = await _context.Ajustes.FirstOrDefaultAsync();
            ajusteFromDb.DeleteUnidadPMG = value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("comprasServicio")]
        public async Task<ActionResult> ComprasServicio([FromBody] bool value)
        {
            var ajusteFromDb = await _context.Ajustes.FirstOrDefaultAsync();
            ajusteFromDb.ComprasServicio = value;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
