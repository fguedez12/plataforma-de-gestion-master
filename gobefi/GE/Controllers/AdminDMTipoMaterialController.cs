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
    public class AdminDMTipoMaterialController : BaseController
    {
        public AdminDMTipoMaterialController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoMaterial
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoMateriales.ToListAsync());
        }

        // GET: AdminDMTipoMaterial/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMaterial = await context.TipoMateriales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoMaterial == null)
            {
                return NotFound();
            }

            return View(tipoMaterial);
        }

        // GET: AdminDMTipoMaterial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoMaterial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] TipoMaterial tipoMaterial)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoMaterial);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMaterial);
        }

        // GET: AdminDMTipoMaterial/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMaterial = await context.TipoMateriales.FindAsync(id);
            if (tipoMaterial == null)
            {
                return NotFound();
            }
            return View(tipoMaterial);
        }

        // POST: AdminDMTipoMaterial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] TipoMaterial tipoMaterial)
        {
            if (id != tipoMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoMaterial);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMaterialExists(tipoMaterial.Id))
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
            return View(tipoMaterial);
        }

        // GET: AdminDMTipoMaterial/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMaterial = await context.TipoMateriales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoMaterial == null)
            {
                return NotFound();
            }

            return View(tipoMaterial);
        }

        // POST: AdminDMTipoMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoMaterial = await context.TipoMateriales.FindAsync(id);
            context.TipoMateriales.Remove(tipoMaterial);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMaterialExists(long id)
        {
            return context.TipoMateriales.Any(e => e.Id == id);
        }
    }
}
