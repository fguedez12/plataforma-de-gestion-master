using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GE.Core.Contracts.Services;
using GobEfi.Web.Controllers;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class MisReportesController : BaseController
    {
        private readonly IReporteService _servReporte;

        public MisReportesController(ApplicationDbContext context, 
        UserManager<Usuario> manager, 
        IHttpContextAccessor httpContextAccessor,
        IUsuarioService service,
        IReporteService servReporte) : base(context, manager, httpContextAccessor, service)
        {
            _servReporte = servReporte;
        }

        public IActionResult Index(string id)
        {
            //var usuarioActual = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            //ViewData["reportes"] = _servReporte.GetByUser(usuarioActual.Id, id);
            //_servReporte.DisenioPasivoReporte();

            return View();
        }
    }
}