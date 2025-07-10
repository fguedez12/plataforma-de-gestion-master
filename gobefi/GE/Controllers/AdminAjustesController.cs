using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Migrations;
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
    [Route("[controller]")]
    public class AdminAjustesController : BaseController
    {
        public AdminAjustesController(
            ApplicationDbContext context,
            IUsuarioService service,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor
            ) : base(context, manager, httpContextAccessor, service)
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["permisos"] = GetPermisions();
            return View();
        }

        [AllowAnonymous]
        [HttpGet("BloqueoInfoServicio/{userId}")]
        public async Task<IActionResult> BloqueoInfoServicio(string userId)
        {
            var servicios = await context.UsuariosServicios.Where(x=>x.UsuarioId == userId).Select(x => x.Servicio).ToListAsync();
            foreach (var servicio in servicios)
            {
                servicio.BloqueoIngresoInfo = true;
                await context.SaveChangesAsync();

                var role = "GESTOR DE CONSULTA";

                var users = await context.UsuariosServicios.Include(x => x.Usuario)
                    .Where(x => x.ServicioId == servicio.Id)
                    .Select(x => x.Usuario).ToListAsync();

                foreach (var usuario in users)
                {
                    if (!await manager.IsInRoleAsync(usuario, role) && !await manager.IsInRoleAsync(usuario, "ADMINISTRADOR") && !await manager.IsInRoleAsync(usuario, "AUDITOR"))
                    {
                        var rolesActuales = await manager.GetRolesAsync(usuario);
                        foreach (var roleActual in rolesActuales)
                        {
                            await manager.RemoveFromRoleAsync(usuario, roleActual);
                        }

                        var roleResult = await manager.AddToRoleAsync(usuario, role);
                    }
                }
            }
           
            return Ok();
        }

    }
}
