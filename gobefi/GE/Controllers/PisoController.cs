using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.PisoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoController : ControllerBase
    {
        private readonly IPisoService _servicioPiso;

        public PisoController(IPisoService servicioPiso)
        {
            _servicioPiso = servicioPiso;
        }
        // GET: api/Piso/bydivision/5
        [HttpGet("bydivision/{id}")]
        public async Task<IActionResult> GetPisos([FromRoute] long Id)
        {
            try
            {
                IEnumerable<PisoModel> pisos = await _servicioPiso.GetByDivisionId(Id);

                return Ok(pisos);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET: api/Piso/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Piso
        [HttpPost]
        public async Task<ActionResult<PisoResponse>> PostPiso([FromBody] PisoForSaveModel piso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = new PisoResponse();
                response.Ok = true;
                response.Message = "OK";
                response.PisoPasoUnoList = await _servicioPiso.SavePiso(piso);
               

                return response;
            }
            catch (Exception ex)    
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Piso/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PisoResponse>> PutPiso([FromRoute] long id, [FromBody] PisoModel pisoFromBody)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                 await _servicioPiso.UpdateV2(id, pisoFromBody);



                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Piso/setFrontis/5
        //[HttpPut("setFrontis/{id}")]
        //public async Task<IActionResult> SetFrontis(long id,[FromBody] int muroIndex)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {

        //        var ret = await _servicioPiso.SetFrontis(id, muroIndex);

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PisoResponse>> DeletePiso([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = new PisoResponse();

                var piso = _servicioPiso.Get(id);
                if (piso == null)
                {
                    return NotFound();
                }

                response.Ok = true;
                response.Message = "OK";
                response.PisoPasoUnoList = await _servicioPiso.DeleteAsync(id);

                

                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addUnidad")]
        public async Task<PisoResponse> AddUnidad(PisoUnidadRequest request)
        {
            var response = new PisoResponse();
            try
            {
                await _servicioPiso.AddUnidad(request);

                response.Message = null;
                response.Ok = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                return response;
            }

        }

        [HttpPost("remunidad")]
        public async Task<PisoResponse> RemUnidad(PisoUnidadRequest request)
        {
            var response = new PisoResponse();
            try
            {
                await _servicioPiso.RemUnidad(request);

                response.Message = null;
                response.Ok = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
                return response;
            }

        }
    }
}
