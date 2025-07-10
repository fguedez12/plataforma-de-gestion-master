using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using GobEfi.Web.Controllers;
using GobEfi.Web.Core.Configuration;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Controllers
{
    public class ConsumoController : BaseController
    {
        private readonly ApiConfiguration _apiConfiguration;
        private readonly IDivisionService _divisionService;

        public ConsumoController(
            ApplicationDbContext context, 
            ApiConfiguration apiConfiguration,
            IUsuarioService servUsuario,UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService, IDivisionService divisionService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _apiConfiguration = apiConfiguration;
            _divisionService = divisionService;
        }

        public IActionResult Index(string id)
        {
            bool compraActiva = _divisionService.Get(long.Parse(id)).Servicio.CompraActiva;
            var permisos = GetPermisions();
            if (!compraActiva)
            {
                permisos.Escritura = false;
            }
            ViewData["permisos"] = permisos;
            ViewData["ajustes"] = this.ajustes;
            ViewBag.isAdmin = _isAdmin;
            ViewBag.isGestServ = _isGestServ;
            return View();
        }

        [HttpGet]
        public ActionResult GetConfigurationValue()
        {
            return Json(new { _apiConfiguration });
        }

        public ActionResult CargaMasiva()
        {
            return View("CargaMasiva");
        }
    }
}