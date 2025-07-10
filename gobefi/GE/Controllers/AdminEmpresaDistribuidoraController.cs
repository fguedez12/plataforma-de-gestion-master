using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Helper;
using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Services.Request;

namespace GobEfi.Web.Controllers
{
    public class AdminEmpresaDistribuidoraController : BaseController
    {
        private readonly IEmpresaDistribuidoraService _servEmpresaDistribuidora;
        private readonly IComunaService _servComuna;
        private readonly IProvinciaService _servProvincia;
        private readonly IRegionService _servRegion;
        private readonly IEnergeticoService _servEnergetico;

        public AdminEmpresaDistribuidoraController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService servUsuario,
            IEmpresaDistribuidoraService servEmpresaDistribuidora,
            IComunaService servComuna,
            IProvinciaService servProvincia,
            IRegionService servRegion,
            IEnergeticoService servEnergetico) : base(context, manager, httpContextAccessor, servUsuario)
        {
            _servEmpresaDistribuidora = servEmpresaDistribuidora;
            _servComuna = servComuna;
            _servProvincia = servProvincia;
            _servRegion = servRegion;
            _servEnergetico = servEnergetico;
        }

        private void LlenarDropDown()
        {
            // ViewBag.Energeticos = new SelectList(context.Energeticos, "Id", "Nombre");
            ViewBag.Energeticos = SelectHelper.LlenarDDL(_servEnergetico.All(), true);


            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);
            ViewBag.Provincias = SelectHelper.LlenarDDL(_servProvincia.All(), true);
            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);

        }

        // GET: AdminEmpresaDistribuidora
        public async Task<IActionResult> Index(EmpresaDistribuidoraRequest request)
        {
            // _logger.LogInformation("Admin Empresa distribuidora Index");
            ViewData["permisos"] = GetPermisions();

            var indexModel = new EmpresaDistribuidoraIndexModel();
            indexModel.Items = _servEmpresaDistribuidora.Paged(request);
            indexModel.Sort = SortItems();

            LlenarDropDown();

            return View(indexModel);
        }

        // GET: AdminEmpresaDistribuidora/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaDistribuidora = await context.EmpresaDistribuidoras
                .Include(e => e.Energetico)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaDistribuidora == null)
            {
                return NotFound();
            }

            return View(empresaDistribuidora);
        }

        // GET: AdminEmpresaDistribuidora/Create
        public IActionResult Create()
        {
            ViewBag.ComunasNoAsociadas = SelectHelper.LlenarDDL(_servComuna.All());

            LlenarDropDown();
            return View();
        }

        // POST: AdminEmpresaDistribuidora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,EnergeticoId,Comunas")] EmpresaDistribuidoraModel empresaDistribuidora)
        {
            if (!ModelState.IsValid)
            {
                return View(empresaDistribuidora);
            }


            _servEmpresaDistribuidora.Insert(empresaDistribuidora);
            return RedirectToAction(nameof(Index));

            //context.Add(empresaDistribuidora);
            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));


        }

        // GET: AdminEmpresaDistribuidora/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            //var empresaDistribuidora = await context.EmpresaDistribuidoras.FindAsync(id);
            var empresaDistribuidora = _servEmpresaDistribuidora.Get(id);


            if (empresaDistribuidora == null)
            {
                return NotFound();
            }

            ViewBag.ComunasAsociadas = SelectHelper.LlenarDDL(_servComuna.GetByEmpresaDistribuidora(id));
            ViewBag.ComunasNoAsociadas = SelectHelper.LlenarDDL(_servComuna.GetByEmpresaDistribuidoraNoAsociadas(id));


            LlenarDropDown();

            return View(empresaDistribuidora);
        }

        // POST: AdminEmpresaDistribuidora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,EnergeticoId,Comunas")] EmpresaDistribuidoraModel empresaDistribuidora)
        {
            if (id != empresaDistribuidora.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                //ViewData["EnergeticoId"] = new SelectList(context.Energeticos, "Id", "Id", empresaDistribuidora.EnergeticoId);
                return BadRequest(empresaDistribuidora);
            }
            

            try
            {
                _servEmpresaDistribuidora.Update(empresaDistribuidora);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaDistribuidoraExists(empresaDistribuidora.Id))
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

        // GET: AdminEmpresaDistribuidora/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            //var empresaDistribuidora = await context.EmpresaDistribuidoras
            //    .Include(e => e.Energetico)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var empresaDistribuidora = _servEmpresaDistribuidora.Get(id);

            if (empresaDistribuidora == null)
            {
                return NotFound();
            }
            ViewBag.ComunasAsociadas = SelectHelper.LlenarDDL(_servComuna.GetByEmpresaDistribuidora(id));
            return View(empresaDistribuidora);
        }

        // POST: AdminEmpresaDistribuidora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _servEmpresaDistribuidora.Delete(id);

            return RedirectToAction(nameof(Index));

            //var empresaDistribuidora = await context.EmpresaDistribuidoras.FindAsync(id);
            //context.EmpresaDistribuidoras.Remove(empresaDistribuidora);
            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
        }

        private bool EmpresaDistribuidoraExists(long id)
        {
            return context.EmpresaDistribuidoras.Any(e => e.Id == id);
        }
    }
}
