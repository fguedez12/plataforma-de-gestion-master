using GobEfi.FV.APIV2.Models.DTOs;
using GobEfi.FV.APIV2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISectorPublicoService _sectorPublicoService;

        public AccountController(IConfiguration configuration, ISectorPublicoService sectorPublicoService )
        {
            _configuration = configuration;
            _sectorPublicoService = sectorPublicoService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserTokenDTO>> Login([FromBody] UserDTO model)
        {
           
            var resultado = await _sectorPublicoService.GetUser(model);
            if (!string.IsNullOrEmpty(resultado.Id))
            {
                return ConstruirToken(resultado);
            }

            return new UserTokenDTO { Ok = false }; 
            
        }

        [HttpGet("User")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [AllowAnonymous]
        public ActionResult<UserResponseDTO> Usuario()
        {
            var response = new UserResponseDTO();
            ClaimsPrincipal currentUser = this.User;
            if (currentUser.Claims.Count() > 0)
            {
                var email = currentUser.FindFirst(ClaimTypes.Email).Value;
                var id = currentUser.FindFirst("id").Value;
                var role = currentUser.FindFirst(ClaimTypes.Role).Value;
                var nombre = currentUser.FindFirst("Nombre").Value;
                var sexo = currentUser.FindFirst("Sexo").Value;
                var servicioId = Convert.ToInt64(currentUser.FindFirst("ServicioId").Value);

                var userInfo = new UserInfoDTO
                {
                    Email = email,
                    Id = id,
                    Role = role,
                    Nombre = nombre,
                    Sexo = sexo,
                    ServicioId = servicioId
                };

                response.Ok = true;
                response.User = userInfo;
                
            }
            else {
                response.Ok = false;

            }

            return response;
   
        }

        private  UserTokenDTO ConstruirToken(UserInfoDTO userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", userInfo.Id),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.Role),
                new Claim("Nombre",userInfo.Nombre),
                new Claim("Sexo",userInfo.Sexo),
                new Claim("ServicioId",userInfo.ServicioId.ToString()),
            };

            var userId = "";

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiracion,
                signingCredentials: creds);

            return new UserTokenDTO()
            {
                Ok = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion
            };

        }

    }
}
