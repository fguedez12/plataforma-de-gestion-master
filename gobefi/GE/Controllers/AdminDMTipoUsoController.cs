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
    public class AdminDMTipoUsoController : BaseController
    {
        public AdminDMTipoUsoController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AminDMTipoUso
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoUsos.ToListAsync());
        }

        // GET: AminDMTipoUso/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUso = await context.TipoUsos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoUso == null)
            {
                return NotFound();
            }

            return View(tipoUso);
        }

        // GET: AminDMTipoUso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AminDMTipoUso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoUso tipoUso)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoUso);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoUso);
        }

        // GET: AminDMTipoUso/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUso = await context.TipoUsos.FindAsync(id);
            if (tipoUso == null)
            {
                return NotFound();
            }
            return View(tipoUso);
        }

        // POST: AminDMTipoUso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] TipoUso tipoUso)
        {
            if (id != tipoUso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoUso);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoUsoExists(tipoUso.Id))
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
            return View(tipoUso);
        }

        // GET: AminDMTipoUso/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoUso = await context.TipoUsos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoUso == null)
            {
                return NotFound();
            }

            return View(tipoUso);
        }

        // POST: AminDMTipoUso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoUso = await context.TipoUsos.FindAsync(id);
            context.TipoUsos.Remove(tipoUso);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoUsoExists(long id)
        {
            return context.TipoUsos.Any(e => e.Id == id);
        }
    }
}
