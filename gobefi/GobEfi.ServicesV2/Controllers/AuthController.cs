using GobEfi.ServicesV2.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GobEfi.ServicesV2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(UserInfo userInfo)
        {
            var info = userInfo;
            if (info.UserName == _configuration["CMCreds:Username"] &&
                info.Password == _configuration["CMCreds:Password"]
                )
            {
                return await Task.FromResult(BuildToken(info));
            }

            return BadRequest("login attempt failed");
        }

        private UserToken BuildToken(UserInfo userInfo)
        {
            //se crean los claims para enviar en el token
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.UserName),
            new Claim("Application", "API Integración ChileMedido GE"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            //seguidad del token

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
