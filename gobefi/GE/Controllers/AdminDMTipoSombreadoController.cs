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
    public class AdminDMTipoSombreadoController : BaseController
    {
        public AdminDMTipoSombreadoController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoSombreado
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoSombreados.ToListAsync());
        }

        // GET: AdminDMTipoSombreado/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSombreado = await context.TipoSombreados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSombreado == null)
            {
                return NotFound();
            }

            return View(tipoSombreado);
        }

        // GET: AdminDMTipoSombreado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoSombreado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,FactorSombra,Id")] TipoSombreado tipoSombreado)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoSombreado);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoSombreado);
        }

        // GET: AdminDMTipoSombreado/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSombreado = await context.TipoSombreados.FindAsync(id);
            if (tipoSombreado == null)
            {
                return NotFound();
            }
            return View(tipoSombreado);
        }

        // POST: AdminDMTipoSombreado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,FactorSombra,Id")] TipoSombreado tipoSombreado)
        {
            if (id != tipoSombreado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoSombreado);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoSombreadoExists(tipoSombreado.Id))
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
            return View(tipoSombreado);
        }

        // GET: AdminDMTipoSombreado/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoSombreado = await context.TipoSombreados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoSombreado == null)
            {
                return NotFound();
            }

            return View(tipoSombreado);
        }

        // POST: AdminDMTipoSombreado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoSombreado = await context.TipoSombreados.FindAsync(id);
            context.TipoSombreados.Remove(tipoSombreado);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoSombreadoExists(long id)
        {
            return context.TipoSombreados.Any(e => e.Id == id);
        }
    }
}
