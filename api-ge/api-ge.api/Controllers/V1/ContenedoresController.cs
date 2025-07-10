using api_gestiona.DTOs.Contenedores;
using api_gestiona.DTOs.Pagination;
using api_gestiona.DTOs.Residuos;
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
    public class ContenedoresController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContenedoresController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ContenedoresResponse> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Contenedores.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active).AsQueryable();
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var dtos = _mapper.Map<List<ContenedorDTO>>(entities);
            var response = new ContenedoresResponse { Ok = true, Contenedores = dtos };
            return response;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ContenedoresResponse> GetById([FromRoute] long id)
        {
            var entity = await _context.Contenedores.Where(x => x.Id == id && x.Active).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new ContenedoresResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var dto = _mapper.Map<ContenedorDTO>(entity);
            var response = new ContenedoresResponse { Ok = true, Contenedor = dto };
            return response;
        }


        [HttpPost]
        public async Task<ActionResult<ContenedoresResponse>> Post(ContenedorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ContenedoresResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Contenedor>(model);
            SetAuditableSave(entity, userId);
            _context.Contenedores.Add(entity);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return new ContenedoresResponse { Ok = true };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContenedoresResponse>> Put([FromRoute] long id, ContenedorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ContenedoresResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var entityDb = await _context.Contenedores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ContenedoresResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            model.Id = id;
            var entity = _mapper.Map<Contenedor>(model);
            SetAuditableUpdate(entity, userId, entityDb);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ContenedoresResponse { Ok = true };
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ContenedoresResponse>> Delete([FromRoute] long id)
        {
            var entityDb = await _context.Contenedores.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ContenedoresResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            SetAuditableDelete(entityDb, userId);
            _context.Update(entityDb);
            await _context.SaveChangesAsync();
            return new ContenedoresResponse { Ok = true };
        }
    }
}
