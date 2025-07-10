using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.InerciaTermicaModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InerciaTermicaController : ControllerBase
    {
        private readonly IInerciaTermicaService _servicioInercia;


        public InerciaTermicaController(IInerciaTermicaService servicioInercia)
        {
            _servicioInercia = servicioInercia;

        }

        // GET: api/InerciaTermica
        [HttpGet]
        public async Task<IActionResult> GetInercia()
        {
            try
            {
                IEnumerable<InerciaTermicaModel> entornos = await _servicioInercia.GetAllAsync();
                if (entornos.Count() == 0)
                {

                    return NotFound("No existen registros de Inercia Térmica para mostrar");
                }

                return Ok(entornos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

       
    }
}
