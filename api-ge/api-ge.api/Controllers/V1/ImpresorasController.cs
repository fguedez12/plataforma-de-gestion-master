using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Impresoras;
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
    public class ImpresorasController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ImpresorasController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ImpresorasResponse> GetImpresoras([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Impresoras.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active).AsQueryable();
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var impresoras = _mapper.Map<List<ImpresoraDTO>>(entities);
            var noDeclara = await _context.Divisiones.Where(x => x.Id == paginationDTO.DivisionId).Select(x => x.NoDeclaraImpresora).FirstOrDefaultAsync();
            var response = new ImpresorasResponse { Ok = true, Impresoras = impresoras, NoDeclaraImpresora = noDeclara };
            return response;
        }

        [HttpPut("no-declara/{id}")]
        public async Task<ActionResult> PutNoDeclara([FromRoute] int id, [FromQuery] bool value)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { return BadRequest("El recurso solicitado no existe"); }
            entity.NoDeclaraImpresora = value;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-impresora-by-id/{id}")]
        public async Task<ImpresorasResponse> GetImpresoraById([FromRoute] long id)
        {
            var entity = await _context.Impresoras.Where(x => x.Id == id && x.Active).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new ImpresorasResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var impresora = _mapper.Map<ImpresoraDTO>(entity);
            var response = new ImpresorasResponse { Ok = true, Impresora = impresora };
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ImpresorasResponse>> Post(ImpresoraDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ImpresorasResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Impresora>(model);
            SetAuditableSave(entity, userId);
            _context.Impresoras.Add(entity);
            await _context.SaveChangesAsync();
            return new ImpresorasResponse { Ok = true };

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ImpresorasResponse>> Put([FromRoute] long id, ImpresoraDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ImpresorasResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var entityDb = await _context.Impresoras.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ImpresorasResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Impresora>(model);
            SetAuditableUpdate(entity, userId, entityDb);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ImpresorasResponse { Ok = true };

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ImpresorasResponse>> Delete([FromRoute] long id)
        {
            var entityDb = await _context.Impresoras.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ImpresorasResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            SetAuditableDelete(entityDb, userId);
            _context.Update(entityDb);
            await _context.SaveChangesAsync();
            return new ImpresorasResponse { Ok = true };

        }
    }
}
