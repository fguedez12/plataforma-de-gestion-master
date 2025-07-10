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
    public class RegionesController : CustomController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _manager;

        public RegionesController(IMapper mapper, ApplicationDbContext context, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _mapper = mapper;
            _context = context;
            _manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult<List<RegionDTO>>> Get()
        {
            var list = await _context.Regiones.OrderBy(x => x.Posicion).ToListAsync();
            return _mapper.Map<List<RegionDTO>>(list);
        }
    }
}
