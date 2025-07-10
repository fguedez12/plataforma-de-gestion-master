using api_gestiona.DTOs.PlanGestion;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/plan-gestion/filtros")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlanGestionFiltrosController : ControllerBase
    {
        private readonly IFiltrosService _filtrosService;

        public PlanGestionFiltrosController(IFiltrosService filtrosService)
        {
            _filtrosService = filtrosService;
        }

        /// <summary>
        /// Obtiene todos los filtros disponibles para el plan de gestión (dimensiones, objetivos y años)
        /// </summary>
        /// <returns>Objeto con todos los filtros disponibles</returns>
        [HttpGet]
        public async Task<ActionResult<FiltrosDTO>> GetFiltrosPlanGestion()
        {
            try
            {
                var filtros = await _filtrosService.GetFiltrosAsync();
                return Ok(filtros);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las dimensiones disponibles para filtrar en el plan de gestión
        /// </summary>
        /// <returns>Lista de dimensiones</returns>
        [HttpGet("dimensiones")]
        public async Task<ActionResult<List<DimensionFiltroDTO>>> GetDimensiones()
        {
            try
            {
                var dimensiones = await _filtrosService.GetDimensionesAsync();
                return Ok(dimensiones);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los objetivos disponibles para filtrar en el plan de gestión
        /// </summary>
        /// <param name="dimensionId">ID de dimensión para filtrar objetivos (opcional)</param>
        /// <returns>Lista de objetivos</returns>
        [HttpGet("objetivos")]
        public async Task<ActionResult<List<ObjetivoFiltroDTO>>> GetObjetivos([FromQuery] long? dimensionId = null)
        {
            try
            {
                var objetivos = await _filtrosService.GetObjetivosAsync(dimensionId);
                return Ok(objetivos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los objetivos filtrados por servicio y dimensión para la vista de implementación y seguimiento
        /// </summary>
        /// <param name="servicioId">ID del servicio para filtrar objetivos</param>
        /// <param name="dimensionId">ID de dimensión para filtrar objetivos (opcional)</param>
        /// <returns>Lista de objetivos filtrados por servicio y dimensión</returns>
        [HttpGet("objetivos-por-servicio")]
        public async Task<ActionResult<List<ObjetivoFiltroDTO>>> GetObjetivosByServicioDimension(
            [FromQuery] long servicioId,
            [FromQuery] long? dimensionId = null)
        {
            try
            {
                var objetivos = await _filtrosService.GetObjetivosByServicioDimensionAsync(servicioId, dimensionId);
                return Ok(objetivos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los años disponibles para filtrar en el plan de gestión
        /// </summary>
        /// <returns>Lista de años</returns>
        [HttpGet("anios")]
        public async Task<ActionResult<List<int>>> GetAnios()
        {
            try
            {
                var anios = await _filtrosService.GetAniosDisponiblesAsync();
                return Ok(anios);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}