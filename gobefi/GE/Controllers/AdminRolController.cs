using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.RolModels;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace GobEfi.Web.Controllers
{
    public class AdminRolController : BaseController
    {
        private readonly IRolRepository _repository;
        private readonly IRolService _servRol;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AdminRolController(
            ApplicationDbContext context,
            IRolRepository repository,
            IRolService service,
            ILoggerFactory factory,
            IMapper mapper, 
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _repository = repository;
            _servRol = service;
            _logger = factory.CreateLogger<AdminRolController>();
            _mapper = mapper;
        }

        // GET: RolModels
        public async Task<IActionResult> Index()
        {
            ViewData["permisos"] = GetPermisions();

            return View(_servRol.All());
        }

        public async Task<IActionResult> GetRoles(string id)
        {
            return Json(_servRol.AllByUser(id));
        }

        // GET: RolModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolModel = _servRol.Get(id);
            if (rolModel == null)
            {
                return NotFound();
            }

            return View(rolModel);
        }

        // GET: RolModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RolModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] RolModel rolModel)
        {
            if (!ModelState.IsValid)
            {
                return View(rolModel);
            }

            _servRol.Insert(rolModel);

            return RedirectToAction(nameof(Index));

            //context.Add(rolModel);
            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            
        }

        // GET: RolModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolModel = _servRol.Get(id); ;
            if (rolModel == null)
            {
                return NotFound();
            }
            return View(rolModel);
        }

        // POST: RolModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Id")] RolModel rolModel)
        {
            if (id != rolModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _servRol.Update(rolModel);
                    //context.Update(rolModel);
                    //await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!RolModelExists(rolModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    if (!RolModelExists(rolModel.Id))
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
            return View(rolModel);
        }

        // GET: RolModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolModel = _servRol.Get(id); ;
            if (rolModel == null)
            {
                return NotFound();
            }

            return View(rolModel);
        }

        // POST: RolModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _servRol.Delete(id);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolModelExists(string id)
        {
            return (_servRol.Get(id) == null) ? false : true;
        }
    }
}
