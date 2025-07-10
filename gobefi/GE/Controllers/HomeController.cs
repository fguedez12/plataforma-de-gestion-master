using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GobEfi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data;
using GobEfi.Web.Core.Contracts.Services;

namespace GobEfi.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            ApplicationDbContext context,
            UserManager<Usuario> manager, 
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
