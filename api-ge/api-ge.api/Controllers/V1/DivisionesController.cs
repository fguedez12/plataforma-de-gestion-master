using api_gestiona.DTOs;
using api_gestiona.DTOs.Divisiones;
using api_gestiona.DTOs.Instituciones;
using api_gestiona.DTOs.Servicios;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DivisionesController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDivisionService _divisionService;
        private readonly UserManager<Usuario> _manager;
        private readonly IFlotaVehicularService _flotaVehicularService;

        public DivisionesController(ApplicationDbContext context, IMapper mapper, IDivisionService divisionService, UserManager<Usuario> manager, IFlotaVehicularService flotaVehicularService) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
            _divisionService = divisionService;
            _manager = manager;
            _flotaVehicularService = flotaVehicularService;
        }

        [HttpGet("{id}")]
        public async Task<DivisionesResponse> GetById([FromRoute] long id)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = await _context.Divisiones.Where(x => x.Id == id).FirstOrDefaultAsync();
            var dtoDivision = _mapper.Map<DivisionDTO>(entity);
            return new DivisionesResponse { Ok = true, Division = dtoDivision };
        }


        [HttpGet("by-user-id")]
        public async Task<DivisionesResponse> GetByUserId()
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            await setUser(userId);
            var result = await _divisionService.GetByUserId(_usuario.Id, _isAdmin);
            return result;
        }

        [HttpGet("observacion-papel/{divisionId}")]
        public async Task<ActionResult> JustificaCionPapel([FromRoute] long divisionId)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            return Ok(new ObservacionDTO { CheckObserva = entity.ObservaPapel, Observacion = entity.ObservacionPapel }); ;
        }

        [HttpGet("observacion-residuos/{divisionId}")]
        public async Task<ActionResult> JustificaResiduos([FromRoute] long divisionId)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            return Ok(new ObservacionDTO { CheckObserva = entity.ObservaResiduos, Observacion = entity.ObservacionResiduos }); ;
        }

        [HttpGet("reporta-residuos/{divisionId}")]
        public async Task<ActionResult> ReportaResiduos([FromRoute] long divisionId)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            return Ok(new ReportaResiduosDTO 
            { 
                CheckReporta = entity.JustificaResiduos, 
                Justificacion = entity.JustificacionResiduos,
                CheckReportaNoReciclados = entity.JustificaResiduosNoReciclados,
                JustificacionNoReciclados = entity.JustificacionResiduosNoReciclados
            }); 
        }


        [HttpGet("observacion-agua/{divisionId}")]
        public async Task<ActionResult> JustificaAgua([FromRoute] long divisionId)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            return Ok(new ObservacionDTO { CheckObserva = entity.ObservaAgua, Observacion = entity.ObservacionAgua }); ;
        }

        [HttpPut("observacion-papel/{divisionId}")]
        public async Task<ActionResult> ObservacionPapel([FromRoute] long divisionId, ObservacionDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            entity.ObservaPapel = model.CheckObserva;
            entity.ObservacionPapel = model.Observacion;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        [HttpPut("observa-residuos/{divisionId}")]
        public async Task<ActionResult> ObservaResiduos([FromRoute] long divisionId, ObservacionDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            entity.ObservaResiduos = model.CheckObserva;
            entity.ObservacionResiduos = model.Observacion;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        [HttpPut("reporta-residuos/{divisionId}")]
        public async Task<ActionResult> ReportaResiduos([FromRoute] long divisionId, ReportaResiduosDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            entity.JustificaResiduos = model.CheckReporta;
            entity.JustificacionResiduos = model.Justificacion;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        [HttpPut("reporta-residuos-no-reciclados/{divisionId}")]
        public async Task<ActionResult> ReportaResiduosNoReciclados([FromRoute] long divisionId, ReportaResiduosDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            entity.JustificaResiduosNoReciclados = model.CheckReportaNoReciclados;
            entity.JustificacionResiduosNoReciclados = model.JustificacionNoReciclados;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        [HttpPut("justifica-agua/{divisionId}")]
        public async Task<ActionResult> JustificaAgua([FromRoute] long divisionId, ObservacionDTO justificacion)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            entity.ObservaAgua = justificacion.CheckObserva;
            entity.ObservacionAgua = justificacion.Observacion;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        [HttpGet("inexistencia-eyv/{divisionId}")]
        public async Task<ActionResult> GetInexistenciaEyV([FromRoute] long divisionId)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            return Ok(new { Observacion = entity.ObsInexistenciaEyV });
        }

        [HttpPut("inexistencia-eyv/{divisionId}")]
        public async Task<ActionResult> UpdateInexistenciaEyV([FromRoute] long divisionId, [FromBody] ObservacionInexistenciaDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId);
            if (entity == null)
            {
                return BadRequest("No existe el recurso solicitado");
            }

            entity.ObsInexistenciaEyV = model.Observacion;
            _context.Divisiones.Update(entity);
            await _context.SaveChangesAsync();

            return Ok("ok");
        }

        [HttpGet("by-servicio-id")]
        public async Task<DivisionesResponse> GetByServicioId([FromQuery] long servicioId, [FromQuery] string? searchText)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (!await userInService(userId, servicioId))
            {
                return new DivisionesResponse { Ok = false, Msj = "El usuario no corresponde al servicio" };
            }
            var result = await _divisionService.GetByServicioId(servicioId, searchText);
            return result;
        }

        [HttpPost("set-inicio-gestion")]
        public async Task<ActionResult> SetInicioGestion(DivisionDTO model)
        {
            await _divisionService.SetInicioGestion(model);
            return NoContent();
        }

        [HttpPost("set-resto-items")]
        public async Task<ActionResult> SetInicioResto(DivisionDTO model)
        {
            await _divisionService.SetRestoItems(model);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            if (userId == null) return BadRequest();
            var inmuebleDb = await _context.Divisiones.Include(x => x.ListPisos).ThenInclude(p => p.Areas).FirstOrDefaultAsync(x => x.Id == id);
            if (inmuebleDb != null && userId != null)
            {
                inmuebleDb.Active = false;
                inmuebleDb.Version += inmuebleDb.Version;
                inmuebleDb.UpdatedAt = DateTime.Now;
                inmuebleDb.ModifiedBy = userId;

                foreach (var piso in inmuebleDb.ListPisos)
                {
                    piso.Active = false;
                    piso.Version += inmuebleDb.Version;
                    piso.UpdatedAt = DateTime.Now;
                    piso.ModifiedBy = userId;
                    foreach (var area in piso.Areas)
                    {
                        area.Active = false;
                        area.Version += inmuebleDb.Version;
                        area.UpdatedAt = DateTime.Now;
                        area.ModifiedBy = userId;
                    }
                }

                await _context.SaveChangesAsync();


            }
            return NoContent();
        }

        [HttpGet("actualizacion-unidad/{id}")]
        public async Task<ActionResult> GetActualizacionUnidad([FromRoute] long id)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            var response = _mapper.Map<ActualizacionDivisionDTO>(entity);
            var vehiculos = await _flotaVehicularService.GetVehiculosServicioByDivisionId(id);
            response.Vehiculos = vehiculos;
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(long id, JsonPatchDocument<DivisionPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var divison = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);

            if (divison == null)
            {
                return NotFound();
            }

            var divisonDTO = _mapper.Map<DivisionPatchDTO>(divison);
            try
            {
                patchDocument.ApplyTo(divisonDTO);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            var isValid = TryValidateModel(divisonDTO);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(divisonDTO, divison);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
