using api_gestiona.DTOs.Medidor;
using api_gestiona.Filters;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class MedidoresController : ControllerBase
    {
        private readonly IMedidorService _medidorService;

        public MedidoresController(IMedidorService medidorService)
        {
            _medidorService = medidorService;
        }

        [HttpGet("all")]
        [ServiceFilter(typeof(ApplicationValidation))]
        public async Task<ActionResult<List<MedidorDTO>>> GetAll([FromQuery] MedidorFilterDTO medidorFilterDTO)
        {
            var list = await _medidorService.GetAll(medidorFilterDTO);
            return list;
        }
    }
}
