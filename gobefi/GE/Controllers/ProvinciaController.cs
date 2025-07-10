using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.ProvinciaModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _servProvincia;

        public ProvinciaController(IProvinciaService servProvincia)
        {
            _servProvincia = servProvincia;
        }

        // GET: api/Provincia
        [HttpGet]
        public IEnumerable<ProvinciaModel> GetProvincias()
        {
            return _servProvincia.All();
        }

        // GET: api/Provincia/5
        [HttpGet("getByRegionId/{regionId}")]
        public async Task<IActionResult> GetByRegionId([FromRoute] long regionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var provincias = await _servProvincia.GetByRegionId(regionId);

            if (provincias == null)
            {
                return NotFound();
            }

            return Ok(provincias);
        }
    }
}