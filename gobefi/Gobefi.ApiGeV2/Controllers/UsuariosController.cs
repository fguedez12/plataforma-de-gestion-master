using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gobefi.ApiGeV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _context;
        public UsuariosController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("validar")]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponseDTO>> validar(UserInfoDTO userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
               userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var identityUser = await _userManager.FindByEmailAsync(userInfo.Email);
                var roles = await _userManager.GetRolesAsync(identityUser);
                var servicio = await _context.UsuariosServicios.FirstOrDefaultAsync(x => x.UsuarioId == identityUser.Id);
                var userResponse = new UserResponseDTO
                {
                    Id = identityUser.Id,
                    Email = identityUser.Email,
                    Role = roles.FirstOrDefault(),
                    Nombre = $"{identityUser.Nombres.ToUpper()} {identityUser.Apellidos.ToUpper()}",
                    Sexo = identityUser.SexoId == 1 ? "Hombre" : "Mujer",
                    ServicioId = servicio == null ? 0 : servicio.ServicioId
                };
                return userResponse;
            }
            else
            {
                return BadRequest("No existe el usuario");
            }
        }
    }


}
