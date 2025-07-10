using api_gestiona.DTOs.Instituciones;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InstitucionController : CustomController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IInstitucionService _institucionService;

        public InstitucionController(IMapper mapper, ApplicationDbContext context, UserManager<Usuario> userManager, IInstitucionService institucionService) : base(mapper, context, userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _institucionService = institucionService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<InstitucionListDTO>>> GetListAll()
        {
            var institucionesFromDb = await _context.Instituciones.Where(x => x.Active).OrderBy(x => x.Nombre).ToListAsync();
            var institucionesList = _mapper.Map<List<InstitucionListDTO>>(institucionesFromDb);
            return Ok(institucionesList);
        }

        [AllowAnonymous]
        [HttpGet("getAsociadosByUserId/{userId}")]
        public async Task<ActionResult<List<InstitucionListDTO>>> GetAsociadosByUserId(string userId)
        {
            var response = await _institucionService.GetByUserId(userId);

            return response;
        }
    }
}
