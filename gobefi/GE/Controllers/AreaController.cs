using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.AreaDTO;
using GobEfi.Web.Models;
using GobEfi.Web.Models.AreaModels;
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
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly ApplicationDbContext _context;
        private readonly IUsuarioService _servUsuario;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;
        private readonly Usuario _currentUser;

        public AreaController(IAreaService areaService
            , ApplicationDbContext context
             ,IUsuarioService servUsuario,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
            ) 
        {
            _areaService = areaService;
            _context = context;
            _servUsuario = servUsuario;
            _userManager = userManager;
            _mapper = mapper;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        // POST: api/Area
        [HttpPost]
        public async Task<ActionResult<AreaResponse>> PostArea([FromBody] AreaForSave Area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = new AreaResponse
                {
                    Ok = true,
                    Message = "OK",
                    List = await _areaService.Save(Area)
                };


                return response;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addUnidad")]
        public async Task<AreaResponse> AddUnidad(AreaUnidadRequest request)
        {
            var response = new AreaResponse();
            try
            {
                await _areaService.AddUnidad(request);
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
        public async Task<AreaResponse> RemUnidad(AreaUnidadRequest request)
        {
            var response = new AreaResponse();
            try
            {
                await _areaService.RemUnidad(request);
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

        [HttpPut("{id}")]
        public async Task<ActionResult<AreaResponse>> UpdateArea(AreaEditDTO request)
        {
            var areaFromDb = await _context.Areas.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (areaFromDb == null) 
            {
                return NotFound();
            }

            areaFromDb.Nombre = request.Nombre;
            areaFromDb.TipoUsoId = request.TipoUsoId;
            areaFromDb.Superficie = request.Superficie;
            areaFromDb.NroRol = request.NroRol;
            areaFromDb.UpdatedAt = DateTime.Now;
            areaFromDb.Version = ++areaFromDb.Version;
            areaFromDb.ModifiedBy = _currentUser.Id;
            await _context.SaveChangesAsync();

            var pisoFromDb = await _context.Pisos.Include(p => p.Areas).Where(p => p.Id == request.PisoId).FirstOrDefaultAsync();

            foreach (var area in pisoFromDb.Areas.ToList())
            {
                if (!area.Active)
                {
                    pisoFromDb.Areas.Remove(area);
                }
            }

            var response = new AreaResponse
            {
                Ok = true,
                Message = "OK",
                List = _mapper.Map<List<AreaModel>>(pisoFromDb.Areas.OrderBy(a => a.Id))
            };


            return response;
        }

    }
}
