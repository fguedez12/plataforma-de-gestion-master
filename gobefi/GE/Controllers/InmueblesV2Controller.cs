using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using GobEfi.Web.DTOs.InmuebleDTO;
using GobEfi.Web.Models.InmuebleModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/inmuebles/V2")]
    public class InmueblesV2Controller : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IInmuebleService _service;
        private readonly IUsuarioService _servUsuario;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;
        private readonly Usuario _currentUser;

        public InmueblesV2Controller(
            ApplicationDbContext context, 
            IMapper mapper, 
            IInmuebleService  service,
            IUsuarioService servUsuario,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor
            )
            :base(context, mapper)
        {
            _context = context;
            _service = service;
            _servUsuario = servUsuario;
            _userManager = userManager;
            _mapper = mapper;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }
        public async Task<ActionResult<List<InmuebleListDTO>>> Get()
        {
            return await Get<Division, InmuebleListDTO>();
        }
        [HttpGet("GEV3")]
        public async Task<ActionResult<List<InmuebleListDTO>>> GetGEV3([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _context.Divisiones.AsQueryable();
            queryable = queryable.Where(x => x.Active && x.GeVersion == 3);

            return await Get<Division, InmuebleListDTO>(paginationDTO,queryable);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<InmuebleListDTO>>> Filter([FromQuery] InmuebleFilterDTO filter, [FromQuery] PaginationDTO paginationDTO)
        {
            var InmuebleQueryable = _service.Query();
            if (!string.IsNullOrEmpty(filter.Direccion))
            {
                InmuebleQueryable  = InmuebleQueryable
                    .Where(x => x.DireccionInmueble.Calle.Contains(filter.Direccion) ||  x.DireccionInmueble.Numero.Contains(filter.Direccion));
            }
            if (filter.RegionId !=null) 
            {
                InmuebleQueryable = InmuebleQueryable.Where(x => x.DireccionInmueble.RegionId == filter.RegionId);
            }
            if (filter.ComunaId != null)
            {
                InmuebleQueryable = InmuebleQueryable.Where(x => x.DireccionInmueble.ComunaId == filter.ComunaId);
            }

            return await Get<Division, InmuebleListDTO>(paginationDTO, InmuebleQueryable);
        }

        [HttpGet("{id:long}")]
        public async Task<InmuebleModel> GetById(long id) 
        {
            return await _service.GetById(id);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            await _service.DeleteInmueble(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, [FromBody] InmuebleEditDTO request)
        {
            var edificioFromDB = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            edificioFromDB.Nombre = request.Nombre;
            edificioFromDB.Superficie = request.Superficie;
            edificioFromDB.AnyoConstruccion = request.AnyoConstruccion;
            edificioFromDB.TipoUsoId = request.TipoUsoId;
            edificioFromDB.AdministracionServicioId = request.AdministracionServicioId;
            edificioFromDB.NroRol = request.NroRol;
            edificioFromDB.ModifiedBy = _currentUser.Id;
            edificioFromDB.UpdatedAt = DateTime.Now;
            edificioFromDB.Version = ++edificioFromDB.Version;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
