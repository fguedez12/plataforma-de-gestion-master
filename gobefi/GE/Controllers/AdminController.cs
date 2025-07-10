using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GE.Core.Contracts.Services;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IMenuService _servMenu;

        public AdminController(
            ApplicationDbContext context,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService servUsuario,
            IMenuService servMenu,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servMenu = servMenu;
        }

        public async Task<IActionResult> Index()
        {
            string rutaActual = httpContextAccessor.HttpContext.Request.Path.Value;

            ViewData["permisos"] = permisos;
            ViewData["subMenus"] = await _servMenu.GetSubMenusByMenuAndRoles(rutaActual, this.usuario.Id);
            

            return View();
        }
    }
}