using api_gestiona.DTOs.RegistrosDTOs;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]

    public class RegistrosController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _manager;

        public RegistrosController(ApplicationDbContext context, IMapper mapper, IConfiguration configuration, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _manager = manager;
        }
        // GET: api/tipouso/list
        [HttpGet("list")]
        public async Task<ActionResult<List<RegistroListDTO>>> GetRegistros()
        {
            var keyFromRequest = Request.Headers["key"].ToString();
            var key = _configuration["internalKey:key"].ToString();
            if (key == keyFromRequest)
            {
                return await Get<Registro, RegistroListDTO>();
            }

            return NoContent();

        }
    }
}
