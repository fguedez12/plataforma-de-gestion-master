using api_gestiona.DTOs.MedicionInteligente;
using api_gestiona.DTOs.MedicionInteligente.ResponseDTO;
using api_gestiona.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MedicionInteligenteController : Controller
    {
        private readonly IMedicionInteligenteService _service;

        public MedicionInteligenteController(IMedicionInteligenteService service)
        {
            _service = service;
        }

        [HttpGet("medidores/{id}")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            var medidores = await _service.GetMedidores(id);

            return Ok(medidores);
        }

        [HttpGet("mediciones/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMediciones([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesDefault(id);
            return Ok(mediciones);
        }

        [HttpGet("medicionespot/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesPot([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesDefaultPot(id);
            return Ok(mediciones);
        }

        [HttpGet("mediciones/mes/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesMes([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesMensual(id);
            return Ok(mediciones);
        }

        [HttpGet("medicionespot/mes/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesMesPot([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesMensualPot(id);
            return Ok(mediciones);
        }

        [HttpGet("mediciones/semana/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesSemana([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesSemanal(id);
            return Ok(mediciones);
        }

        [HttpGet("medicionespot/semana/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesSemanaPot([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesSemanalPot(id);
            return Ok(mediciones);
        }

        [HttpGet("mediciones/dia/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesDiarias([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesDiaria(id);
            return Ok(mediciones);
        }

        [HttpGet("medicionespot/dia/{id}")]
        public async Task<ActionResult<ApiResponseDTO>> GetMedicionesDiariasPot([FromRoute] int id)
        {
            var mediciones = await _service.GetMedicionesDiariaPot(id);
            return Ok(mediciones);
        }

        [HttpPost("consultaavanzada")]
        public async Task<ActionResult<ApiResponseDTO>> GetConsultaAvanzada([FromBody] RequestConsultaAvanzadaDTO request)
        {
            var mediciones = await _service.GetMedicionAvanzada(request);
            return Ok(mediciones);
        }

        [HttpPost("consultaavanzadapot")]
        public async Task<ActionResult<ApiResponseDTO>> GetConsultaAvanzadaPot([FromBody] RequestConsultaAvanzadaDTO request)
        {
            var mediciones = await _service.GetMedicionAvanzadaPot(request);
            return Ok(mediciones);
        }

    }
}
