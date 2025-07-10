using api_gestiona.DTOs;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ComunasController : CustomController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _manager;

        public ComunasController(IMapper mapper, ApplicationDbContext context, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _mapper = mapper;
            _context = context;
            _manager = manager;
        }

        [HttpGet("byRegionId/{id:long}")]
        public async Task<ActionResult<List<ComunaDTO>>> Get([FromRoute] long id)
        {
            var region = await _context.Regiones.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return NotFound("La region seleccionada no existe");
            }
            var list = await _context.Comunas.Where(x => x.RegionId == id).OrderBy(c => c.Nombre).ToListAsync();
            return _mapper.Map<List<ComunaDTO>>(list);
        }
    }
}
