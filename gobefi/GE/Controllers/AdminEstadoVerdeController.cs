using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    public class AdminEstadoVerdeController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMenuService _servMenu;
        private readonly IMapper _mapper;

        public AdminEstadoVerdeController(
            ApplicationDbContext context,
            IUsuarioService service,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IMenuService servMenu,
            IMapper mapper
            ) : base(context, manager, httpContextAccessor, service)
        {
            _context = context;
            _servMenu = servMenu;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(long id)
        {
            var unidad = await _context.Divisiones.FirstOrDefaultAsync(x=>x.Id == id);
            string rutaActual = httpContextAccessor.HttpContext.Request.Path.Value;

            ViewData["permisos"] = permisos;
            ViewData["subMenus"] = await _servMenu.GetSubMenusByMenuAndRoles(rutaActual, this.usuario.Id);
            return View(unidad);
        }
    }
}
