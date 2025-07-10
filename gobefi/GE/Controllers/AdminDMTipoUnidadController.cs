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
    public class AdminDMTipoUnidadController : BaseController
    {
        public AdminDMTipoUnidadController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoUnidad
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoUnidades.ToListAsync());
        }

        // GET: AdminDMTipoUnidad/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUnidad = await context.TipoUnidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoUnidad == null)
            {
                return NotFound();
            }

            return View(tipoUnidad);
        }

        // GET: AdminDMTipoUnidad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoUnidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Id,OldId")] TipoUnidad tipoUnidad)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoUnidad);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoUnidad);
        }

        // GET: AdminDMTipoUnidad/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUnidad = await context.TipoUnidades.FindAsync(id);
            if (tipoUnidad == null)
            {
                return NotFound();
            }
            return View(tipoUnidad);
        }

        // POST: AdminDMTipoUnidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,Id,OldId")] TipoUnidad tipoUnidad)
        {
            if (id != tipoUnidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoUnidad);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUnidadExists(tipoUnidad.Id))
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
            return View(tipoUnidad);
        }

        // GET: AdminDMTipoUnidad/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUnidad = await context.TipoUnidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoUnidad == null)
            {
                return NotFound();
            }

            return View(tipoUnidad);
        }

        // POST: AdminDMTipoUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoUnidad = await context.TipoUnidades.FindAsync(id);
            context.TipoUnidades.Remove(tipoUnidad);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoUnidadExists(long id)
        {
            return context.TipoUnidades.Any(e => e.Id == id);
        }
    }
}
