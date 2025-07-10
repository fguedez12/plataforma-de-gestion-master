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
    public class AdminDMTipoEdificioController : BaseController
    {
        public AdminDMTipoEdificioController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminTipoEdificio
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoEdificios.ToListAsync());
        }

        // GET: AdminTipoEdificio/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEdificio = await context.TipoEdificios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEdificio == null)
            {
                return NotFound();
            }

            return View(tipoEdificio);
        }

        // GET: AdminTipoEdificio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminTipoEdificio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,OldId")] TipoEdificio tipoEdificio)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoEdificio);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEdificio);
        }

        // GET: AdminTipoEdificio/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEdificio = await context.TipoEdificios.FindAsync(id);
            if (tipoEdificio == null)
            {
                return NotFound();
            }
            return View(tipoEdificio);
        }

        // POST: AdminTipoEdificio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,OldId")] TipoEdificio tipoEdificio)
        {
            if (id != tipoEdificio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoEdificio);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEdificioExists(tipoEdificio.Id))
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
            return View(tipoEdificio);
        }

        // GET: AdminTipoEdificio/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEdificio = await context.TipoEdificios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEdificio == null)
            {
                return NotFound();
            }

            return View(tipoEdificio);
        }

        // POST: AdminTipoEdificio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoEdificio = await context.TipoEdificios.FindAsync(id);
            context.TipoEdificios.Remove(tipoEdificio);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEdificioExists(long id)
        {
            return context.TipoEdificios.Any(e => e.Id == id);
        }
    }
}
