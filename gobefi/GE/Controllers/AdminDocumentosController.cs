using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Controllers
{
    public class AdminDocumentosController : BaseController
    {
        public AdminDocumentosController(
            ApplicationDbContext context,
            IUsuarioService service,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor) : base(context, manager, httpContextAccessor, service)
        {

        }

        public IActionResult Index()
        {
            ViewData["permisos"] = GetPermisions();
            ViewData["etapa"] = 1;
            return View();
        }
    }
}
