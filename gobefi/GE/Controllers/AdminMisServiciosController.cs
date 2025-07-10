using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    public class AdminMisServiciosController : BaseController
    {
        private readonly IMenuService _servMenu;

        public AdminMisServiciosController(
            ApplicationDbContext context,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService servUsuario,
            IMenuService servMenu
            ) : base(context, manager, httpContextAccessor, servUsuario)
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
