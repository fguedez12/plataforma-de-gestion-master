using api_gestiona.DTOs.PlanGestion;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/implementacion-seguimiento")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImplementacionSeguimientoController : ControllerBase
    {
        private readonly IPlanGestionService _planGestionService;
        private readonly IFiltrosService _filtrosService;

        public ImplementacionSeguimientoController(
            IPlanGestionService planGestionService,
            IFiltrosService filtrosService)
        {
            _planGestionService = planGestionService;
            _filtrosService = filtrosService;
        }

        /// <summary>
        /// Obtiene las brechas asociadas a un objetivo específico para la vista de implementación y seguimiento
        /// </summary>
        /// <param name="objetivoId">ID del objetivo para obtener sus brechas asociadas</param>
        /// <returns>Lista de brechas asociadas al objetivo</returns>
        [HttpGet("brechas/{objetivoId}")]
        public async Task<ActionResult<List<BrechaListDTO>>> GetBrechasByObjetivo([FromRoute] long objetivoId)
        {
            try
            {
                var result = await _planGestionService.GetBrechasByObjetivoId(objetivoId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las acciones asociadas a un objetivo específico para la vista de implementación y seguimiento
        /// </summary>
        /// <param name="objetivoId">ID del objetivo</param>
        /// <param name="anio">Año de ejecución (opcional)</param>
        /// <param name="includeTareas">Indica si se deben incluir las tareas asociadas a cada acción</param>
        /// <returns>Lista de acciones con sus tareas asociadas</returns>
        [HttpGet("acciones")]
        public async Task<ActionResult<List<AccionListDTO>>> GetAccionesByObjetivo(
            [FromQuery] long objetivoId,
            [FromQuery] int? anio = null,
            [FromQuery] bool includeTareas = false)
        {
            try
            {
                var result = await _planGestionService.GetAccionesByObjetivo(objetivoId, anio, includeTareas);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Actualiza una acción específica
        /// </summary>
        /// <param name="id">ID de la acción a actualizar</param>
        /// <param name="accion">Datos de la acción a actualizar</param>
        /// <returns>NoContent si es exitoso</returns>
        [HttpPut("accion/{id}")]
        public async Task<ActionResult> UpdateAccion([FromRoute] long id, [FromBody] AccionToSaveDTO accion)
        {
            try
            {
                accion.UserId = User.Claims.First(i => i.Type == "userId").Value;
                await _planGestionService.UpdateAccion(id, accion);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Actualiza una tarea específica
        /// </summary>
        /// <param name="id">ID de la tarea a actualizar</param>
        /// <param name="tarea">Datos de la tarea a actualizar</param>
        /// <returns>NoContent si es exitoso</returns>
        [HttpPut("tarea/{id}")]
        public async Task<ActionResult> UpdateTarea([FromRoute] long id, [FromBody] TareaToEditDTO tarea)
        {
            try
            {
                tarea.UserId = User.Claims.First(i => i.Type == "userId").Value;
                var success = await _planGestionService.EditTareaAsync(id, tarea);
                
                if (!success)
                {
                    return NotFound($"Tarea con ID {id} no encontrada");
                }

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una acción específica con todos sus datos para edición
        /// </summary>
        /// <param name="id">ID de la acción</param>
        /// <returns>Datos completos de la acción para edición</returns>
        [HttpGet("accion/{id}")]
        public async Task<ActionResult<AccionToEditDTO>> GetAccionById([FromRoute] long id)
        {
            try
            {
                var result = await _planGestionService.GetAccionId(id);
                if (result == null)
                {
                    return NotFound($"Acción con ID {id} no encontrada");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}