using api_gestiona.DTOs.Agua;
using api_gestiona.DTOs.Files;
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
    public class ResiduosController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO = 1005;
        private readonly int TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO_SISTEMA = 1006;

        public ResiduosController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ResiduosResponse> GetResiduos([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Residuos.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active && x.TipoResiduo!= "Residuos NO reciclados").AsQueryable();
            if (paginationDTO.AnioDoc != null)
            {
                queryable = queryable
                    .Where(x => (x.Fecha.HasValue && x.Fecha.Value.Year == paginationDTO.AnioDoc) || (x.Anio.HasValue && x.Anio.Value == paginationDTO.AnioDoc));
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var dtos = _mapper.Map<List<ResiduoDTO>>(entities);
            var noDeclara = await _context.Divisiones.Where(x => x.Id == paginationDTO.DivisionId).Select(x => x.NoDeclaraContenedores).FirstOrDefaultAsync();
            var response = new ResiduosResponse { Ok = true, Residuos = dtos, NoDeclaraContenedores = noDeclara };
            return response;
        }

        [HttpGet("no-reciclados")]
        public async Task<ResiduosResponse> GetResiduosNoReciclados([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Residuos.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active && x.TipoResiduo == "Residuos NO reciclados").AsQueryable();
            if (paginationDTO.AnioDoc != null)
            {
                queryable = queryable
                    .Where(x => (x.Fecha.HasValue && x.Fecha.Value.Year == paginationDTO.AnioDoc) || (x.Anio.HasValue && x.Anio.Value == paginationDTO.AnioDoc));
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var dtos = _mapper.Map<List<ResiduoDTO>>(entities);
            var noDeclara = await _context.Divisiones.Where(x => x.Id == paginationDTO.DivisionId).Select(x => x.NoDeclaraContenedores).FirstOrDefaultAsync();
            var response = new ResiduosResponse { Ok = true, Residuos = dtos, NoDeclaraContenedores = noDeclara };
            return response;
        }

        [HttpPut("no-declara/{id}")]
        public async Task<ActionResult> PutNoDeclara([FromRoute] int id, [FromQuery] bool value)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { return BadRequest("El recurso solicitado no existe"); }
            entity.NoDeclaraContenedores = value;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("procedimientos-residuos/{divisionId}")]
        public async Task<List<ProcedimientoListaDTO>> GetProcedimientos([FromRoute] long divisionId)
        {
            var servicioId = await _context.Divisiones.Where(x => x.Id == divisionId).Select(x => x.ServicioId).FirstOrDefaultAsync();
            var entities = await _context.Documentos
                .Where(x => x.ServicioId == servicioId && (x.TipoDocumentoId == TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO || x.TipoDocumentoId == TIPO_DOCUMENTO_PROCEDIMIENTO_RESIDUO_SISTEMA))
                .ToListAsync();
            var response = _mapper.Map<List<ProcedimientoListaDTO>>(entities);
            return response;

        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ResiduosResponse> GetById([FromRoute] long id)
        {
            var entity = await _context.Residuos.Where(x => x.Id == id && x.Active).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new ResiduosResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var dto = _mapper.Map<ResiduoDTO>(entity);
            var response = new ResiduosResponse { Ok = true, Residuo = dto };
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ResiduosResponse>> Post(ResiduoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ResiduosResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Residuo>(model);
            SetAuditableSave(entity, userId);
            _context.Residuos.Add(entity);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return new ResiduosResponse { Ok = true };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResiduosResponse>> Put([FromRoute] long id, ResiduoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ResiduosResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var entityDb = await _context.Residuos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ResiduosResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            model.Id = id;
            var entity = _mapper.Map<Residuo>(model);
            SetAuditableUpdate(entity, userId, entityDb);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ResiduosResponse { Ok = true };

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResiduosResponse>> Delete([FromRoute] long id)
        {
            var entityDb = await _context.Residuos.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new ResiduosResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            SetAuditableDelete(entityDb, userId);
            _context.Update(entityDb);
            await _context.SaveChangesAsync();
            return new ResiduosResponse { Ok = true };
        }
    }
}
