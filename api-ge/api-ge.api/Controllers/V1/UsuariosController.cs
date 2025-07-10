using api_gestiona.DTOs.UserDTO;
using api_gestiona.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
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
                try
                {
                    var identityUser = await _context.Usuarios
                        .Where(x => x.Email == userInfo.Email && x.Active == true)
                        .FirstOrDefaultAsync();
                    if (identityUser == null) {
                        return BadRequest("No existe el usuario");
                    }
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
                catch (Exception e)
                {

                    throw e;
                }
                
            }
            else
            {
                return BadRequest("No existe el usuario");
            }
        }
    }
}
