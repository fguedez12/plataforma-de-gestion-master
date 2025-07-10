using GobEfi.Services.Models;
using GobEfi.Services.Models.MedidoresModels;
using GobEfi.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MedidorController : ControllerBase
    {
        private readonly IMedidorService service;
        private readonly DbContext context;

        public MedidorController(IMedidorService service, DbContext context)
        {
            this.service = service;
            this.context = context;
        }
        [HttpPost]
        public async Task<ActionResult> Post(MedidorModel medidor)
        {
            if (medidor is null)
            {
                return BadRequest();
            }
            try
            {
                var result = await service.Create(medidor);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }
        [HttpGet("byunidadid")]
        public async Task<IActionResult> GetByUnidad(MedidorByUnidadesRequest request)
        {
            var result = new MedidorByUnidadesResponse();
            try
            {
                result.Ok = true;
                result.Message = "Consulta ejecutada correctamente";
                result.Unidades = await service.GetByUnidades(request.IdsUnidades);
            }
            catch (Exception ex)
            {

                result.Ok = false;
                result.Message = "Ocurrio un error: " + ex.Message;
            }

            return Ok(result);
        }
    }
}
