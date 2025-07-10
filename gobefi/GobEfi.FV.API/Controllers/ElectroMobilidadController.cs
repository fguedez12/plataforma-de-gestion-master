using AutoMapper;
using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.API.Services;
using GobEfi.FV.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElectroMobilidadController  : CustomBaseController
    {
        private readonly IElectroMobilidadService _electroMobilidadService;
        private readonly ILogger _logger;

        public ElectroMobilidadController(ApplicationDbContext context, 
            IMapper mapper,
            IElectroMobilidadService electroMobilidadService,
            ILogger<ElectroMobilidadController> logger
            ) : base(context, mapper)
        {
            _electroMobilidadService = electroMobilidadService;
            _logger = logger;
        }

        [HttpGet("marcas")]
        public async Task<ActionResult> GetMarca()
        {
            _logger.LogDebug("Inicio peticion Marca");
            var list = await _electroMobilidadService.GetMarca();
            return  Ok(list);
        }

        [HttpGet("carrocerias")]
        public async Task<ActionResult> GetCarroceria()
        {

            return Ok(await _electroMobilidadService.GetCarroceria());
        }
        [HttpGet("propulsiones")]
        public async Task<ActionResult> GetPropulsion()
        {

            return Ok(await _electroMobilidadService.GetPropulsion());
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<ModeloDTO>>> buscarModelo([FromQuery] BuscarModeloDTO buscar)
        {
            var result = await _electroMobilidadService.BuscarModelo(buscar);
            return Ok(result);
        }

    }
}
