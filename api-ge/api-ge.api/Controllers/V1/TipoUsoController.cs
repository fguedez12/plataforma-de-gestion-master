using api_gestiona.DTOs.TipoUsoListDTO;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoUsoController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _manager;

        public TipoUsoController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _manager = manager;
        }
        // GET: api/tipouso/list
        [HttpGet("list")]
        public async Task<ActionResult<List<TipoUsoListDTO>>> GetTipoUsoList()
        {
            return await Get<TipoUso, TipoUsoListDTO>();
        }
    }
}
