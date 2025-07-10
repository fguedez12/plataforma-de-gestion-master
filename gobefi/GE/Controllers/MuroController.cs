using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PisoModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuroController : ControllerBase
    {
        private readonly IMuroService _muroService;

        public MuroController(IMuroService muroService)
        {
            _muroService = muroService;
        }
        [HttpPost]
        public async Task<ActionResult<MuroResponse>> Post(MurosForSave dataForSave)
        {
            try
            {
                var response = new MuroResponse
                {
                    Ok = true,
                    Message = "OK",
                    MuroList = await _muroService.SaveMuros(dataForSave)
                };

                return response;
            }
            catch (Exception ex)
            {

                return BadRequest($"Ha ocurrido un error: {ex.Message}");
            }

        }
        [HttpPost("internos")]
        public async Task<ActionResult<MuroResponse>> PostInternos(MurosForSave dataForSave)
        {
            try
            {
                var response = new MuroResponse
                {
                    Ok = true,
                    Message = "OK",
                    MuroList = await _muroService.SaveMurosInternos(dataForSave)
            };
               
                return response;
            }
            catch (Exception ex)
            {

                return BadRequest($"Ha ocurrido un error: {ex.Message}");
            }

        }

        [HttpPut("murodisable")]
        public async Task<ActionResult> DisableMuros(MurosForSave dataForSave)
        {
            try
            {
                await _muroService.DisableMuros(dataForSave);
                return await Task.FromResult(Ok("OK"));
            }
            catch (Exception ex)
            {

                return BadRequest($"Ha ocurrido un error: {ex.Message}");
            }
        }
        [HttpPut("murodisable/{pisoId}")]
        public async Task<ActionResult> DisableMurosByPiso([FromRoute] long pisoId)
        {
            try
            {
                await _muroService.DisableMurosByPiso(pisoId);
                return await Task.FromResult(Ok("OK"));
            }
            catch (Exception ex)
            {

                return BadRequest($"Ha ocurrido un error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute]long id, MuroModel muro) {
            try
            {
                await _muroService.UpdateMuro(id, muro);
                return  await Task.FromResult(Ok("ok"));
            }
            catch (Exception ex)
            {

                return BadRequest($"Ha ocurrido un error: {ex.Message}");
            }
        
        }
    }
}
