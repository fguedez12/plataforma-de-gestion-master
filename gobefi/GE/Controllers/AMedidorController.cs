using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.AMedidorModels;
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
    public class AMedidorController : BaseController
    {
        private readonly IAMedidorService _service;
        public AMedidorController(IAMedidorService service, ApplicationDbContext context,
            IUsuarioService servUsuario,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor) : base(context, manager, httpContextAccessor, servUsuario)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<MedidorResponse> GetMedidores(int? filtroServicio,int page = 1, int? id=null)
        {
            var response = new MedidorResponse();
            try
            {
                response.MedidoresPorPagina = await _service.ListMedidores(filtroServicio,page,id);
                response.Ok = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Message = "Ocurrio un error: " + ex.Message;
                return response;
            }

        }

        [HttpPost]
        public async Task<MedidorResponse> UpdateMedidor(MedidorModel model, int? filtroServicio, int page = 1, int? id=null) {
            var response = new MedidorResponse();
            try
            {
                await _service.UpdateMedidor(model);
                response.MedidoresPorPagina = await _service.ListMedidores(filtroServicio, page,id);
                response.Ok = true;
                return response;
            }
            catch (Exception ex)
            {

                response.Ok = false;
                response.Message = "Ocurrio un error: " + ex.Message;
                return response;
            }
        }
    }
}
