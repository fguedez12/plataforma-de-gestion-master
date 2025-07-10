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
    public class AdminDMTipoTecnologiaController : BaseController
    {
        public AdminDMTipoTecnologiaController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }

        // GET: AdminDMTipoTecnologia
        public async Task<IActionResult> Index()
        {
            return View(await context.TipoTecnologias.ToListAsync());
        }

        // GET: AdminDMTipoTecnologia/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTecnologia = await context.TipoTecnologias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTecnologia == null)
            {
                return NotFound();
            }

            return View(tipoTecnologia);
        }

        // GET: AdminDMTipoTecnologia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDMTipoTecnologia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Id")] TipoTecnologia tipoTecnologia)
        {
            if (ModelState.IsValid)
            {
                context.Add(tipoTecnologia);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoTecnologia);
        }

        // GET: AdminDMTipoTecnologia/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTecnologia = await context.TipoTecnologias.FindAsync(id);
            if (tipoTecnologia == null)
            {
                return NotFound();
            }
            return View(tipoTecnologia);
        }

        // POST: AdminDMTipoTecnologia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Nombre,Id")] TipoTecnologia tipoTecnologia)
        {
            if (id != tipoTecnologia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(tipoTecnologia);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoTecnologiaExists(tipoTecnologia.Id))
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
            return View(tipoTecnologia);
        }

        // GET: AdminDMTipoTecnologia/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoTecnologia = await context.TipoTecnologias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoTecnologia == null)
            {
                return NotFound();
            }

            return View(tipoTecnologia);
        }

        // POST: AdminDMTipoTecnologia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tipoTecnologia = await context.TipoTecnologias.FindAsync(id);
            context.TipoTecnologias.Remove(tipoTecnologia);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoTecnologiaExists(long id)
        {
            return context.TipoTecnologias.Any(e => e.Id == id);
        }
    }
}
