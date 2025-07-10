using api_gestiona.DTOs.PlanGestion;
using api_gestiona.Services.Contracts;
using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlanGestionController : ControllerBase
    {
        private readonly IPlanGestionService _service;
        private readonly IDivisionService _divisionService;

        public PlanGestionController(IPlanGestionService service,IDivisionService divisionService)
        {
            _service = service;
            _divisionService = divisionService;
        }
        [HttpGet("{servicioId}")]
        public async Task<ActionResult<PlanGestionDTO>> GetByservicioId([FromRoute] long servicioId)
        {
            try
            {
                var response = await _service.GetPlanGetion(servicioId);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("dimension-servicio/{servicioId}")]
        public async Task<ActionResult> PostDimensionServicio([FromRoute] long servicioId, [FromBody] DimensionDTO dimension)
        {
            try
            {
                await _service.SaveDimensionServicio(dimension, servicioId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
           
        }

        [HttpGet("unidades-by-servicioId/{servicioId}")]
        public async Task<ActionResult> GetUnidadesListByServicioId(long servicioId, [FromQuery] string? searchtext)
        {
            try
            {
                var response = await _divisionService.GetListByServicioId(servicioId, searchtext);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }

        [HttpPost("save-brecha/{servicioId}")]
        public async Task<ActionResult> Savebrecha([FromRoute] long servicioId, [FromBody] BrechaToSaveDTO brecha)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.SaveBrecha(servicioId, brecha, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpGet("brechas/{servicioId}")]
        public async Task<ActionResult<List<BrechaListDTO>>> GetBrechas([FromRoute] long servicioId)
        {
            var result = await _service.GetBrechas(servicioId);
            return Ok(result);
        }

        [HttpGet("brecha-by-id/{id}")]
        public async Task<ActionResult<BrechaToEditDTO>> GetBrechaById([FromRoute] long id)
        {
            try
            {
                var response = await _service.GetBrechaById(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
           
        }

        [HttpPut("edit-brecha/{id}")]
        public async Task<ActionResult> Updatebrecha([FromRoute] long id, [FromBody] BrechaToEditDTO brecha)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.EditBrecha(id, brecha, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpDelete("brecha/{id}")]
        public async Task<ActionResult>Deletebrecha([FromRoute] long id)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.DeleteBrecha(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("save-objetivo")]
        public async Task<ActionResult> SaveObetivo([FromBody] ObjetivoToSaveDTO objetivo)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.SaveObjetivo(objetivo, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("objetivos/{servicioId}")]
        public async Task<ActionResult<List<ObjetivoListDTO>>> GetObjetivos([FromRoute] long servicioId)
        {
            var result = await _service.GetObjetivos(servicioId);
            return Ok(result);
        }

        [HttpGet("objetivo-by-id/{id}")]
        public async Task<ActionResult<BrechaToEditDTO>> GetObjetivoById([FromRoute] long id)
        {
            try
            {
                var response = await _service.GetObjetivoById(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut("edit-objetivo/{id}")]
        public async Task<ActionResult> UpdateObjetivo([FromRoute] long id, [FromBody] ObjetivoToEditDTO objetivo)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.EditObjetivo(id, objetivo, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpDelete("objetivo/{id}")]
        public async Task<ActionResult> DeleteObjetivo([FromRoute] long id)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.DeleteObjetivo(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpGet("medidas/{dimId}")]
        public async Task<ActionResult> GetMedidas(long dimId)
        {
            try
            {
                var response = await _service.GetMedidas(dimId);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpGet("unidades-by-objetivoId/{objetivoId}")]
        public async Task<ActionResult> GetUnidadesListByObjetivoId(long objetivoId)
        {
            try
            {
                var response = await _service.GetDivisionesByObjetivId(objetivoId);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet("usuarios-by-servicioId")]
        public async Task<ActionResult> GetUsuariosByServicioId([FromQuery] long servicioId)
        {
            try
            {
                var response = await _service.GetUserList(servicioId);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("otros-servicios/{servicioId}")]
        public async Task<ActionResult> GetServicios([FromRoute] long servicioId)
        {
            try
            {
                var response = await _service.GetServicios(servicioId);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPost("accion")]
        public async Task<ActionResult> Post(AccionToSaveDTO model)
        {
            try
            {
                model.UserId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.SaveAccion(model);
                return NoContent();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpGet("acciones/{servicioId}")]
        public async Task<ActionResult<List<AccionListDTO>>> GetAcciones([FromRoute] long servicioId, [FromQuery] bool includeTareas = false)
        {
            var result = await _service.GetAcciones(servicioId, includeTareas);
            return Ok(result);
        }

        [HttpGet("accion-by-id/{id}")]
        public async Task<ActionResult<AccionToEditDTO>> GetAccionById([FromRoute] long id)
        {
            try
            {
                var response = await _service.GetAccionId(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut("edit-accion/{id}")]
        public async Task<ActionResult> UpdateAccion([FromRoute] long id, [FromBody] AccionToSaveDTO model)
        {
            try
            {
                model.UserId = User.Claims.First(i => i.Type == "userId").Value; ;
                await _service.UpdateAccion(id,model);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("pga-info/{id}")]
        public async Task<ActionResult> UpdatePga([FromRoute] long id, [FromBody] PgaInfoDTO model)
        {
            try
            {
                model.UserId = User.Claims.First(i => i.Type == "userId").Value; ;
                await _service.UpdatePgaInfo(id,model);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("accion/{id}")]
        public async Task<ActionResult> DeleteAccion([FromRoute] long id)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.DeleteAccion(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPost("save-indicador")]
        public async Task<ActionResult> SaveIndicador([FromBody] IndicadorToSaveDTO indicador)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                indicador.UserId = userId;
                await _service.SaveIndicador(indicador);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("indicadores/{servicioId}")]
        public async Task<ActionResult<List<IndicadorListDTO>>> GetIndicadores([FromRoute] long servicioId)
        {
            try
            {
                var result = await _service.GetIndicadores(servicioId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
          
        }
        [HttpGet("indicador-by-id/{id}")]
        public async Task<ActionResult<IndicadorToEditDTO>> GetIndicadorById([FromRoute] long id)
        {
            try
            {
                var response = await _service.GetIndicadorById(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [HttpPut("edit-indicador/{id}")]
        public async Task<ActionResult> UpdateIndicador([FromRoute] long id, [FromBody] IndicadorToEditDTO model)
        {
            try
            {
                model.UserId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.EditIndicador(id, model);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("indicador/{id}")]
        public async Task<ActionResult> DeleteIndicador([FromRoute] long id)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.DeleteIndicador(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpPost("programa")]
        public async Task<ActionResult> PostPrograma(ProgramaToSaveDTO model)
        {
            try
            {
                model.UserId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.SavePrograma(model);
                return NoContent();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("programas/{servicioId}")]
        public async Task<ActionResult<List<ProgramaListDTO>>> GetProgramas([FromRoute] long servicioId)
        {
            try
            {
                var result = await _service.GetProgramas(servicioId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet("programa-by-id/{id}")]
        public async Task<ActionResult<ProgramaToEditDTO>> GetProgramaById([FromRoute] long id)
        {
            try
            {
                var response = await _service.GetProgramaById(id);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [HttpPut("edit-programa/{id}")]
        public async Task<ActionResult> UpdatePrograma([FromRoute] long id, [FromBody] ProgramaToEditDTO model)
        {
            try
            {
                model.UserId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.EditPrograma(id, model);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("programa/{id}")]
        public async Task<ActionResult> DeletePrograma([FromRoute] long id)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                await _service.DeletePrograma(id, userId);
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet("{servicioId}/pga")]
        [ProducesResponseType(typeof(PgaInfoDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPgaInfo(long servicioId)
        {
            try
            {
                var pgaInfo = await _service.GetPgaInfoAsync(servicioId);
                return Ok(pgaInfo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Endpoints para Tarea
        [HttpPost("save-tarea")]
        [Authorize]
        public async Task<IActionResult> SaveTarea([FromBody] TareaToSaveDTO tareaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.Claims.First(i => i.Type == "userId").Value;
                tareaDto.UserId = userId;
                var tareaId = await _service.SaveTareaAsync(tareaDto);
                return Created($"api/v2/plangestion/tarea-by-id/{tareaId}", new { id = tareaId });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("tareas/{servicioId}")]
        [Authorize]
        public async Task<IActionResult> GetTareasByServicio(long servicioId)
        {
            try
            {
                var result = await _service.GetTareasByServicioIdAsync(servicioId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("tarea-by-id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetTareaById(long id)
        {
            try
            {
                var response = await _service.GetTareaByIdAsync(id);
                if (response == null)
                {
                    return NotFound($"Tarea con ID {id} no encontrada");
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("edit-tarea/{id}")]
        [Authorize]
        public async Task<IActionResult> EditTarea(long id, [FromBody] TareaToEditDTO tareaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.Claims.First(i => i.Type == "userId").Value;
                tareaDto.UserId = userId;
                var success = await _service.EditTareaAsync(id, tareaDto);
                
                if (!success)
                {
                    return NotFound($"Tarea con ID {id} no encontrada");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("tarea/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTarea(long id)
        {
            try
            {
                var userId = User.Claims.First(i => i.Type == "userId").Value;
                var success = await _service.DeleteTareaAsync(id, userId);
                
                if (!success)
                {
                    return NotFound($"Tarea con ID {id} no encontrada");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
