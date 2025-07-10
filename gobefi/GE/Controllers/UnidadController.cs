using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Helper;
using GobEfi.Web.Models.UnidadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadController : BaseController
    {
        private readonly IUnidadService _service;
        private readonly IMapper _mapper;

        public UnidadController(
            ApplicationDbContext context,
            IUsuarioService servUsuario,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUnidadService service,
            IMapper mapper
            ) : base(context, manager, httpContextAccessor, servUsuario)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UnidadResponse> CreateUnidad(UnidadToSave request)
        {
            var response = new UnidadResponse();
            try
            {
                var list = new List<UnidadModel>();
                var newInmueble = await _service.Create(request);
                list.Add(newInmueble);
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

        [HttpGet("filter")]
        public async Task<ActionResult<List<UnidadListDTO>>> Filter([FromQuery] UnidadFilterDTO filter, [FromQuery] PaginationDTO paginationDTO)
        {
            var UnidadesQueryable = _service.Query();
            if (!string.IsNullOrEmpty(filter.Unidad))
            {
                UnidadesQueryable = UnidadesQueryable
                    .Where(x => x.Nombre.Contains(filter.Unidad));
            }

            await HttpContext.PaginationParams(UnidadesQueryable, paginationDTO.PerPage);
            var entidades = await UnidadesQueryable.Pager(paginationDTO).ToListAsync();
            return _mapper.Map<List<UnidadListDTO>>(entidades);
        }

        [HttpGet("getasociadosbyuser/{userId}")]
        public async Task<ActionResult<List<UnidadListDTO>>> ListByUser([FromQuery] UnidadFilterDTO filter, [FromRoute] string userId)
        {

            return await _service.GetAsociadosByUserId(userId);
        }

        [HttpGet("{unidadId}")]
        public async Task<UnidadDTO> GetInmueble(long unidadId) 
        {
            return await _service.Get(unidadId);

        }

        [HttpGet("getbyfilter")]
        public async Task<ActionResult<List<UnidadListDTO>>> GetByFilter([FromQuery] UnidadFilterDTO filtro)
        {

            return await _service.GetbyFilter(filtro.userId, filtro.InstitucionId, filtro.ServicioId, filtro.RegionId);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, [FromBody] UnidadPatchDTO model)
        //{
        //    var unidadFDB = await context.Unidades.FirstOrDefaultAsync(x => x.Id == id);
        //    if (unidadFDB == null) { return NotFound(); }

        //    unidadFDB.Nombre = model.Nombre;

        //    context.Entry(unidadFDB).State = EntityState.Modified;
        //    await context.SaveChangesAsync();

        //    return NoContent();
        //}


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] UnidadToUpdate model)
        {

            await _service.Update(id,model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var unidadFDB = await context.Unidades.FirstOrDefaultAsync(x => x.Id == id);
            if (unidadFDB == null) { return NotFound(); }

            await _service.DeleteUnidad(unidadFDB);

            //unidadFDB.Active = false;

            //context.Entry(unidadFDB).State = EntityState.Modified;
            //await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{nombre}/{unidadId}")]
        public async Task<bool> CheckUnidadNombre(string nombre,long unidadId)
        {
            if (await _service.CheckUnidadNombre(nombre, unidadId))
            {
                return true;
            }
            return false;
        }
    }
}
