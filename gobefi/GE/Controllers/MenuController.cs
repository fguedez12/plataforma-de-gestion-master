using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase //TODO: Cambiar a BaseController
    {
        private readonly UserManager<Usuario> manager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMenuService _servMenu;

        public MenuController(UserManager<Usuario> manager, IHttpContextAccessor httpContextAccessor, IMenuService servMenu)
        {
            this.manager = manager;
            this.httpContextAccessor = httpContextAccessor;
            _servMenu = servMenu;
        }

        // GET: api/Menu/CrearMenu
        [HttpGet("CrearMenu")]
        public async Task<IActionResult> CrearMenu()
        {
            var usuario = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            var listaMenu = _servMenu.GetByUser(usuario.Id);

            return Ok(listaMenu);
        }
    }
}