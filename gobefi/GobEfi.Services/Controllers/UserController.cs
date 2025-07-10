using GobEfi.Services.Models.UserModels;
using GobEfi.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GobEfi.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController :  ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;

        public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IUserService userService )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet("validar")]
        public async Task<ActionResult<UserResponse>> Get(UserInfo model) {

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var response = new UserResponse
                {
                    Ok = true,
                    UserId = user.Id,
                    Message = "Usuario valido"
                };

                return response;
            }
            else {
                var response = new UserResponse
                {
                    Ok = false,
                    UserId = null,
                    Message = "Usuario/Contraseña invalidos"
                };
                return response;
            }

        }

        [HttpGet("acceso")]
        public async Task<ActionResult<AccesoResponse>> Get(AccesoInfo model) 
        {
            var result = new AccesoResponse();

            try
            {
                var acceso =await _userService.Acceso(model);

                if (acceso)
                {
                    result.Ok = true;
                    result.Message = "Usuario OK";
                }
                else 
                {
                    result.Ok = false;
                    result.Message = "Usuario sin permisos";
                }

               
            }
            catch (Exception ex)
            {

                result.Ok = false;
                result.Message = "ha ocurrido un error: "+ ex.Message;
            }

            return result;
        }



        [HttpGet("entidades/{id}")]
        public async Task<ActionResult<EntidadesResponse>> Get(string id)
        {
            var response = new EntidadesResponse();

            if (string.IsNullOrWhiteSpace(id))
            {
                response.Ok = false;
                response.Message = "Debe ingresar un id de usuario";
                response.Entidades = null;

                return BadRequest(response);
            }

            try
            {
                var entidades = await _userService.GetEntidades(id);

                if (entidades == null) 
                {
                    response.Ok = false;
                    response.Message = "No se encuentra el usuario";
                    response.Entidades = null;

                    return BadRequest(response);
                }

                response.Ok = true;
                response.Entidades = entidades;
                response.Message = "Consulta correcta";

                return response;
            }
            catch (Exception ex)
            {

                response.Ok = false;
                response.Message = "Ocurrio un error : " + ex.Message;
                response.Entidades = null;

                return StatusCode(500, response);
            }
        }
    }
}
