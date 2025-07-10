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
    public class AdminDMTipoAgrupamientoController : BaseController
    {
        public AdminDMTipoAgrupamientoController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoAgrupamiento
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoAgrupamientos.ToListAsync());
        }

        // GET: AdminDMTipoAgrupamiento/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAgrupamiento = await context.TipoAgrupamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAgrupamiento == null)
            {
                return NotFound();
            }

            return View(tipoAgrupamiento);
        }

        // GET: AdminDMTipoAgrupamiento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoAgrupamiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoAgrupamiento tipoAgrupamiento)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoAgrupamiento);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAgrupamiento);
        }

        // GET: AdminDMTipoAgrupamiento/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAgrupamiento = await context.TipoAgrupamientos.FindAsync(id);
            if (tipoAgrupamiento == null)
            {
                return NotFound();
            }
            return View(tipoAgrupamiento);
        }

        // POST: AdminDMTipoAgrupamiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] TipoAgrupamiento tipoAgrupamiento)
        {
            if (id != tipoAgrupamiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoAgrupamiento);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAgrupamientoExists(tipoAgrupamiento.Id))
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
            return View(tipoAgrupamiento);
        }

        // GET: AdminDMTipoAgrupamiento/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAgrupamiento = await context.TipoAgrupamientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAgrupamiento == null)
            {
                return NotFound();
            }

            return View(tipoAgrupamiento);
        }

        // POST: AdminDMTipoAgrupamiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoAgrupamiento = await context.TipoAgrupamientos.FindAsync(id);
            context.TipoAgrupamientos.Remove(tipoAgrupamiento);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAgrupamientoExists(long id)
        {
            return context.TipoAgrupamientos.Any(e => e.Id == id);
        }
    }
}
