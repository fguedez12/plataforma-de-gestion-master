using AutoMapper;
using GobEfi.FV.API.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MenuController(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<List<MenuDTO>>> Get()
        {
            var response = new UserResponseDTO();
            var role = "";
            ClaimsPrincipal currentUser = this.User;
            if (currentUser.Claims.Count() > 0)
            {
                
                role = currentUser.FindFirst(ClaimTypes.Role).Value;
                if (role == "ADMINISTRADOR")
                {
                    var menuAllDb = await _context.Menus
                                        .OrderBy(x=>x.Orden)
                                        .ToListAsync();
                    return _mapper.Map<List<MenuDTO>>(menuAllDb);
                }
                else {
                    var menusDb = await _context.Permisos
                                .Include(x => x.Menu)
                                .Where(p => p.Role == role)
                                .Select(x => x.Menu)
                                .OrderBy(x => x.Orden)
                                .ToListAsync();

                    return _mapper.Map<List<MenuDTO>>(menusDb);
                }
            }
            

            return new List<MenuDTO>();
        }

        [HttpGet("permisos")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public async Task<ActionResult<List<PermisoDTO>>> GetPermisos()
        {
            var response = new UserResponseDTO();
            var role = "";
            ClaimsPrincipal currentUser = this.User;
            if (currentUser.Claims.Count() > 0)
            {

                role = currentUser.FindFirst(ClaimTypes.Role).Value;
                
               
                    var permisosDb = await _context.Permisos
                                .Include(x => x.Menu)
                                .Where(p => p.Role == role)
                                .ToListAsync();

                    return _mapper.Map<List<PermisoDTO>>(permisosDb);
                
            }


            return new List<PermisoDTO>();
        }
    }
}
