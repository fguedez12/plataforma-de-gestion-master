using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Core.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using GobEfi.Web.Services.Request;
using GobEfi.Web.Models;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Helper;

namespace GobEfi.Web.Controllers
{
    public class AdminServicioController : BaseController //AdminBaseController
    {
        private readonly IServicioService _service;
        private readonly IInstitucionService _servInstitucion;
        private readonly ILogger _logger;

        public AdminServicioController(
            ApplicationDbContext context,
            IServicioService service,
            IInstitucionService servInstitucion,
            ILoggerFactory factory, 
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _service = service;
            _servInstitucion = servInstitucion;
            _logger = factory.CreateLogger<AdminServicioController>();
        }

        private void ObtenerData()
        {
            ViewBag.Instituciones = SelectHelper.LlenarDDL(_servInstitucion.All(),true);
        }

        // GET: AdminServicio
        public async Task<IActionResult> Index(ServicioRequest request)
        {
            _logger.LogInformation("Admin Servicios Index");
            ViewData["permisos"] = GetPermisions();

            var indexModel = new ServicioIndexModel();
            indexModel.Items = _service.Paged(request);
            indexModel.Sort = SortItems();

            ObtenerData();

            return View(indexModel);
        }

        // GET: AdminServicio/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioModel = _service.Get(id);
            if (servicioModel == null)
            {
                return NotFound();
            }
            
            servicioModel.CreatedBy = NombreCompletoUsuario(servicioModel.CreatedBy);
            servicioModel.ModifiedBy = NombreCompletoUsuario(servicioModel.ModifiedBy);

            return View(servicioModel);
        }

        // GET: AdminServicio/Create
        public IActionResult Create()
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            ObtenerData();
            return View();
        }

        // POST: AdminServicio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Identificador,ReportaPMG,InstitucionId")] ServicioModel servicioModel)
        {
            if (ModelState.IsValid)
            {
                _service.Insert(servicioModel);
                return RedirectToAction(nameof(Index));
            }
            return View(servicioModel);
        }

        // GET: AdminServicio/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var servicioModel = _service.Get(id);

            servicioModel.CreatedBy = NombreCompletoUsuario(servicioModel.CreatedBy);
            servicioModel.ModifiedBy = NombreCompletoUsuario(servicioModel.ModifiedBy);
            ObtenerData();

            if (servicioModel == null)
            {
                return NotFound();
            }
            return View(servicioModel);
        }

        // POST: AdminServicio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Identificador,ReportaPMG,InstitucionId,PgaRevisionRed,RevisionRed,ValidacionConcientizacion")] ServicioModel servicioModel)
        {
            if (id != servicioModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(servicioModel, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioModelExists(servicioModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servicioModel);
        }

        // GET: AdminServicio/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioModel = _service.Get(id);
            if (servicioModel == null)
            {
                return NotFound();
            }

            servicioModel.CreatedBy = NombreCompletoUsuario(servicioModel.CreatedBy);
            servicioModel.ModifiedBy = NombreCompletoUsuario(servicioModel.ModifiedBy);

            return View(servicioModel);
        }

        // POST: AdminServicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _service.Delete(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioModelExists(long id)
        {
            return (_service.Get(id) == null) ? false : true;
        }

        private SortModel SortItems()
        {
            var order = new SortModel();
            order.AddSortField("Id", "id");
            order.AddSortField("Nombre", "nombre");
            return order;
        }
    }
}
