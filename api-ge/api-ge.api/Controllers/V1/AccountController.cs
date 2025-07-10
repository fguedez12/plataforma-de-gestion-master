using api_gestiona.DTOs.Account;
using api_gestiona.DTOs.UserDTO;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_gestiona.Controllers.V1
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : CustomController
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _manager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServicioService _servicioService;
        private readonly IInstitucionService _institucionService;

        public AccountController(SignInManager<Usuario> signInManager,
            IConfiguration configuration,
            UserManager<Usuario> manager,
            ApplicationDbContext context,
            IMapper mapper,
            IServicioService servicioService,
            IInstitucionService institucionService) : base(mapper, context, manager, configuration)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _manager = manager;
            _context = context;
            _mapper = mapper;
            _servicioService = servicioService;
            _institucionService = institucionService;
        }


        [HttpGet("user-id")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<long> GetUserServiceId() 
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            
            return Ok(userId);
        }


        [HttpPost("login")]
        public async Task<ActionResult<AutenticationResponse>> Login(UserCredentials userCredentials)
        {

            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);


            if (result.Succeeded)
            {
                //var user = await _manager.FindByEmailAsync(userCredentials.Email);
                var user = await _context.Usuarios.Where(x => x.Email == userCredentials.Email && x.Active == true).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                userCredentials.UserId = user.Id;
                return BuildToken(userCredentials);
            }
            else
            {
                return BadRequest("Login failed");
            }
        }

        [HttpPost("login-ppfv")]
        public async Task<ActionResult<LoginPpfvResponseDTO>> LoginPPFV(UserCredentials userCredentials)
        {

            var result = await _signInManager.PasswordSignInAsync(userCredentials.Email,
                userCredentials.Password, isPersistent: false, lockoutOnFailure: false);



            var response = new LoginPpfvResponseDTO();

            if (result.Succeeded)
            {
                var user = await _manager.FindByEmailAsync(userCredentials.Email);
                response.Ok = true;
                response.Message = "Ok";
                response.UserId = user.Id;
                response.Instituciones = await _institucionService.GetByUserId(user.Id);
                return Ok(response);
            }
            else
            {
                response.Ok = false;
                response.Message = "Login failed";
                return Ok(response);
            }
        }

        [HttpGet("user/by-id/{id}")]
        public async Task<ActionResult> GetById([FromRoute] string id)
        {
            if (!IsAppVaidate(Request))
            {
                return BadRequest("La aplicación no es valida");
            }

            var user = await _manager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest("No se encuentra el recurso solicitado");
            }

            var res = await getUser(id);

            return Ok(res);
        }

        [HttpGet("user/list-by-id/{ids}")]
        public async Task<ActionResult> GetListById([FromRoute] string ids)
        {
            if (ids == null)
            {
                return BadRequest("Debe enviar los ids");
            }

            var listId = ids.Split(',');
            var res = new List<UserDTO>();

            foreach (var id in listId)
            {
                var user = await getUser(id);
                res.Add(user);
            }

            return Ok(res);

        }

        private async Task<UserDTO> getUser(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            var entity = await _context.Users.Include(u => u.Sexo).FirstOrDefaultAsync(x => x.Id == user.Id);
            var roles = await _manager.GetRolesAsync(user);
            var res = _mapper.Map<UserDTO>(entity);
            if (roles != null && roles.Count > 0)
            {
                res.Rol = roles.First();
            }

            res.Sexo = entity.Sexo.Nombre;

            var servicioResp = await _servicioService.GetByUserId(user.Id);
            if (servicioResp.Servicios != null && servicioResp.Servicios.Count > 0)
            {
                res.ServicioId = servicioResp.Servicios.First().Id;
                res.Servicio = servicioResp.Servicios.First().Nombre;
            }

            return res;
        }


        private AutenticationResponse BuildToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email),
                new Claim("userId",userCredentials.UserId),
                new Claim("correo",userCredentials.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(6);
            //var expiration = DateTime.UtcNow.AddMinutes(1);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null,
                claims: claims, expires: expiration, signingCredentials: creds);

            return new AutenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }
    }
}
