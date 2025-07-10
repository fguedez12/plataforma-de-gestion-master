using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    public class MiUnidadV2Controller : BaseController
    {
        private readonly IMiUnidadService _miUnidadService;

        public MiUnidadV2Controller(
            ApplicationDbContext context,
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService usuarioService,
            IMiUnidadService miUnidadService
            ) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _miUnidadService = miUnidadService;
        }

        public async  Task<IActionResult> Index()
        {
            var model = _miUnidadService.GetNewModel();
            model.Instituciones = await _miUnidadService.SetInstituciones();
            

            return View(model);
        }
    }
}
