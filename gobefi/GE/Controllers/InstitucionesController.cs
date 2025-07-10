using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitucionesController : ApiBaseController
    {
        private readonly IInstitucionService _servInstitucion;
        private readonly IInstitucionRepository _repository;
        private readonly ILogger _logger;

        public InstitucionesController(
            ApplicationDbContext context,
            IInstitucionService servInstitucion,
            IInstitucionRepository repository,
            ILoggerFactory factory,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servInstitucion = servInstitucion;
            _repository = repository;
            _logger = factory.CreateLogger<InstitucionesController>();
        }

        // GET: api/Instituciones
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetInstituciones()
        {
            IEnumerable<InstitucionModel> instituciones = await _servInstitucion.GetInstituciones();

            return Ok(instituciones);
        }

        [Route("getinstituciones/{userId}")]
        public async Task<IActionResult> GetInstituciones(string userId)
        {
            var data = await _servInstitucion.AllByUser(userId);
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("getAsociadosByUserId/{userId}")]
        public async Task<IActionResult> GetAsociadosByUserId(string userId)
        {
            IEnumerable<InstitucionListModel> data = await _servInstitucion.GetAsociadosByUserId(userId);
            //IEnumerable<InstitucionListModel> data = await _servInstitucion.GetAsociados();
            return Ok(data);
        }

        [HttpGet("getNoAsociadosByUserId/{userId}")]
        public async Task<IActionResult> GetNoAsociadosByUserId(string userId)
        {
            IEnumerable<InstitucionListModel> data = await _servInstitucion.GetNoAsociadosByUserId(userId);
            return Ok(data);
        }

    }
}