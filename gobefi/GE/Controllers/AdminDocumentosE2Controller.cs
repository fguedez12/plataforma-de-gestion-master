using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Controllers
{
    public class AdminDocumentosE2Controller : BaseController
    {
        public AdminDocumentosE2Controller(ApplicationDbContext context,
            IUsuarioService service,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor) : base(context, manager, httpContextAccessor, service)
        {
            
        }

        public IActionResult Index()
        {
            ViewData["permisos"] = GetPermisions();
            ViewData["etapa"] = 2;
            return View();
        }
    }
}
