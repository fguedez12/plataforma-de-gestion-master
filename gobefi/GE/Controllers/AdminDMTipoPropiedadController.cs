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
    public class AdminDMTipoPropiedadController : BaseController
    {
        public AdminDMTipoPropiedadController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoPropiedad
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoPropiedades.ToListAsync());
        }

        // GET: AdminDMTipoPropiedad/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPropiedad = await context.TipoPropiedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPropiedad == null)
            {
                return NotFound();
            }

            return View(tipoPropiedad);
        }

        // GET: AdminDMTipoPropiedad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoPropiedad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoPropiedad tipoPropiedad)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoPropiedad);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPropiedad);
        }

        // GET: AdminDMTipoPropiedad/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPropiedad = await context.TipoPropiedades.FindAsync(id);
            if (tipoPropiedad == null)
            {
                return NotFound();
            }
            return View(tipoPropiedad);
        }

        // POST: AdminDMTipoPropiedad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] TipoPropiedad tipoPropiedad)
        {
            if (id != tipoPropiedad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoPropiedad);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPropiedadExists(tipoPropiedad.Id))
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
            return View(tipoPropiedad);
        }

        // GET: AdminDMTipoPropiedad/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPropiedad = await context.TipoPropiedades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPropiedad == null)
            {
                return NotFound();
            }

            return View(tipoPropiedad);
        }

        // POST: AdminDMTipoPropiedad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoPropiedad = await context.TipoPropiedades.FindAsync(id);
            context.TipoPropiedades.Remove(tipoPropiedad);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPropiedadExists(long id)
        {
            return context.TipoPropiedades.Any(e => e.Id == id);
        }
    }
}
