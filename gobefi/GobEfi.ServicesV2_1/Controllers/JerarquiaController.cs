using GobEfi.ServicesV2_1.Models;
using GobEfi.Web.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.ServicesV2_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JerarquiaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public JerarquiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("inmuebles")]
        public async Task<ActionResult> GetInstituciones()
        {
            var result = await _context.Divisiones.Where(x => x.GeVersion == 3).ToListAsync();

            return Ok(result);
        }

        [HttpGet("direcciones")]
        public async Task<ActionResult> GetDirecciones()
        {
            var result = await _context.Direcciones.ToListAsync();

            return Ok(result);
        }

        [HttpGet("pisos")]
        public async Task<ActionResult> GetPisos()
        {
            var result = await _context.Pisos.Where(x => x.EdificioId == null).Include(p=>p.NumeroPiso).ToListAsync();

            return Ok(result);
        }

        [HttpGet("areas")]
        public async Task<ActionResult> GetAreas()
        {
            var result = await _context.Areas.ToListAsync();

            return Ok(result);
        }

        [HttpGet("unidades")]
        public async Task<ActionResult> GetUnidades()
        {
            var result = await _context.Unidades.ToListAsync();

            return Ok(result);
        }

        [HttpGet("unidadesInmuebles")]
        public async Task<ActionResult> GetUnidadesInmuebles()
        {
            var result = await _context.UnidadesInmuebles.ToListAsync();

            return Ok(result);
        }
        [HttpGet("unidadesPisos")]
        public async Task<ActionResult> GetUnidadesPisos()
        {
            var result = await _context.UnidadesPisos.ToListAsync();

            return Ok(result);
        }
        [HttpGet("unidadesAreas")]
        public async Task<ActionResult> GetUnidadesAreas()
        {
            var result = await _context.UnidadesAreas.ToListAsync();

            return Ok(result);
        }
        [HttpGet("usuariosUnidades")]
        public async Task<ActionResult> GetUnidadesUsuarios() 
        {
            var result = await _context.UsuariosUnidades.ToListAsync();
            return Ok(result);
        }
    }
}
