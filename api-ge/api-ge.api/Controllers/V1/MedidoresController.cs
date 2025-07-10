using api_gestiona.DTOs.Medidor;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedidoresController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MedidoresController(ApplicationDbContext context, IConfiguration configuration, IMapper mapper) : base(null, null, null, configuration)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if (!IsAppVaidate(Request))
            {
                return Unauthorized("El usuario no tiene permisos para acceder al recurso");
            }

            var entities = await _context.Medidores.ToListAsync().ConfigureAwait(false);
            var response = _mapper.Map<List<MedidorDTO>>(entities);

            return Ok(response);
        }
    }
}
