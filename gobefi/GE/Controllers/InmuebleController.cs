using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InmuebleModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InmuebleController : BaseController
    {
        private readonly IInmuebleService _service;
        private readonly IInstitucionService _serviceInstitucion;
        private readonly IServicioService _serviceServicio;

        public InmuebleController
            (
            ApplicationDbContext context,
            IUsuarioService servUsuario,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IInmuebleService service,
            IInstitucionService serviceInstitucion,
            IServicioService serviceServicio
            ) : base(context, manager, httpContextAccessor, servUsuario)
        
        {
            _service = service;
            _serviceInstitucion = serviceInstitucion;
            _serviceServicio = serviceServicio;
        }

        [HttpPost]
        public async Task<InmuebleResponse> CreateInmueble(InmuebleToSaveRequest request)
        {
            var response = new InmuebleResponse();
            try
            {
                var list = new List<InmuebleModel>();
                var newInmueble = await _service.Create(request);
                list.Add(newInmueble);
                response.Message = null;
                response.Inmuebles = list;
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

        [HttpPut]
        public async Task<ActionResult> UpdateInmueble(InmuebleToUpdateRequest request)
        {
            
            try
            {
                var list = new List<InmuebleModel>();
                await _service.Update(request);
                return NoContent();
               
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }

        }



        [HttpPost("getbyaddress")]
        public async Task<InmuebleResponse> GetByAddress(InmuebleByAddressRequest request)
        {
            var response = new InmuebleResponse();
            try
            {
                var list = await _service.GetByAddress(request);

                response.Message = null;
                response.Inmuebles = list;
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

        [HttpPost("addUnidad")]
        public async Task<InmuebleResponse> AddUnidad(InmuebleUnidadRequest request)
        {
            var response = new InmuebleResponse();
            try
            {
                await _service.AddUnidad(request);

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
        public async Task<InmuebleResponse> RemUnidad(InmuebleUnidadRequest request)
        {
            var response = new InmuebleResponse();
            try
            {
                await _service.RemUnidad(request);

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

        [HttpGet("userdata")]
        public async Task<DataResponse> GetUserData()
        {
            var response = new DataResponse();
            try
            {
                var list = await _serviceInstitucion.AllByUser(usuario.Id);

                response.Message = null;
                response.Instituciones = list;
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

        [HttpGet("getUnidades/{id}")]
        public async Task<DataResponse> GetUserData([FromRoute] long id)
        {
            var response = new DataResponse();
            try
            {
                var list = await _service.GetUnidades(id);

                response.Message = null;
                response.Unidades = list;
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
