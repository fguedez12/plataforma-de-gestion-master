using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.EstadoValidacionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoValidacionController : ControllerBase
    {
        private readonly IEstadoValidacionService _servEstadoValidacion;

        public EstadoValidacionController(IEstadoValidacionService  servEstadoValidacion)
        {
            _servEstadoValidacion = servEstadoValidacion;
        }

        [HttpGet("getestados")]
        public async Task<IActionResult> GetEstados()
        {
            IEnumerable<EstadoValidacionModel> estados = await _servEstadoValidacion.GetAllAsync();

            return Ok(estados);
        }

    }
}