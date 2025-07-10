using AutoMapper;
using GobEfi.Business.Validaciones;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Extensions;
using GobEfi.Web.Models.AccountViewModels;
using GobEfi.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IServicioService _servicioService;
        private readonly IInstitucionService _institucionService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            IServicioService servicioService,
            IInstitucionService institucionService,
            ApplicationDbContext  context,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _servicioService = servicioService;
            _institucionService = institucionService;
            _context = context;
            _mapper = mapper;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var version = "2.35";

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            ViewData["version"] = version;
            return View();
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {

            return View();
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Login(int id=0)
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result =
                    await _signInManager.PasswordSignInAsync(
                        model.Email,
                        model.Password,
                        model.RememberMe,
                        lockoutOnFailure: true);

                Usuario user = null;
                if (result.Succeeded)
                    user = await _userManager.FindByEmailAsync(model.Email);

                if (result.Succeeded && user.Active._toBool())
                {
                    if (await _userManager.IsInRoleAsync(user, "GESTOR_FLOTA"))
                    {
                        return RedirectToAction("Login");

                    };

                    if ( await _servicioService.IsGEV3(user))
                    {
                        return RedirectToAction("Index","MiUnidadV2");
                    }

                    //if (!await _userManager.IsInRoleAsync(user, "ADMINISTRADOR"))
                    //{
                    //    //Todo consultar si esta bloqueado el ingreso de información
                    //    var bloqueoIngresoInfo = await IsInfoLockedService(user.Id);
                    //    //var bloqueoIngresoInfo = false;
                    //    var role = "GESTOR DE CONSULTA"; // Reemplaza "NombreDelRol" con el nombre del rol que deseas asignar

                    //    //obtener los servicios del usuario
                    //    var servicios = await _context.UsuariosServicios
                    //        .Include(x => x.Servicio)
                    //        .Where(x => x.UsuarioId == user.Id)
                    //        .Select(x => x.Servicio).ToListAsync();

                    //    foreach (var servicio in servicios)
                    //    {
                    //        //obtener los usuarios del servicio
                    //        var users = await _context.UsuariosServicios.Include(x => x.Usuario)
                    //            .Where(x => x.ServicioId == servicio.Id)
                    //            .Select(x => x.Usuario).ToListAsync();
                    //        foreach (var usuario in users)
                    //        {
                    //            if (bloqueoIngresoInfo)
                    //            {
                    //                if (!await _userManager.IsInRoleAsync(usuario, role) && !await _userManager.IsInRoleAsync(usuario, "ADMINISTRADOR"))
                    //                {
                    //                    var rolesActuales = await _userManager.GetRolesAsync(usuario);
                    //                    var newRespaldo = new RespladoUserRole()
                    //                    {
                    //                        UserId = usuario.Id,
                    //                        Role = rolesActuales.FirstOrDefault()
                    //                    };
                    //                    _context.RespladoUserRoles.Add(newRespaldo);
                    //                    await _context.SaveChangesAsync();
                    //                    foreach (var roleActual in rolesActuales)
                    //                    {
                    //                        await _userManager.RemoveFromRoleAsync(usuario, roleActual);
                    //                    }
                                        
                    //                    var roleResult = await _userManager.AddToRoleAsync(usuario, role);
                    //                    if (!roleResult.Succeeded)
                    //                    {
                    //                        _logger.LogWarning($"No se pudo agregar el rol {role} al usuario {model.Email}.");
                    //                        ModelState.AddModelError(string.Empty, "No se pudo agregar el rol al usuario.");
                    //                    }
                    //                }

                    //            }
                    //            else 
                    //            {
                    //                var respaldoRole = await _context.RespladoUserRoles.FirstOrDefaultAsync(x => x.UserId == usuario.Id);
                    //                if (respaldoRole != null)
                    //                {
                    //                    var rolesActuales = await _userManager.GetRolesAsync(usuario);
                    //                    foreach (var roleActual in rolesActuales)
                    //                    {
                    //                        await _userManager.RemoveFromRoleAsync(usuario, roleActual);
                    //                    }
                    //                    var roleResult = await _userManager.AddToRoleAsync(usuario, respaldoRole.Role);
                    //                    _context.RespladoUserRoles.Remove(respaldoRole);
                    //                    await _context.SaveChangesAsync();
                    //                }
                                    
                                    
                    //            }
                                
                    //        }
                    //    }
                    //}

                    _logger.LogInformation($"{model.Email} ha ingresado");
                    return RedirectToLocal(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl, model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning($"{model.Email} se encuentra bloqueado.");
                    return RedirectToAction(nameof(Lockout));
                }
                if (result.IsNotAllowed)
                {
                    _logger.LogWarning($"{model.Email} es posible que deba confirmar su email antes de ingresar al portal.");

                    ModelState.AddModelError(string.Empty, "Es posible que deba confirmar su email.");
                    return View(model);
                }
                else
                {
                    await _signInManager.SignOutAsync();

                    ModelState.AddModelError(string.Empty, "Email o Contraseña no válidos.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var model = new LoginWith2faViewModel { RememberMe = rememberMe };
            ViewData["ReturnUrl"] = returnUrl;

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, model.RememberMachine);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with 2fa.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID {UserId}.", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWithRecoveryCode(string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWithRecoveryCode(LoginWithRecoveryCodeViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new ApplicationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = model.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID {UserId} logged in with a recovery code.", user.Id);
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID {UserId} account locked out.", user.Id);
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID {UserId}", user.Id);
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.Instituciones = new SelectList(_institucionService.All(),"Id","Nombre");
            ViewBag.Resultado = false;
            return View();
        }

        [AllowAnonymous]
        public async  Task<JsonResult> GetServicios(long ministerioId)
        {
            var list = await _servicioService.GetByInstitucionId(ministerioId);
            return Json(list);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.Instituciones = new SelectList(_institucionService.All(), "Id", "Nombre");
            var serviciosList = await _servicioService.GetByInstitucionId(Convert.ToInt64(model.MinisterioId));
            var list = serviciosList.ToList();
            list.Add(new Models.ServicioModels.ServicioModel() { Id = -1, Nombre = "Otro" });
            ViewBag.Servicios = new SelectList(list, "Id", "Nombre");
            if (ModelState.IsValid)
            {
                var registro = _mapper.Map<Registro>(model);
                registro.Fecha = DateTime.Now;
                _context.Registros.Add(registro);
                await _context.SaveChangesAsync();
                //var user = new Usuario { UserName = model.Email, Email = model.Email };
                //var result = await _userManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //    var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //    await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                //    //await _signInManager.SignInAsync(user, isPersistent: false);
                //    _logger.LogInformation("El usuario creó una nueva cuenta con contraseña.");
                //    return RedirectToLocal(returnUrl);
                //}
                //AddErrors(result);
                ViewBag.Resultado = true;
                return View("Contacto");
            }
            ViewBag.Resultado = false;
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Contacto()
        {
            return View();
        }

            [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            // _logger.LogInformation("Usuario se desconecto.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Usuario ingreso con el proveedor {info.LoginProvider}.");
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["LoginProvider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                var user = new Usuario { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(nameof(ExternalLogin), model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPassword));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                _logger.LogInformation("User " + user.UserName + " has request to reset his/her password.");
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Olvidó su contraseña",
                   $"Hemos recibido una solicitud para crear una nueva contraseña, si usted ha realizado esta petición haga click en el siguiente enlace: <a href='{callbackUrl}'>cambiar contraseña</a>, de otra forma omita este correo.");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                throw new ApplicationException("A code must be supplied for password reset.");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            AddErrors(result);
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return RedirectToAction(nameof(HomeController.Index), "Home");
                return RedirectToAction(nameof(MiUnidadController.Index), "MiUnidad");
            }
        }

        private async Task<bool> IsInfoLockedService(string userId) 
        {
            var resp = false;
            var servicios = await _context.UsuariosServicios.Include(x=>x.Servicio).Where(x => x.UsuarioId == userId).Select(x=>x.Servicio).ToListAsync();
            foreach (var item in servicios)
            {
                if (item.BloqueoIngresoInfo)
                {
                    resp = true;
                }
            }
            return resp;
        }

        #endregion
    }
}
