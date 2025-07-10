using api_gestiona.DTOs.Sistemas;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SistemasController : ControllerBase
    {
        private readonly ISistemasService _sistemasService;

        public SistemasController(ISistemasService sistemasService)
        {
            _sistemasService = sistemasService;
        }
        [HttpGet("getData/{divisionId}")]
        public async Task<ActionResult> GetData([FromRoute] long divisionID)
        {
            try
            {
                var resp = await _sistemasService.GetData(divisionID);
                return Ok(resp);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("energetico-by-equipoid/{equipoId}")]
        public async Task<ActionResult<List<EnergeticoEquipoListDTO>>> GetEnergeticoByEquipoId([FromRoute] long equipoId)
        {
            try
            {
                var response = await _sistemasService.GetEnergeticoEquipo(equipoId);
                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("colectores")]
        public async Task<ActionResult<List<TipoColectorListDTO>>> GetColectores([FromQuery] string tipo)
        {
            var resp = await _sistemasService.GetTiposColectores(tipo);

            return resp;
        }

        [HttpPut("{divisionId}")]
        public async Task<ActionResult> SaveSistemasData([FromRoute] long divisionId,[FromBody] SistemasDataDTO model)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Los datos ingresados no son correctos");
            }

            var userId = User.Claims.First(i => i.Type == "userId").Value;
            try
            {
                await _sistemasService.SaveSistemaData(divisionId, userId, model);

                return NoContent();
            }
            catch (Exception Ex)
            {

                return BadRequest(Ex.Message);
            }
            
        }

    }
}
