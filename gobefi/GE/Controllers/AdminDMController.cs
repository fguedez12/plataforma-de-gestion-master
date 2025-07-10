using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Controllers
{
    public class AdminDMController : BaseController
    {
        private readonly IMenuService _servMenu;

        public AdminDMController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor,
            IMenuService servMenu,
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
            _servMenu = servMenu;
        }

        public async Task<IActionResult> Index()
        {
            string rutaActual = httpContextAccessor.HttpContext.Request.Path.Value;

            ViewData["permisos"] = GetPermisions();
            ViewData["subMenus"] = await _servMenu.GetSubMenusByMenuAndRoles(rutaActual, this.usuario.Id);
            
            return View();
        }
    }
}