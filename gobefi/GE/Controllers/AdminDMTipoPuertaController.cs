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
    public class AdminDMTipoPuertaController : BaseController
    {
        public AdminDMTipoPuertaController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoPuerta
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoPuertas.ToListAsync());
        }

        // GET: AdminDMTipoPuerta/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPuerta = await context.TipoPuertas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPuerta == null)
            {
                return NotFound();
            }

            return View(tipoPuerta);
        }

        // GET: AdminDMTipoPuerta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoPuerta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Factor,Id")] TipoPuerta tipoPuerta)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoPuerta);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPuerta);
        }

        // GET: AdminDMTipoPuerta/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPuerta = await context.TipoPuertas.FindAsync(id);
            if (tipoPuerta == null)
            {
                return NotFound();
            }
            return View(tipoPuerta);
        }

        // POST: AdminDMTipoPuerta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,Factor,Id")] TipoPuerta tipoPuerta)
        {
            if (id != tipoPuerta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoPuerta);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPuertaExists(tipoPuerta.Id))
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
            return View(tipoPuerta);
        }

        // GET: AdminDMTipoPuerta/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPuerta = await context.TipoPuertas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPuerta == null)
            {
                return NotFound();
            }

            return View(tipoPuerta);
        }

        // POST: AdminDMTipoPuerta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoPuerta = await context.TipoPuertas.FindAsync(id);
            context.TipoPuertas.Remove(tipoPuerta);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPuertaExists(long id)
        {
            return context.TipoPuertas.Any(e => e.Id == id);
        }
    }
}
