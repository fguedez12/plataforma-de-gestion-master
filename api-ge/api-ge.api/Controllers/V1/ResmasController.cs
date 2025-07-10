using api_gestiona.DTOs.Impresoras;
using api_gestiona.DTOs.Pagination;
using api_gestiona.DTOs.Resmas;
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
    public class ResmasController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ResmasController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResmasResponse> GetResmas([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Resmas.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active).AsQueryable();
            if (paginationDTO.AnioDoc != null)
            {
                queryable = queryable
                    .Where(x => (x.FechaAdquisicion.HasValue && x.FechaAdquisicion.Value.Year == paginationDTO.AnioDoc) || (x.AnioAdquisicion.HasValue && x.AnioAdquisicion.Value == paginationDTO.AnioDoc));
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var resmas = _mapper.Map<List<ResmaDTO>>(entities);
            var response = new ResmasResponse { Ok = true, Resmas = resmas };
            return response;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ResmasResponse> GetById([FromRoute] long id)
        {
            var entity = await _context.Resmas.Where(x => x.Id == id && x.Active).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new ResmasResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var dto = _mapper.Map<ResmaDTO>(entity);
            var response = new ResmasResponse { Ok = true, Resma = dto };
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ResmasResponse>> Post(ResmaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ResmasResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Resma>(model);
            SetAuditableSave(entity, userId);
            _context.Resmas.Add(entity);
            await _context.SaveChangesAsync();
            return new ResmasResponse { Ok = true };

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResmasResponse>> Put([FromRoute] long id, ResmaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ResmasResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var entityDb = await _context.Resmas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ResmasResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Resma>(model);
            SetAuditableUpdate(entity, userId, entityDb);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ResmasResponse { Ok = true };

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResmasResponse>> Delete([FromRoute] long id)
        {
            var entityDb = await _context.Resmas.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ResmasResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            SetAuditableDelete(entityDb, userId);
            _context.Update(entityDb);
            await _context.SaveChangesAsync();
            return new ResmasResponse { Ok = true };

        }
    }
}
