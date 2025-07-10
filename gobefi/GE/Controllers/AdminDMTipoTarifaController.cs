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

namespace GobEfi.Web.Controllers
{
    public class AdminDMTipoTarifaController : BaseController
    {
        public AdminDMTipoTarifaController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoTarifa
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoTarifas.ToListAsync());
        }

        // GET: AdminDMTipoTarifa/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTarifa = await context.TipoTarifas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTarifa == null)
            {
                return NotFound();
            }

            return View(tipoTarifa);
        }

        // GET: AdminDMTipoTarifa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoTarifa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoTarifa tipoTarifa)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoTarifa);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTarifa);
        }

        // GET: AdminDMTipoTarifa/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTarifa = await context.TipoTarifas.FindAsync(id);
            if (tipoTarifa == null)
            {
                return NotFound();
            }
            return View(tipoTarifa);
        }

        // POST: AdminDMTipoTarifa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] TipoTarifa tipoTarifa)
        {
            if (id != tipoTarifa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoTarifa);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTarifaExists(tipoTarifa.Id))
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
            return View(tipoTarifa);
        }

        // GET: AdminDMTipoTarifa/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTarifa = await context.TipoTarifas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTarifa == null)
            {
                return NotFound();
            }

            return View(tipoTarifa);
        }

        // POST: AdminDMTipoTarifa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoTarifa = await context.TipoTarifas.FindAsync(id);
            context.TipoTarifas.Remove(tipoTarifa);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTarifaExists(long id)
        {
            return context.TipoTarifas.Any(e => e.Id == id);
        }
    }
}
