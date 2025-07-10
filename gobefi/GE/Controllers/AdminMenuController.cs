using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Data;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GobEfi.Web.Controllers
{
    public class AdminMenuController : BaseController
    {
        public AdminMenuController(
            ApplicationDbContext context, 
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor, 
            IUsuarioService service) : base(context, manager, httpContextAccessor, service)
        {
        }


        // GET: AdminMenu
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Menu.Include(m => m.MenuPanel);
            ViewData["permisos"] = permisos;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminMenu/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await context.Menu
                .Include(m => m.MenuPanel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: AdminMenu/Create
        public IActionResult Create()
        {
            ViewData["MenuPanelId"] = new SelectList(context.MenuPanel, "Id", "Id");
            return View();
        }

        // POST: AdminMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MenuPanelId,Nombre,Url,Icono,IdTag,ControllerName,ActionName,Title,Orden")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                context.Add(menu);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuPanelId"] = new SelectList(context.MenuPanel, "Id", "Id", menu.MenuPanelId);
            return View(menu);
        }

        // GET: AdminMenu/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            ViewData["MenuPanelId"] = new SelectList(context.MenuPanel, "Id", "Id", menu.MenuPanelId);
            return View(menu);
        }

        // POST: AdminMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MenuPanelId,Nombre,Url,Icono,IdTag,ControllerName,ActionName,Title,Orden")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(menu);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            ViewData["MenuPanelId"] = new SelectList(context.MenuPanel, "Id", "Id", menu.MenuPanelId);
            return View(menu);
        }

        // GET: AdminMenu/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await context.Menu
                .Include(m => m.MenuPanel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: AdminMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var menu = await context.Menu.FindAsync(id);
            context.Menu.Remove(menu);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(long id)
        {
            return context.Menu.Any(e => e.Id == id);
        }
    }
}
