using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.Extensions.Logging;
using GobEfi.Web.Core.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace GobEfi.Web.Controllers
{
    public class AdminEnergeticoController : BaseController
    {
        private readonly IEnergeticoService _energeticoService;
        private readonly IEnergeticoRepository _repository;
        private readonly UserManager<Usuario> _manager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public AdminEnergeticoController(
            ApplicationDbContext context,
            ILoggerFactory logger,
            IEnergeticoService energeticoService,
            IEnergeticoRepository energeticoRepository,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _logger = logger.CreateLogger<AdminEnergeticoController>();
            _energeticoService = energeticoService;
            _repository = energeticoRepository;
        }

        // GET: AdminEnergetico
        public async Task<IActionResult> Index()
        {
            ViewData["permisos"] = permisos;
            return View(_energeticoService.All());
        }

        // GET: AdminEnergetico/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energeticoModel = _energeticoService.Get(id);
            if (energeticoModel == null)
            {
                return NotFound();
            }

            return View(energeticoModel);
        }

        // GET: AdminEnergetico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminEnergetico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Icono,Nombre,Posicion,PermiteMedidor,Activo,Id")] EnergeticoModel energeticoModel)
        {
            if (ModelState.IsValid)
            {
                await _energeticoService.InsertAsync(energeticoModel);
                return RedirectToAction(nameof(Index));
            }
            return View(energeticoModel);
        }

        // GET: AdminEnergetico/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EnergeticoForEditModel energeticoModel = await _energeticoService.GetForEdit(id);

            if (energeticoModel == null)
            {
                return NotFound();
            }
            return View(energeticoModel);
        }

        // POST: AdminEnergetico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,PermiteMedidor,Activo,Id,UnidadesDeMedidasId")] EnergeticoForEditModel energeticoModel)
        {
            if (id != energeticoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _energeticoService.UpdateAsync(energeticoModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnergeticoModelExists(energeticoModel.Id))
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
            return View(energeticoModel);
        }

        // GET: AdminEnergetico/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var energeticoModel = _energeticoService.Get(id);
            if (energeticoModel == null)
            {
                return NotFound();
            }

            return View(energeticoModel);
        }

        // POST: AdminEnergetico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _energeticoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EnergeticoModelExists(long id)
        {
            return (_energeticoService.Get(id) == null) ? false : true;
        }
    }
}
