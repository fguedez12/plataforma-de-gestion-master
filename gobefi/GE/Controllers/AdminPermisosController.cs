using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;

namespace GobEfi.Web.Controllers
{
    public class AdminPermisosController : BaseController
    {
        private readonly IPermisosRepository _repository;
        private readonly IPermisosService _service;

        public AdminPermisosController(
            ApplicationDbContext context,
            IPermisosRepository repository,
            IPermisosService service,
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _repository = repository;
            _service = service;
        }



        // GET: AdminPermisos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Permisos.Include(p => p.Menu).Include(p => p.Rol);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminPermisos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisos = await context.Permisos
                .Include(p => p.Menu)
                .Include(p => p.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permisos == null)
            {
                return NotFound();
            }

            return View(permisos);
        }

        // GET: AdminPermisos/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(context.Menu, "Id", "Id");
            ViewData["RolId"] = new SelectList(context.Rols, "Id", "Id");
            return View();
        }

        // POST: AdminPermisos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuId,SubMenuId,RolId,Lectura,Escritura")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                context.Add(permisos);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(context.Menu, "Id", "Id", permisos.MenuId);
            ViewData["RolId"] = new SelectList(context.Rols, "Id", "Id", permisos.RolId);
            return View(permisos);
        }

        // GET: AdminPermisos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisos = await context.Permisos.FindAsync(id);
            if (permisos == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(context.Menu, "Id", "Id", permisos.MenuId);
            ViewData["RolId"] = new SelectList(context.Rols, "Id", "Id", permisos.RolId);
            return View(permisos);
        }

        // POST: AdminPermisos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MenuId,SubMenuId,RolId,Lectura,Escritura")] Permisos permisos)
        {
            if (id != permisos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(permisos);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermisosExists(permisos.Id))
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
            ViewData["MenuId"] = new SelectList(context.Menu, "Id", "Id", permisos.MenuId);
            ViewData["RolId"] = new SelectList(context.Rols, "Id", "Id", permisos.RolId);
            return View(permisos);
        }

        // GET: AdminPermisos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permisos = await context.Permisos
                .Include(p => p.Menu)
                .Include(p => p.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permisos == null)
            {
                return NotFound();
            }

            return View(permisos);
        }

        // POST: AdminPermisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var permisos = await context.Permisos.FindAsync(id);
            context.Permisos.Remove(permisos);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermisosExists(long id)
        {
            return context.Permisos.Any(e => e.Id == id);
        }
    }
}
