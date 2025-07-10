using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Controllers;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AdminConsumosController : BaseController
    {
        private readonly IInstitucionService _servInstitucion;
        private readonly IRegionService _servRegion;


        public AdminConsumosController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service,
            IInstitucionService servInstitucion,
            IRegionService servRegion) : base(context, manager, httpContextAccessor, service)
        {
            _servInstitucion = servInstitucion;
            _servRegion = servRegion;
        }

        private void LlenarCombosIndex()
        {
            ViewBag.Instituciones = SelectHelper.LlenarDDL(_servInstitucion.GetInstituciones().Result, true);
            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
        }

        public IActionResult Index()
        {
            ViewData["permisos"] = GetPermisions();
           
            //LlenarCombosIndex();
            return View();
        }
    }
}