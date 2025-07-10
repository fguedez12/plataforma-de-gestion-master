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
    public class AdminDMTipoAislacionController : BaseController
    {
        public AdminDMTipoAislacionController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoAislacion
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoAislaciones.ToListAsync());
        }

        // GET: AdminDMTipoAislacion/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAislacion = await context.TipoAislaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAislacion == null)
            {
                return NotFound();
            }

            return View(tipoAislacion);
        }

        // GET: AdminDMTipoAislacion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoAislacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoAislacion tipoAislacion)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoAislacion);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAislacion);
        }

        // GET: AdminDMTipoAislacion/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAislacion = await context.TipoAislaciones.FindAsync(id);
            if (tipoAislacion == null)
            {
                return NotFound();
            }
            return View(tipoAislacion);
        }

        // POST: AdminDMTipoAislacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] TipoAislacion tipoAislacion)
        {
            if (id != tipoAislacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoAislacion);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAislacionExists(tipoAislacion.Id))
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
            return View(tipoAislacion);
        }

        // GET: AdminDMTipoAislacion/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAislacion = await context.TipoAislaciones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoAislacion == null)
            {
                return NotFound();
            }

            return View(tipoAislacion);
        }

        // POST: AdminDMTipoAislacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoAislacion = await context.TipoAislaciones.FindAsync(id);
            context.TipoAislaciones.Remove(tipoAislacion);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAislacionExists(long id)
        {
            return context.TipoAislaciones.Any(e => e.Id == id);
        }
    }
}
