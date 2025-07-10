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
    public class AdminUnidadMedidaController : BaseController
    {
        private readonly IUnidadMedidaService _servUnidadMedida;

        public AdminUnidadMedidaController(
            IUnidadMedidaService servUnidadMedida,
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService servUsuario) : base(context, manager, httpContextAccessor, servUsuario)
        {
            _servUnidadMedida = servUnidadMedida;
        }
        

        // GET: AdminUnidadMedida
        public async Task<IActionResult> Index()
        {
            ViewData["permisos"] = this.permisos;

            return View(await context.UnidadesMedida.ToListAsync());
        }

        // GET: AdminUnidadMedida/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadMedida = await context.UnidadesMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadMedida == null)
            {
                return NotFound();
            }

            return View(unidadMedida);
        }

        // GET: AdminUnidadMedida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminUnidadMedida/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Abrv")] UnidadMedida unidadMedida)
        {
            if (ModelState.IsValid)
            {
                context.Add(unidadMedida);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadMedida);
        }

        // GET: AdminUnidadMedida/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var unidadMedida = await context.UnidadesMedida.FindAsync(id);

            if (unidadMedida == null)
            {
                return NotFound();
            }
            return View(unidadMedida);
        }

        // POST: AdminUnidadMedida/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Abrv")] UnidadMedida unidadMedida)
        {
            if (id != unidadMedida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(unidadMedida);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadMedidaExists(unidadMedida.Id))
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
            return View(unidadMedida);
        }

        // GET: AdminUnidadMedida/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadMedida = await context.UnidadesMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadMedida == null)
            {
                return NotFound();
            }

            return View(unidadMedida);
        }

        // POST: AdminUnidadMedida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var unidadMedida = await context.UnidadesMedida.FindAsync(id);
            context.UnidadesMedida.Remove(unidadMedida);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadMedidaExists(long id)
        {
            return context.UnidadesMedida.Any(e => e.Id == id);
        }
    }
}
