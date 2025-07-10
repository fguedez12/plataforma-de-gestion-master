using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Controllers
{
    [Authorize(Roles = "ADMINISTRADOR")]
    public abstract class AdminBaseController : BaseController
    {
        public AdminBaseController(
            ApplicationDbContext context,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        { } 
    }
}