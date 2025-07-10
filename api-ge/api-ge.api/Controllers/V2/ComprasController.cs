using api_gestiona.DTOs.Compras;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace api_gestiona.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly IComprasService _comprasService;
        private readonly IConfiguration _configuration;

        public ComprasController(IComprasService comprasService, IConfiguration configuration)
        {
            _comprasService = comprasService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompraToSaveDTO compra)
        {
            try
            {
                var headerTokenName = _configuration["application-key:header"];
                if (headerTokenName == null)
                {
                    return BadRequest("No esta definido el nombre del header");
                }
                if (Request.Headers.TryGetValue(headerTokenName, out var tokenValue))
                {
                    // Usa el valor del encabezado aquí si es necesario
                    var customHeaderValue = tokenValue.ToString();
                    var storedTokenValue = _configuration["application-key:value"];
                    if (customHeaderValue != storedTokenValue)
                    {
                        return Unauthorized("el token de autorización no es válido");
                    }
                }
                else
                {
                    return Unauthorized("No se encuentra el token solicitado");
                }
                var response = await _comprasService.InsertCompra(compra);
                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            try
            {
                var headerTokenName = _configuration["application-key:header"];
                if (headerTokenName == null) {
                    return BadRequest("No esta definido el nombre del header");
                }
                if (Request.Headers.TryGetValue(headerTokenName, out var tokenValue))
                {
                    // Usa el valor del encabezado aquí si es necesario
                    var customHeaderValue = tokenValue.ToString();
                    var storedTokenValue = _configuration["application-key:value"];
                    if (customHeaderValue != storedTokenValue)
                    {
                        return Unauthorized("el token de autorización no es válido");
                    }
                }
                else {
                    return Unauthorized("No se encuentra el token solicitado");
                }

                await _comprasService.DeleteCompra(id);    
                return NoContent();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);   
            }
        }
    }
}
