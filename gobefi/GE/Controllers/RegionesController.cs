using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RegionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionesController : ApiBaseController
    {
        private readonly IRegionService _service;
        private readonly ILogger _logger;

        public RegionesController(
            ApplicationDbContext context,
            IRegionService service,
            ILoggerFactory factory,
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _service = service;
            _logger = factory.CreateLogger<RegionesController>();
        }

        [HttpGet]
        [Route("getregiones")]
        public async Task<IActionResult> GetRegiones()
        {
            return Ok(await _service.AllAsync());
        }

        [HttpGet]
        [Route("getregioneswithcomunas")]
        public async Task<IActionResult> GetRegionesWithComunas()
        {
            return Ok(await _service.AllRegionesWithComunas());
        }

        [HttpGet]
        [Route("getByServicioId/{servicioId}")]
        public async Task<IActionResult> getByServicioId(long servicioId)
        {
            IEnumerable<RegionModel> regiones = await _service.GetByServicioId(servicioId);

            return Ok(regiones);
        }

    }
}