using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Pagination;
using api_gestiona.DTOs.Servicios;
using api_gestiona.Entities;
using api_gestiona.Helpers;
using api_gestiona.Services;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServiciosController : CustomController
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IServicioService _servicioService;
        private readonly IConfiguration _configuration;

        public ServiciosController(IMapper mapper, ApplicationDbContext context, UserManager<Usuario> userManager, IServicioService servicioService, IConfiguration configuration) : base(mapper, context, userManager, configuration)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _servicioService = servicioService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet("getByInstitucionIdAndUserId/{institucionId}/{userId}")]
        public async Task<ActionResult<List<ServicioDTO>>> GetByInstitucionIdAndUserId(long institucionId, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            bool isAdmin = await _userManager.IsInRoleAsync(user, "ADMINISTRADOR");


            var query = _context.Servicios.Where(x => x.Active).AsQueryable();

            var serviciosFromDb = await query
                            .Where(c => c.InstitucionId == institucionId)
                            .Include(c => c.UsuariosServicios)
                            .Where(s => isAdmin ? true : s.UsuariosServicios.Any(us => us.UsuarioId == user.Id))
                            .OrderBy(x => x.Nombre)
                            .ToListAsync();



            var servicios = _mapper.Map<List<ServicioDTO>>(serviciosFromDb);

            return servicios;
        }

        [HttpGet("getlist-by-institucionid/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ServicioListDTO>>> GetListByInstitucionId([FromRoute] long id)
        {
            if (!IsAppVaidate(Request))
            {
                return BadRequest("La aplicación no es valida");
            }

            var serviciosFromDb = await _context.Servicios.Where(x => x.Active).Where(x => x.InstitucionId == id).OrderBy(x => x.Nombre).ToListAsync();
            var serviciosList = _mapper.Map<List<ServicioListDTO>>(serviciosFromDb);

            return Ok(serviciosList);

        }


        [HttpGet("getByUserId/{userId}")]
        public async Task<ServicioResponse> GetByUserId(string userId)
        {
            var response = await _servicioService.GetByUserId(userId);
            return response;
        }

        [HttpGet("getByUserIdPagin")]
        public async Task<ServicioResponse> GetByUserIdpagin([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "ADMINISTRADOR");
            var queryable = isAdmin ?
                 _context.Servicios.Where(x => x.Active).OrderBy(x => x.Nombre).AsQueryable() :
                 _context.UsuariosServicios.Where(x => x.UsuarioId == userId).Include(x => x.Servicio).Select(x => x.Servicio).Where(x => x.Active).OrderBy(x => x.Nombre).AsQueryable();

            if (paginationDTO.InstitucionId != null)
            {
                queryable = queryable.Where(x => x.InstitucionId == paginationDTO.InstitucionId);
            }

            if (paginationDTO.Pmg)
            {
                queryable = queryable.Where(x => x.ReportaPMG);
            }

            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var response = new ServicioResponse() { Ok = true };
            var servicios = await queryable.Pagin(paginationDTO).ToListAsync();
            response.Servicios = _mapper.Map<List<ServicioDTO>>(servicios);
            return response;
        }

        [HttpPost("set-justificacion")]
        public async Task<ActionResult> SetJustificacion(ServicioDTO model)
        {
            await _servicioService.SaveJustificacion(model);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(long id, JsonPatchDocument<ServicioPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);

            if (servicio == null)
            {
                return NotFound();
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;

            var servicioDTO = _mapper.Map<ServicioPatchDTO>(servicio);

            servicioDTO.UpdatedAt = DateTime.Now;
            servicioDTO.ModifiedBy = userId;
            servicioDTO.Version = servicio.Version + 1;

            patchDocument.ApplyTo(servicioDTO, ModelState);

            var isValid = TryValidateModel(servicioDTO);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(servicioDTO, servicio);
            await _context.SaveChangesAsync();
            return NoContent();

        }
        [HttpGet("get-diagnostico/{id}")]
        public async Task<ActionResult> GetDiagnostico([FromRoute] long id)
        {
            var result = await _servicioService.GetDiagnostico(id);
            return Ok(result);
        }

    }
}
