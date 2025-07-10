using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Models.ReporteModels;

namespace GobEfi.Web.Controllers
{
    public class AdminReportesController : BaseController
    {
        private readonly IReporteService _servReporte;

        public AdminReportesController(
            IReporteService servReporte,
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
            _servReporte = servReporte;
        }


        // GET: AdminReportes
        public async Task<IActionResult> Index()
        {
            ViewData["permisos"] = GetPermisions();


            return View(await context.Reportes.ToListAsync());
        }

        // GET: AdminReportes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await context.Reportes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        // GET: AdminReportes/Create
        public IActionResult Create()
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            return View();
        }

        // POST: AdminReportes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] ReporteModel reporte)
        {
            if (ModelState.IsValid)
            {
                await _servReporte.InsertAsync(reporte);
                return RedirectToAction(nameof(Index));

                //context.Add(reporte);
                //await context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(reporte);
        }

        // GET: AdminReportes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var reporte = await context.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }
            return View(reporte);
        }

        // POST: AdminReportes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre")] ReporteModel reporte)
        {
            if (id != reporte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _servReporte.UpdateAsync(reporte);

                    //context.Update(reporte);
                    //await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteExists(reporte.Id))
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
            return View(reporte);
        }

        // GET: AdminReportes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await context.Reportes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        // POST: AdminReportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var reporte = await context.Reportes.FindAsync(id);
            context.Reportes.Remove(reporte);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReporteExists(long id)
        {
            return context.Reportes.Any(e => e.Id == id);
        }
    }
}
