using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ServicioModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ApiBaseController
    {
        private readonly IServicioService _servServicio;
        private readonly IServicioRepository _repository;
        private readonly ILogger _logger;

        public ServiciosController(
            ApplicationDbContext context,
            IServicioService service,
            IServicioRepository repository,
            ILoggerFactory factory,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servServicio = service;
            _repository = repository;
            _logger = factory.CreateLogger<ServiciosController>();
        }

        [HttpGet("{servicioId}")]
        public async Task<IActionResult> GetServicio(long servicioId)
        {
            ServicioModel servicio = await _servServicio.GetAsync(servicioId);

            return Ok(servicio);
        }

        // GET: api/Instituciones
        [HttpGet]
        public async Task<IActionResult> GetServicios()
        {
            IEnumerable<ServicioModel> servicios = await _servServicio.GetServicios();

            return Ok(servicios);
        }

        // GET: api/Servicios/getNoAsociadosByUserId/5
        [HttpGet("getNoAsociadosByUserId/{userId}")]
        public async Task<IActionResult> GetNoAsociadosByUserId([FromRoute] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<ServicioListModel> listaServicios = await _servServicio.GetNoAsociadosByUserId(userId);

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

            IEnumerable<ServicioListModel> listaServicios = await _servServicio.GetAsociadosByUserId(userId);

            if (listaServicios == null)
            {
                return NotFound();
            }

            return Ok(listaServicios);
        }

        // GET: api/Comuna/getByProvinciaId/5
        [HttpGet("getByInstitucionId/{institucionId}")]
        [AllowAnonymous]
        public async Task<IActionResult> getByInstitucionId([FromRoute] long institucionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituciones = await _servServicio.GetByInstitucionId(institucionId);

            if (instituciones == null)
            {
                return NotFound();
            }

            return Ok(instituciones);
        }
        [HttpGet("getByInstitucionIdAndUsuarioId/{institucionId}/{usuarioId}")]
        [AllowAnonymous]
        public async Task<IActionResult> getByInstitucionIdAndUsuarioId([FromRoute] long institucionId, [FromRoute] string usuarioId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituciones = await _servServicio.GetByInstitucionIdAndUserId(institucionId, usuarioId);

            if (instituciones == null)
            {
                return NotFound();
            }

            return Ok(instituciones);
        }

        [HttpGet("getByInstitucionIdAsync/{institucionId}")]
        [AllowAnonymous]
        public async Task<IActionResult> getByInstitucionIdAsync([FromRoute] long institucionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituciones = await _servServicio.GetServiciosByInstitucionIdAsync(institucionId);

            if (instituciones == null)
            {
                return NotFound();
            }

            return Ok(instituciones);
        }

        // GET: api/Servicios/byUserId/5/byInstitucionId/6
        [HttpGet("byUserId/{userId}/byInstitucionId/{institucionId}")]
        public async Task<IActionResult> GetNoAsociados([FromRoute] string userId,[FromRoute] long institucionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // retornar todos los servicios de la institucion que no esten asociados al usuario 

            IEnumerable<ServicioListModel> listaServicios = await _servServicio.GetNoAsociados(userId, institucionId);


            if (listaServicios == null)
            {
                return NotFound();
            }

            return Ok(listaServicios);
        }

        [Route("getserviciosbyuserandinstitucion/{userid}/{institucionid}")]
        public async Task<IActionResult> GetServiciosByUserAndInstitucion(string userId, long institucionId)
        {
            var result = await _servServicio.AllByUserAndInstitucion(userId, institucionId);

            return Ok(result);
        }

        [Route("getserviciosbyuser/{id}")]
        public async Task<IActionResult> GetServiciosByUser(string id)
        {
            var result = await _servServicio.AllByUser(id);

            return Ok(result);
        }

        [HttpGet("toogle/{id}/{divisionId}")]
        public IActionResult ToogleServicioCompras(int id, int divisionid)
        {
            _servServicio.Toogle(id,divisionid);

            return Ok();
        }
    }
}