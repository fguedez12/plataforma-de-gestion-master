using api_gestiona.DTOs.Agua;
using api_gestiona.DTOs.Artefactos;
using api_gestiona.DTOs.Files;
using api_gestiona.DTOs.Pagination;
using api_gestiona.Entities;
using api_gestiona.Helpers;
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
    public class ArtefactosController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArtefactosController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ArtefactoResponse> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Artefactos.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active).AsQueryable();
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).Include(x => x.TipoArtefacto).ToListAsync();
            var dtos = _mapper.Map<List<ArtefactoDTO>>(entities);
            var response = new ArtefactoResponse { Ok = true, Artefactos = dtos };
            return response;
        }

        [HttpGet("get-tipos")]
        public async Task<ArtefactoResponse> GetTipos()
        {
            var entities = await _context.TipoArtefactos.OrderBy(x=>x.Orden).ToListAsync();
            var dtos = _mapper.Map<List<TipoArtefactosDTO>>(entities);
            var response = new ArtefactoResponse { Ok = true, TipoArtefactos = dtos };
            return response;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ArtefactoResponse> GetById([FromRoute] long id)
        {
            var entity = await _context.Artefactos.Where(x => x.Id == id && x.Active).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new ArtefactoResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var dto = _mapper.Map<ArtefactoDTO>(entity);
            var response = new ArtefactoResponse { Ok = true, Artefacto = dto };
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ArtefactoResponse>> Post([FromForm] ArtefactoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ArtefactoResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Artefacto>(model);
            SetAuditableSave(entity, userId);
            _context.Artefactos.Add(entity);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return new ArtefactoResponse { Ok = true };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtefactoResponse>> Put([FromRoute] long id, [FromForm] ArtefactoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ArtefactoResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var entityDb = await _context.Artefactos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ArtefactoResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }


            var userId = User.Claims.First(i => i.Type == "userId").Value;
            model.Id = id;
            var entity = _mapper.Map<Artefacto>(model);
            SetAuditableUpdate(entity, userId, entityDb);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ArtefactoResponse { Ok = true };

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ArtefactoResponse>> Delete([FromRoute] long id)
        {
            var entityDb = await _context.Artefactos.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ArtefactoResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            SetAuditableDelete(entityDb, userId);
            _context.Update(entityDb);
            await _context.SaveChangesAsync();
            return new ArtefactoResponse { Ok = true };

        }
    }
}
