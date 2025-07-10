using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.UsuarioModels;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.Extensions.Logging;
using GobEfi.Web.Services;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Services.Request;
using GobEfi.Web.Models;
using GobEfi.Web.Models.RolModels;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Helper;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using GobEfi.Web.Core.Configuration;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Business.Validaciones;
using System;
using GobEfi.Web.Models.UnidadModels;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace GobEfi.Web.Controllers
{
    public class AdminUsuarioController : BaseController
    {
        private readonly IUsuarioService _servUsuario;
        private readonly IRolService _servRole;
        private readonly IServicioService _servServicio;
        private readonly IInstitucionService _servInstitucion;
        private readonly IComunaService _servComuna;
        private readonly IRegionService _servRegion;
        private readonly IProvinciaService _servProvinicia;
        private readonly ISexoService _servSexo;
        private readonly IEmailSender _sender;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IDivisionService _servDivision;
        private readonly ILogger _logger;

        private IHostingEnvironment _hostingEnvironment;
        private readonly DirectorioArchivos _directorios;

        public AdminUsuarioController(
            ApplicationDbContext context,
            UserManager<Usuario> userManager,
            IDivisionService servDivision,
            IUsuarioService servUsuario,
            IRolService servRole,
            IServicioService servServicio,
            IInstitucionService servInstitucion,
            IComunaService servComuna,
            IRegionService servRegion,
            IProvinciaService servProvinicia,
            ISexoService servSexo,
            IEmailSender emailSender,
            ILoggerFactory factory,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IHostingEnvironment hostingEnvironment,
            DirectorioArchivos directorios) : base(context, userManager, httpContextAccessor, servUsuario)
        {
            _userManager = userManager;
            _servDivision = servDivision;
            _servUsuario = servUsuario;
            _servRole = servRole;
            _servServicio = servServicio;
            _servInstitucion = servInstitucion;
            _servComuna = servComuna;
            _servRegion = servRegion;
            _servProvinicia = servProvinicia;
            _servSexo = servSexo;
            _sender = emailSender;
            _mapper = mapper;
            _logger = factory.CreateLogger<AdminUsuarioController>();

            _hostingEnvironment = hostingEnvironment;
            _directorios = directorios;
        }

        public async Task<IActionResult> OnPostExport()
        {

            IEnumerable<UsuarioListExcelModel> dataUsuarios = _servUsuario.Exportar();


            // string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sWebRootFolder = _directorios.Temporales;


            string sFileName = @"Usuarios.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("usuarios");
                IRow row = excelSheet.CreateRow(0);

                int celda = 0;

                foreach (var prop in new UsuarioListExcelModel().GetType().GetProperties())
                {
                    row.CreateCell(celda).SetCellValue(prop.Name);
                    excelSheet.SetColumnWidth(celda, 6000);
                    celda++;
                }

                int fila = 1;

                foreach (var item in dataUsuarios)
                {
                    row = excelSheet.CreateRow(fila);
                    celda = 0;

                    foreach (var propieda in item.GetType().GetProperties())
                    {
                        var valor = propieda.GetValue(item, null);

                        switch (valor?.GetType()?.FullName)
                        {
                            case "GobEfi.Web.Models.ComunaModels.ComunaModel":
                                var objComuna = valor;

                                foreach (var propComuna in valor.GetType().GetProperties())
                                {
                                    if (propComuna.Name == "Nombre")
                                    {
                                        valor = propComuna.GetValue(objComuna, null);
                                    }
                                }

                                break;
                            default:
                                break;
                        }

                        row.CreateCell(celda).SetCellValue(string.IsNullOrEmpty(valor._toString()) ? "" : valor.ToString());

                        celda++;
                    }

                    fila++;
                }

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);
        }

        private void LlenarCombosIndex()
        {
            ViewBag.Institucion = SelectHelper.LlenarDDL(_servInstitucion.GetInstituciones().Result, true);
            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);

            ViewBag.Tiene = SelectHelper.LlenarEnumTiene();
        }

        private void LlenarCombosDetails(string userId)
        {
            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);
            ViewBag.Sexos = SelectHelper.LlenarDDL(_servSexo.All(), true);

            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);


            ViewBag.RolesAsociados = SelectHelper.LlenarDDL(_servRole.GetByUserId(userId));
            //ViewBag.Provincias = SelectHelper.LlenarDDL(_servProvinicia.All(), true);


            //ViewBag.RolesNoAsociados = SelectHelper.LlenarDDL(_servRole.GetByCurrentUserRol().Result);
        }

        private void LlenarCombosCreate()
        {
            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);
            ViewBag.Sexos = SelectHelper.LlenarDDL(_servSexo.All(), true);

            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
            ViewBag.Provincias = SelectHelper.LlenarDDL(_servProvinicia.All(), true);


            ViewBag.RolesNoAsociados = SelectHelper.LlenarDDL(_servRole.GetByCurrentUserRol().Result);
        }

        private void LlenarCombosEdit(string id)
        {
            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);
            ViewBag.Sexos = SelectHelper.LlenarDDL(_servSexo.All(), true);

            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
            ViewBag.Provincias = SelectHelper.LlenarDDL(_servProvinicia.All(), true);

            ViewBag.RolesAsociados = SelectHelper.LlenarDDL(_servRole.GetByUserId(id));
            ViewBag.RolesNoAsociados = SelectHelper.LlenarDDL(_servRole.GetNoAsociadasByUserId(id));
        }

        // GET: AdminUsuario
        public async Task<IActionResult> Index(UsuarioRequest request)
        {
            ViewData["permisos"] = GetPermisions();

            var indexModel = new UsuarioIndexModel();
            indexModel.Items = _servUsuario.Paged(request);
            indexModel.Sort = SortItems();
            LlenarCombosIndex();

            ViewBag.ServicioId = request.ServicioId;

            return View(indexModel);
        }

        // GET: AdminUsuario/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioModel = _servUsuario.Get(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            LlenarCombosDetails(id);


            return View(usuarioModel);
        }

        // GET: AdminUsuario/Create
        public IActionResult Create()
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            LlenarCombosCreate();

            ViewBag.CurrentUserIsAdmin = _userManager.IsInRoleAsync(usuario, "ADMINISTRADOR").Result;
            ViewBag.CurrentUserIsGestorServicio = _userManager.IsInRoleAsync(usuario, "GESTOR_SERVICIO").Result;

            return View();
        }

        private Dictionary<string, string> Validaciones(UsuarioModel usuario, bool esCreacion)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (!usuario.Rut._toRut().valido)
            {
                ret.Add("Rut", "El rut ingresado no es valido.");
            }
            
            if (!usuario.Email.Trim()._isEmail())
            {
                ret.Add("Email", "El email ingresado no es valido.");
            }


            if (esCreacion)
            {
                if (_servUsuario.ExisteEmail(usuario.Email.Trim()))
                {
                    ret.Add("Email", "El email ingresado ya existe en nuestros registros.");
                }
            }


            if (usuario.RegionId <= 0)
            {
                ret.Add("RegionId", "La región es obligatoria");
            }

            if (usuario.ProvinciaId <= 0)
            {
                ret.Add("ProvinciaId", "La provincia es obligatoria");
            }

            if (usuario.ComunaId <= 0)
            {
                ret.Add("ComunaId", "La comuna es obligatoria");
            }

            if (usuario.RolesId == null || usuario.RolesId.Count != 1)
            {
                ret.Add("RolesId", "Debe ingresar 1 tipo de gestor.");
            }


            return ret;

        }

        // POST: AdminUsuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombres,Apellidos,Email,RegionId,ProvinciaId,ComunaId,SexoId,Rut,NumeroTelefono,NumeroTelefonoOpcional,Cargo,RolesId,InstitucionesId,ServiciosId,Certificado,Validado")] UsuarioModel model)
        {
            // validar rut, y en actualizar tbn 

            var errores = Validaciones(model, true);

            foreach (var error in errores)
                ModelState.AddModelError(error.Key, error.Value);
                

            if (!ModelState.IsValid)
            {
                //return View(model);

                return BadRequest(ModelState);
            }

            var user = _mapper.Map<Usuario>(model);
            user.EmailConfirmed = true;
            user.Active = true;
            user.CreatedAt = DateTime.Now;
            user.CreatedBy = usuario.Id;

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("El usuario creo una nueva cuenta con clave.");

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _sender.SendEmailAsync(model.Email, "Crear contraseña",
                   $"Ha sido creada una cuenta de usuario para usted, para crear su contraseña haga click en el link a continaución: <a href='{callbackUrl}'>crear contraseña</a>.");

                foreach (string rolId in model.RolesId)
                {
                    string rolName = _servRole.Get(rolId).NormalizedName;

                    await _userManager.AddToRoleAsync(user, rolName);
                }
                model.Id = user.Id;

                int resultAsociaciones = await _servUsuario.AgregarAsociaciones(model);
                
            }
            //ICollection<DivisionesToAssociate> unidadesParaAsociar = await _servDivision.GetToAssociate(model.ServiciosId, user.Id);

            DivisionesParaAsociar unidades = new DivisionesParaAsociar
            {
                PermiteModificar = await _servDivision.RoleActualPermiteAsociar(), // true,
                DivisionesList = await _servDivision.GetToAssociate(model.ServiciosId, user.Id)
            };

            ViewBag.NewUserId = user.Id;
            return PartialView(@"Shared/_AsociarUnidades", unidades);
            //return RedirectToAction(nameof(Index));
        }

        // GET: AdminUsuario/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var userFromDb = await _userManager.FindByIdAsync(id);

            var isGestor = _userManager.IsInRoleAsync(userFromDb, "GESTOR_SERVICIO").Result;

            var currentGs = _userManager.IsInRoleAsync(usuario, "GESTOR_SERVICIO").Result;

            var usuarioModel = _servUsuario.Get(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            ViewBag.CurrentUserIsAdmin = _userManager.IsInRoleAsync(usuario, "ADMINISTRADOR").Result;
            ViewBag.CurrentUserIsGestorServicio = _userManager.IsInRoleAsync(usuario, "GESTOR_SERVICIO").Result;

            ViewBag.restrictGs = isGestor && currentGs;

            LlenarCombosEdit(id);

            return View(usuarioModel);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarClave(UsuarioCambioClaveModel requestUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario user = await _userManager.FindByEmailAsync(requestUser.CambioEmail);
            string tokenPassword = await manager.GeneratePasswordResetTokenAsync(user);
            var changePasswordResult = await manager.ResetPasswordAsync(user, tokenPassword, requestUser.NuevaClave);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                //return View(requestUser);
                //return RedirectToAction(nameof(Edit), new {id = requestUser.Id});
                return BadRequest(ModelState);
            }

            //UsuarioModel model = _servUsuario.Get(requestUser.Id);

            return RedirectToAction(nameof(Edit), new { id = requestUser.Id });
        }

        // POST: AdminUsuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id, 
            [Bind("Nombres,Apellidos,Email,Id,RolesId,InstitucionesId,ServiciosId,SexoId,RegionId,ProvinciaId,ComunaId,NumeroTelefono,NumeroTelefonoOpcional,Rut,Active,Certificado,Validado,Cargo, ServiciosNoAsociados, InstitucionesNoAsociados ")] UsuarioModel usuarioModel)
        {
            var errores = Validaciones(usuarioModel, false);

            foreach (var error in errores)
                ModelState.AddModelError(error.Key, error.Value);

            if (id != usuarioModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<RolModel> roles = new List<RolModel>();
            foreach (string rol in usuarioModel.RolesId)
            {
                _logger.LogInformation("User roles {Id}", rol);
                var userRol = _servRole.Get(rol);
                roles.Add(userRol);
            }

            try
            {
                usuarioModel.Roles = roles;

                _servUsuario.Update(usuarioModel);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioModelExists(usuarioModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //ICollection<DivisionesToAssociate> unidadesParaAsociar = await _servDivision.GetToAssociate(usuarioModel.ServiciosId, usuarioModel.Id);
            DivisionesParaAsociar unidad = new DivisionesParaAsociar
            {
                PermiteModificar = await _servDivision.RoleActualPermiteAsociar(), // true,
                DivisionesList = await _servDivision.GetToAssociate(usuarioModel.ServiciosId, usuarioModel.Id)
            };

            ViewBag.NewUserId = usuarioModel.Id;

            return PartialView(@"Shared/_AsociarUnidades", unidad);
        }

        [HttpPut("actualizaSexo")]
        public IActionResult ActualizaSexo(string userId, int sexoId)
        //public IActionResult ActualizaSexo()
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            else
            {
                try
                { 
                    _servUsuario.UpdateSexo(userId, sexoId);
                    return Ok();
                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }

            }


        }

        [HttpGet("obtenerUnidades/{userId}")]
        public async Task<IActionResult> ObtenerUnidades([FromRoute] string userId)
        {
            var servicios = _servServicio.GetAsociadosByUserId(userId).Result;
            List<long> idsServicios = new List<long>();

            foreach (var item in servicios)
            {
                idsServicios.Add(item.Id);
            }

            DivisionesParaAsociar unidad = new DivisionesParaAsociar
            {
                PermiteModificar = await _servDivision.RoleActualPermiteAsociar(), // true,
                DivisionesList = await _servDivision.GetToAssociate(idsServicios, userId)
            };

            return PartialView(@"Shared/_AsociarUnidades", unidad);
        }

        [HttpGet("asociar-todas-unidades/{userId}")]
        public async Task<IActionResult> AsociarTodasLasUnidades([FromRoute] string userId)
        {
            var servicios = _servServicio.GetAsociadosByUserId(userId).Result;
            List<long> idsServicios = new List<long>();

            foreach (var item in servicios)
            {
                idsServicios.Add(item.Id);
            }

            DivisionesParaAsociar unidad = new DivisionesParaAsociar
            {
                PermiteModificar = await _servDivision.RoleActualPermiteAsociar(), // true,
                DivisionesList = await _servDivision.GetToAssociate(idsServicios, userId)
            };

            var usuarioDivisines = await context.UsuariosDivisiones.Where(x => x.UsuarioId == userId).ToListAsync();

            context.UsuariosDivisiones.RemoveRange(usuarioDivisines);
            await context.SaveChangesAsync();

            foreach (var item in unidad.DivisionesList)
            {
                var newItem = new UsuarioDivision { DivisionId = item.Id, UsuarioId = userId };
                context.UsuariosDivisiones.Add(newItem);
                
            }

            await context.SaveChangesAsync();

            return PartialView(@"Shared/_AsociarUnidades", unidad);
        }

        [HttpGet("obtenerUnidadesv2/{userId}")]
        public async Task<IActionResult> ObtenerUnidadesv2([FromRoute] string userId)
        {
            var servicios = _servServicio.GetAsociadosByUserId(userId).Result;
            List<long> idsServicios = new List<long>();

            foreach (var item in servicios)
            {
                idsServicios.Add(item.Id);
            }



            UnidadParaAsociar unidad = new UnidadParaAsociar
            {
                PermiteModificar = await _servDivision.RoleActualPermiteAsociar(), // true,
                UnidadesList = await _servDivision.GetToAssociatev2(idsServicios, userId)
            };

            return PartialView(@"Shared/_AsociarUnidadesv2", unidad);
        }


        // GET: AdminUsuario/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioModel = _servUsuario.Get(id);
            if (usuarioModel == null)
            {
                return NotFound();
            }

            return View(usuarioModel);
        }

        // POST: AdminUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //_servUsuario.Delete(id);
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return BadRequest("No se encuentra el usuario");
            }
            user.Active = false;
            user.ModifiedBy = usuario.Id;
            user.UpdatedAt = DateTime.Now;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioModelExists(string id)
        {
            return (_servUsuario.Get(id) == null) ? false : true;
        }

        private SortModel SortItems()
        {
            var order = new SortModel();
            order.AddSortField("Id", "id");
            order.AddSortField("Nombre", "nombre");
            return order;
        }
    }
}
