using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.DivisionModels;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Services.Request;
using System;
using GobEfi.Web.Models.DisenioPasivoModels;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionesController : ApiBaseController
    {
        private readonly IDivisionService _servDivision;
        private readonly ILogger _logger;

        public DivisionesController(
            ApplicationDbContext context, 
            IDivisionService servDivision,
            ILoggerFactory factory, 
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servDivision = servDivision;
            _logger = factory.CreateLogger<DivisionesController>();
        }

        
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AsociarUsuarioDivision([FromBody] UsuarioDivisionRequest usuarioDivision)
        {
            bool isChecked = usuarioDivision.IsChecked;
            int result = -1;
            if (isChecked)
            {
                if (string.IsNullOrEmpty(usuarioDivision.UsuarioId))
                    return BadRequest("Id usuario es vacio o null");

                // validar que el usuario tenga acceso a la division segun sus servicios asociados
                bool isValid = _servDivision.SePuedenAsociar(usuarioDivision.UsuarioId, usuarioDivision.DivisionId);
                if (!isValid)
                {
                    return BadRequest("Usuario no tiene asociado el servicio de la unidad.");
                }

                // agregar la asociacion 
                result = await _servDivision.AsociarUsuario(usuarioDivision.UsuarioId, usuarioDivision.DivisionId);
                _logger.LogWarning($"Se asocio la unidad :{usuarioDivision.DivisionId}, al usuario :{usuarioDivision.UsuarioId} creado por:{usuario.Id}");
                

                return CreatedAtAction(nameof(GetAsociadosByUserId), new { userId = usuarioDivision.UsuarioId });
            }

            // eliminar la asociacion 
            result = await _servDivision.EliminarAsociacionConUsuario(usuarioDivision.UsuarioId, usuarioDivision.DivisionId);
            _logger.LogWarning($"Se elimino la unidad :{usuarioDivision.DivisionId}, al usuario :{usuarioDivision.UsuarioId} creado por:{usuario.Id}");

            return CreatedAtAction(nameof(GetAsociadosByUserId), new { userId = usuarioDivision.UsuarioId });

        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AsociarUsuarioDivisionv2([FromBody] UsuarioUnidadRequest usuarioUnidad)
        {
            bool isChecked = usuarioUnidad.IsChecked;
            int result = -1;
            if (isChecked)
            {
                if (string.IsNullOrEmpty(usuarioUnidad.UsuarioId))
                    return BadRequest("Id usuario es vacio o null");

                // validar que el usuario tenga acceso a la division segun sus servicios asociados
                bool isValid = await _servDivision.SePuedenAsociarv2(usuarioUnidad.UsuarioId, usuarioUnidad.UnidadId);
                if (!isValid)
                {
                    return BadRequest("Usuario no tiene asociado el servicio de la unidad.");
                }

                // agregar la asociacion 
                result = await _servDivision.AsociarUsuariov2(usuarioUnidad.UsuarioId, usuarioUnidad.UnidadId);
                _logger.LogWarning($"Se asocio la unidad :{usuarioUnidad.UnidadId}, al usuario :{usuarioUnidad.UsuarioId} creado por:{usuario.Id}");


                return CreatedAtAction(nameof(GetAsociadosByUserId), new { userId = usuarioUnidad.UsuarioId });
            }

            // eliminar la asociacion 
            result = await _servDivision.EliminarAsociacionConUsuariov2(usuarioUnidad.UsuarioId, usuarioUnidad.UnidadId);
            _logger.LogWarning($"Se elimino la unidad :{usuarioUnidad.UnidadId}, al usuario :{usuarioUnidad.UsuarioId} creado por:{usuario.Id}");

            return CreatedAtAction(nameof(GetAsociadosByUserId), new { userId = usuarioUnidad.UsuarioId });

        }

        // GET: api/Servicios/getNoAsociadosByUserId/5
        [HttpGet("getNoAsociadosByUserId/{userId}")]
        public async Task<IActionResult> GetNoAsociadosByUserId([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<DivisionListModel> listaServicios = await _servDivision.GetNoAsociadosByUserId(userId);

            if (listaServicios == null)
            {
                return NotFound();
            }

            return Ok(listaServicios);
        }

        // GET: api/Servicios/getAsociadosByUserId/5
        [HttpGet("getAsociadosByUserId/{userId}")]
        public async Task<IActionResult> GetAsociadosByUserId([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<DivisionListModel> listaServicios = await _servDivision.GetAsociadosByUserId(userId);

            if (listaServicios == null)
            {
                return NotFound();
            }

            return Ok(listaServicios);
        }

        // GET: api/Servicios/getByInstitucionId/5
        [HttpGet("byUserId/{userId}/byServicioId/{servicioId}")]
        public async Task<IActionResult> GetNoAsociados([FromRoute] string userId, [FromRoute] long servicioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // retornar todos los servicios de la institucion que no esten asociados al usuario 

            IEnumerable<DivisionListModel> listaServicios = await _servDivision.GetNoAsociados(userId, servicioId);


            if (listaServicios == null)
            {
                return NotFound();
            }

            return Ok(listaServicios);
        }

        [HttpGet("getByServicioId/{servicioId}")]
        public async Task<IActionResult> GetByServicioId(long servicioId)
        {
            IEnumerable<DivisionListModel> lista = await _servDivision.GetByServicioId(servicioId);

            return Ok(lista);
        }

        [HttpGet("getByEdificioId/{edificioId}")]
        public async Task<IActionResult> GetByEdificioId(long edificioId)
        {
            IEnumerable<DivisionListModel> lista = await _servDivision.GetByEdificioId(edificioId);

            return Ok(lista);
        }


        [HttpGet("{divisionId}")]
        public async Task<IActionResult> GetDivision(long divisionId)
        {
            return Ok(await _servDivision.GetAsync(divisionId));
        }

        [HttpGet]
        [Route("getdivisiones")]
        public IActionResult GetDivisiones()
        {
            return Ok(_servDivision.All());
        }

        [HttpGet]
        [Route("getdivisionesbyuser/{id}")]
        public async Task<IActionResult> GetDivisionesByUser(string id)
        {
            var result = await _servDivision.AllByUser(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("getdivisionesbyuserandservicio/{userId}/{servicioId}")]
        public async Task<IActionResult> GetDivisionesByUserAndServicio(string userId, long servicioId)
        {
            var divisiones = await _servDivision.AllByUserAndServicio(userId, servicioId);

            foreach (var divisionItem in divisiones)
            {
                divisionItem.Nombre = divisionItem.Direccion;
            }


            return Ok(divisiones);
        }

        [HttpGet("GetByFilters/{userId}/{servicioId}/{regionId?}")]
        public async Task<IActionResult> GetByFilters(string userId, long servicioId, long? regionId = null)
        {
            var result = await _servDivision.GetByFilters(userId, servicioId, regionId);

            return Ok(result);
        }

        [HttpGet("GetByServicioId/{servicioId?}/ByRegionId/{regionId}/byPmg/{pmg}")]
        public async Task<IActionResult> GetByServicioRegion(bool pmg, long regionId , long? servicioId = null)
        {
            IEnumerable<DivisionListModel> result = await _servDivision.GetByServicioRegion(servicioId, regionId, pmg);

            return Ok(result);
        }

        [HttpPost("pisosiguales/{divisionId}/{value}")]
        public async Task<ActionResult<DivisionResponse>> SetPisosIguales([FromRoute] long divisionId, bool value)
        {
            try
            {
                var response = new DivisionResponse
                {
                    Ok = true,
                    Message = "OK"
                };
                await _servDivision.SetPisosIguales(divisionId, value);

                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        //[HttpGet("getPisos/{divisionId}")]
        //public async Task<ActionResult> GetPisos(long divisionId)
        //{
        //    try
        //    {
        //        return Ok(await _servDivision.GetDivisionPisos(divisionId));
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}
        [HttpGet("reporte-consumo")]
        public async Task<ActionResult<UnidadReporteConsumoModel>> GetReporteConsumo()
        {
            var model = await _servDivision.GetReporteConsumo(usuario.Id);
            return Ok(model);
        }

        [HttpPost("justificacion")]
        public async Task<IActionResult> Justificacion(JustificaModel model)
        {
            await _servDivision.JustificaReporteConsumo(model);
            return NoContent();
        }

        [HttpPost("observar-justificacion")]
        public async Task<IActionResult> observarJustificacion(JustificaModel model)
        {
            await _servDivision.ObservarJustificacion(model,usuario.Id);
            return NoContent();
        }
        [HttpPost("validar-justificacion")]
        public async Task<IActionResult> validarJustificacion(JustificaModel model)
        {
            await _servDivision.ValidarJustificacion(model, usuario.Id);
            return NoContent();
        }


    }
}