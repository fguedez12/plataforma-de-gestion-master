using api_gestiona.DTOs.Viajes;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ViajesController :ControllerBase
    {
        private readonly IPaisService _paisService;
        private readonly IAeropuertoService _aeropuertoService;
        private readonly IViajeService _viajeService;

        public ViajesController(IPaisService paisService, IAeropuertoService aeropuertoService,IViajeService viajeService)
        {
            _paisService = paisService;
            _aeropuertoService = aeropuertoService;
            _viajeService = viajeService;
        }

        [HttpGet("paises")]
        public async Task<ActionResult> GetPaisesList()
        {
            var resp = await _paisService.GetPaisList();
            return Ok(resp);
        }

        [HttpGet("aeropuerto/{paisId}")]
        public async Task<ActionResult> GetAeropuertoByPaisId(long paisId) 
        {
            var resp = await _aeropuertoService.GetAeropuertosByPaisId(paisId);
            return Ok(resp);
        }

        [HttpPost("{servicioId}/{year}")]
        public async Task<ActionResult> Post([FromRoute] long servicioId, ViajeCreateDTO model,[FromRoute] int year)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            try
            {
                await _viajeService.CreateViaje(userId, servicioId, model,year);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{viajeId}/{year}")]
        public async Task<ActionResult> Put([FromRoute] long viajeId, ViajeCreateDTO model,[FromRoute] int year)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            try
            {
                await _viajeService.UpdateViaje(userId,viajeId,model,year);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
