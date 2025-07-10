using api_gestiona.DTOs.Ajustes;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("bloqueo-info-servicio")]
        public async Task<ActionResult> GetBloqInfoServicio()
        {

            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = userIdClaim.Value;
            var servicios = await _context.UsuariosServicios
                                          .Include(x => x.Servicio)
                                          .Where(x => x.UsuarioId == userId)
                                          .Select(x => x.Servicio)
                                          .ToListAsync();

            var resp = new List<ServiciosABloquearDTO>();

            foreach (var item in servicios)
            {
                resp.Add(new ServiciosABloquearDTO
                {
                    ServicioId = item.Id,
                    ServicioNombre = item.Nombre!,
                });
            }
            return Ok(resp);
        }

        [HttpPost("editUnidadPMG")]
        public async Task<ActionResult> EditUnidadPMG([FromBody] bool value)
        {
            var ajusteFromDb = await _context.Ajustes.FirstOrDefaultAsync();
            ajusteFromDb.EditUnidadPMG = value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("activeAlcanceModulo")]
        public async Task<ActionResult> EditactiveAlcanceModulo([FromBody] bool value)
        {
            var ajusteFromDb = await _context.Ajustes.FirstOrDefaultAsync();
            ajusteFromDb.ActiveAlcanceModule = value;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("createUnidadPMG")]
        public async Task<ActionResult> CreateUnidadPMG([FromBody] bool value)
        {
            var ajusteFromDb = await _context.Ajustes.FirstOrDefaultAsync();
            ajusteFromDb.CreateUnidadPMG = value;
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

        [HttpPost("bloqueo-info-sevicio")]
        public async Task<ActionResult> BloqueoInfoServicio([FromBody] bool value)
        {

            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var userId = userIdClaim.Value;
            var servicios = await _context.UsuariosServicios
                                          .Include(x => x.Servicio)
                                          .Where(x => x.UsuarioId == userId)
                                          .Select(x => x.Servicio)
                                          .ToListAsync();

            foreach (var item in servicios)
            {
                item.BloqueoIngresoInfo = value;
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
