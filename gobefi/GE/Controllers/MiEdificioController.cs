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
    public class MiEdificioController : BaseController
    {
        private readonly IMenuService _servMenu;
        private readonly IDivisionService _divisionService;

        public MiEdificioController(
            IMenuService servMenu,
            IDivisionService divisionService,
            ApplicationDbContext context, 
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService)
            : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servMenu = servMenu;
            _divisionService = divisionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Ver(long id)
        {
            string rutaActual = httpContextAccessor.HttpContext.Request.Path.Value;

            ViewData["subMenus"] = await _servMenu.GetSubMenusByMenuAndRoles(rutaActual, this.usuario.Id);

            HttpContext.Session.SetString("DivisionId", id.ToString());


            var model = _divisionService.Ver(id);
            //var model = new DivisionVerModel();
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult DisenioPasivo(long id)
        {
            ViewData["permisos"] = GetPermisions();
            return View("DisenioPasivo");
        }
    }
}