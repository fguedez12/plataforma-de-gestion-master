using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Core.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using GobEfi.Web.Services.Request;
using GobEfi.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Data.Entities;

namespace GobEfi.Web.Controllers
{
    public class AdminInstitucionController : BaseController
    {
        private readonly IInstitucionService _service;
        private readonly IInstitucionRepository _repository;
        private readonly UserManager<Usuario> _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public AdminInstitucionController(
            ApplicationDbContext context,
            IInstitucionService service,
            IInstitucionRepository repository,
            ILoggerFactory factory,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _service = service;
            _repository = repository;
            _logger = factory.CreateLogger<AdminInstitucionController>();
        }

        // GET: AdminInstitucion
        public async Task<IActionResult> Index(InstitucionRequest request)
        {
            _logger.LogInformation("Admin Instituciones");

            ViewData["permisos"] = GetPermisions();

            var indexModel = new InstitucionIndexModel();
            indexModel.Items = _service.Paged(request);
            indexModel.Sort = SortItem();
            return View(indexModel);
        }
        
        // GET: AdminInstitucion/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var institucionModel = _service.Get(id);
            if (institucionModel == null)
            {
                return NotFound();
            }

            return View(institucionModel);
        }

        // GET: AdminInstitucion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminInstitucion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] InstitucionModel institucionModel)
        {
            if (ModelState.IsValid)
            {
                _service.Insert(institucionModel);
                return RedirectToAction(nameof(Index));
            }
            return View(institucionModel);
        }

        // GET: AdminInstitucion/Edit/5
        public IActionResult Edit(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var institucionModel = _service.Get(id);
            if (institucionModel == null)
            {
                return NotFound();
            }

            
            institucionModel.ModifiedBy = NombreCompletoUsuario(institucionModel.ModifiedBy);
            institucionModel.CreatedBy = NombreCompletoUsuario(institucionModel.CreatedBy);

            return View(institucionModel);
        }

        // POST: AdminInstitucion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,CreatedAt,UpdatedAt,Version,Active,ModifiedBy,CreatedBy")] InstitucionModel institucionModel)
        {
            if (id != institucionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_service.Get(id);
                    _service.Update(institucionModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitucionModelExists(institucionModel.Id))
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
            return View(institucionModel);
        }

        // GET: AdminInstitucion/Delete/5
        public IActionResult Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var institucionModel = _service.Get(id);
            if (institucionModel == null)
            {
                return NotFound();
            }

            institucionModel.ModifiedBy = NombreCompletoUsuario(institucionModel.ModifiedBy);
            institucionModel.CreatedBy = NombreCompletoUsuario(institucionModel.CreatedBy);

            return View(institucionModel);
        }

        // POST: AdminInstitucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucionModelExists(long id)
        {
            return (_service.Get(id) == null) ? false : true;
        }

        private SortModel SortItem()
        {
            var order = new SortModel();
            order.AddSortField("Id", "id");
            order.AddSortField("Nombre", "nombre");
            return order;
        }
    }
}
