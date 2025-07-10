using api_gestiona.DTOs.Servicios;
using api_gestiona.Filters;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IServicioService _servicioService;

        public ServiciosController(IMapper mapper, IServicioService servicioService)
        {
            _mapper = mapper;
            _servicioService = servicioService;
        }

        [HttpGet("all")]
        [ServiceFilter(typeof(ApplicationValidation))]
        public async Task<ActionResult<List<ServicioListDTO>>> GetAll()
        {
            var list = await _servicioService.GetAll();
            return list;
        }

        [HttpGet("by-id/{id}")]
        [ServiceFilter(typeof(ApplicationValidation))]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var exist = await _servicioService.Exist(id);
            if(!exist)
            {
                return NotFound("No existe el recurso solicitado");
            }
            var serivicio = await _servicioService.GetById(id);
            return Ok(serivicio);
        }
    }
}
