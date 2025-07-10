using api_gestiona.DTOs.Agua;
using api_gestiona.DTOs.Documentos;
using api_gestiona.DTOs.Files;
using api_gestiona.DTOs.Impresoras;
using api_gestiona.DTOs.Pagination;
using api_gestiona.Entities;
using api_gestiona.Helpers;
using api_gestiona.Services;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AguasController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public AguasController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager, IFileService fileService) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<ActionResult<AguaResponse>> GetConsumos([FromQuery] PaginationDTO paginationDTO)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var queryable = _context.Aguas.Where(x => x.DivisionId == paginationDTO.DivisionId && x.Active).AsQueryable();
            if (paginationDTO.AnioDoc != null)
            {
                queryable = queryable
                    .Where(x => (x.FinLectura.HasValue && x.FinLectura.Value.Year == paginationDTO.AnioDoc) || (x.Fecha.HasValue && x.Fecha.Value.Year == paginationDTO.AnioDoc) || (x.AnioAdquisicion.HasValue && x.AnioAdquisicion.Value == paginationDTO.AnioDoc));
            }
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var dtos = _mapper.Map<List<AguaDTO>>(entities);
            var unidad = await _context.Divisiones.Where(x => x.Id == paginationDTO.DivisionId).FirstOrDefaultAsync();
            if (unidad == null)
            {
                return BadRequest("No se encuentra el recurso solicitado");
            }
            var noDeclara = unidad.NoDeclaraArtefactos;
            var usaBidon = unidad.UsaBidon;
            var response = new AguaResponse 
            { 
                Ok = true, 
                Consumos = dtos, 
                NoDeclaraArtefactos = noDeclara, 
                UsaBidon = usaBidon, 
                AccesoFacturaAgua = unidad.AccesoFacturaAgua,
                ComparteMedidorAgua = unidad.ComparteMedidorAgua
            };
            return response;
        }

        [HttpPut("no-declara/{id}")]
        public async Task<ActionResult> PutNoDeclara([FromRoute] int id, [FromQuery] bool value)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { return BadRequest("El recurso solicitado no existe"); }
            entity.NoDeclaraArtefactos = value;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("get-by-id/{id}")]
        public async Task<AguaResponse> GetById([FromRoute] long id)
        {
            var entity = await _context.Aguas.Where(x => x.Id == id && x.Active).FirstOrDefaultAsync();

            if (entity == null)
            {
                return new AguaResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var dto = _mapper.Map<AguaDTO>(entity);
            var response = new AguaResponse { Ok = true, Consumo = dto };
            return response;
        }

        [HttpPost]
        public async Task<ActionResult<AguaResponse>> PostAgua([FromForm] AguaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new AguaResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            //if (Request.Form.Files.Count == 0)
            //{
            //    return new AguaResponse { Ok = false, Msj = "No se encontró archivo adjunto" };
            //}

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            //var file = Request.Form.Files[0];
            //var fileDto = _fileService.SaveFile(file);
            var entity = _mapper.Map<Agua>(model);
            SetAuditableSave(entity, userId);
            //entity.AdjuntoUrl = fileDto.Nombre;
            //entity.AdjuntoNombre = fileDto.NombreOriginal;
            _context.Aguas.Add(entity);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return new AguaResponse { Ok = true };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AguaResponse>> Put([FromRoute] long id, [FromForm] AguaDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new AguaResponse { Ok = false, Msj = "Los datos ingresados no son válidos" };
            }

            var entityDb = await _context.Aguas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new AguaResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            IFormFile file;
            FileDTO fileDto = new FileDTO();
            if (Request.Form.Files.Count > 0)
            {
                await _fileService.DeleteFile(entityDb.AdjuntoUrl);
                file = Request.Form.Files[0];
                fileDto = _fileService.SaveFile(file);
            }

            if (!string.IsNullOrEmpty(fileDto.Nombre))
            {
                model.AdjuntoUrl = fileDto.Nombre;
                model.AdjuntoNombre = fileDto.NombreOriginal;
            }
            else
            {
                model.AdjuntoUrl = entityDb.AdjuntoUrl;
                model.AdjuntoNombre = entityDb.AdjuntoNombre;
            }



            var userId = User.Claims.First(i => i.Type == "userId").Value;
            model.Id = id;
            var entity = _mapper.Map<Agua>(model);
            SetAuditableUpdate(entity, userId, entityDb);
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new AguaResponse { Ok = true };

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AguaResponse>> Delete([FromRoute] long id)
        {
            var entityDb = await _context.Aguas.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
            {
                return new AguaResponse { Ok = false, Msj = "El recurso solicitado no se encuentra" };
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            SetAuditableDelete(entityDb, userId);
            _context.Update(entityDb);
            await _context.SaveChangesAsync();
            return new AguaResponse { Ok = true };

        }
    }
}
