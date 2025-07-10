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
    public class AdminDMTipoTechoController : BaseController
    {
        public AdminDMTipoTechoController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoTecho
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoTechos.ToListAsync());
        }

        // GET: AdminDMTipoTecho/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTecho = await context.TipoTechos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTecho == null)
            {
                return NotFound();
            }

            return View(tipoTecho);
        }

        // GET: AdminDMTipoTecho/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoTecho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Factor,Id")] TipoTecho tipoTecho)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoTecho);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTecho);
        }

        // GET: AdminDMTipoTecho/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTecho = await context.TipoTechos.FindAsync(id);
            if (tipoTecho == null)
            {
                return NotFound();
            }
            return View(tipoTecho);
        }

        // POST: AdminDMTipoTecho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,Factor,Id")] TipoTecho tipoTecho)
        {
            if (id != tipoTecho.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoTecho);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTechoExists(tipoTecho.Id))
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
            return View(tipoTecho);
        }

        // GET: AdminDMTipoTecho/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTecho = await context.TipoTechos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTecho == null)
            {
                return NotFound();
            }

            return View(tipoTecho);
        }

        // POST: AdminDMTipoTecho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoTecho = await context.TipoTechos.FindAsync(id);
            context.TipoTechos.Remove(tipoTecho);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTechoExists(long id)
        {
            return context.TipoTechos.Any(e => e.Id == id);
        }
    }
}
