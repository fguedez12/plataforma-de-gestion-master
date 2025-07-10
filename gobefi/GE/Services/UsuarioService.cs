using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Models.PermisosModels;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RolModels;
using GobEfi.Web.Models.UsuarioModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Data;

namespace GobEfi.Web.Services
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _repoUsuario;
        private IRolRepository rolRepository;
        private ILogger _logger;
        private IMapper mapper;
        private UserManager<Usuario> _userManager;
        private readonly IMenuRepository _repoMenu;
        private readonly IPermisosRepository _repoPermisos;
        private readonly IInstitucionRepository _repoInstitucion;
        private readonly IUsuarioDivisionRepository _repoUsuarioDivision;
        private readonly IServicioRepository _repoServicio;
        private readonly IUsuarioServicioRepository _repoUsuarioServicio;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext _context;
        protected readonly Usuario _usuario;
        private readonly bool _isAdmin;

        public readonly IDivisionRepository _repoDivision;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IRolRepository rolRepository,
            ILoggerFactory factory,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IMenuRepository repoMenu,
            IPermisosRepository repoPermisos,
            IInstitucionRepository repoInstitucion,
            IUsuarioDivisionRepository repoUsuarioDivision,
            IServicioRepository repoServicio,
            IDivisionRepository repoDivision,
            IUsuarioServicioRepository repoUsuarioServicio,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context)
        {
            this._repoUsuario = usuarioRepository;
            this.rolRepository = rolRepository;
            _logger = factory.CreateLogger<UsuarioService>();
            this.mapper = mapper;
            this._userManager = userManager;
            _repoMenu = repoMenu;
            _repoPermisos = repoPermisos;
            _repoInstitucion = repoInstitucion;
            _repoUsuarioDivision = repoUsuarioDivision;
            _repoServicio = repoServicio;
            _repoDivision = repoDivision;
            _repoUsuarioServicio = repoUsuarioServicio;
            this.httpContextAccessor = httpContextAccessor;
            _context = context;
            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;

            if (_usuario != null) 
            {
                _isAdmin = _userManager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;
            }
           
        }

        /// <summary>
        /// Este metodo exporta los mismos usuarios que el index de administracion de usuarios, pero utiliza distitos metodos para obtener la data.
        /// Considerar en obtener la data del mismo metodo el cual obtiene los usuarios para index
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UsuarioListExcelModel> Exportar()
        {
            List<long> serviciosIds = new List<long>();
            if (_isAdmin)
                serviciosIds = _repoServicio.Query().Select(s => s.Id).ToList();
            else
                serviciosIds = _repoUsuarioServicio.Query().Where(us => us.UsuarioId == _usuario.Id).Select(us => us.ServicioId).ToList();

            var data = _repoUsuario.ParaExcel(serviciosIds, _isAdmin);

            return data;
        }

        public IEnumerable<UsuarioModel> All()
        {
            return _repoUsuario
                .All()
                .OrderBy(o => o.Apellidos)
                .ProjectTo<UsuarioModel>(mapper.ConfigurationProvider)
                .ToList();
        }

        private IQueryable<Usuario> Query(UsuarioRequest request)
        {
            bool isAdmin = _userManager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;

            var query = _repoUsuario.Query();

            if (!isAdmin)
            {
                var serviciosIds = _repoUsuarioServicio.Query().Where(us => us.UsuarioId == _usuario.Id).Select(us => us.ServicioId).ToList();
                var usuariosIds = _repoUsuarioServicio.Query().Where(us => serviciosIds.Contains(us.ServicioId)).Select(us => us.UsuarioId).ToList();
                //HL-2019-09-30 Se ocultan los usuarios Auditores, son visibles solo para los administradores
                var roles = rolRepository.Query().Where(r => r.Nombre == "Auditor").Include(r=>r.UsuarioRoles).First();
                var usuariosRoles = roles.UsuarioRoles.Select(u=>u.UserId).ToList();
                query = query.Where(u => usuariosIds.Contains(u.Id));
                query = query.Where(u => !usuariosRoles.Contains(u.Id));
            }

            if (!string.IsNullOrEmpty(request.Nombres))
            {
                query = query.Where(u => EF.Functions.Like(u.Nombres, $"%{request.Nombres}%"));
            }

            if (!string.IsNullOrEmpty(request.Rut))
            {
                query = query.Where(u => EF.Functions.Like(u.Rut, $"%{request.Rut}%"));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{request.Email}%"));
            }

            if (request.InstitucionId > 0)
            {
                query = query.Include(ui => ui.UsuariosInstituciones);
                query = query.Where(u => u.UsuariosInstituciones.Any(ui => ui.InstitucionId == request.InstitucionId));
            }

            if (request.ServicioId > 0)
            {
                query = query.Include(ui => ui.UsuariosServicios);
                query = query.Where(u => u.UsuariosServicios.Any(us => us.ServicioId == request.ServicioId));
            }

            if (request.Certificado.HasValue)
            {
                query = query.Where(c => c.Certificado == request.Certificado);
            }

            if (request.Validado.HasValue)
            {
                query = query.Where(c => c.Validado == request.Validado);
            }

            query = query.Where(u => u.Active == request.Activo);

            query.OrderBy(o => o.Email);
            return query;
        }

        public PagedList<UsuarioListModel> Paged(UsuarioRequest request)
        {
            var queryModel = Query(request)
                .ProjectTo<UsuarioListModel>(mapper.ConfigurationProvider);
            return _repoUsuario.Paged(
                queryModel,
                request.Page,
                request.Size);
        }

        public async void Delete(string id)
        {
            var usuario = _repoUsuario.Get(id);
            if (usuario == null)
            {
                throw new NotFoundException(nameof(usuario));
            }

            var notas = await _context.NotasCertificados.Where(x => x.UsuarioId == id).ToListAsync();

            _context.NotasCertificados.RemoveRange(notas);
            await _context.SaveChangesAsync();
            _repoUsuario.Delete(usuario);
            _repoUsuario.SaveChanges();

            _logger.LogInformation($"Usuario: {usuario.Email} Eliminado por: {_usuario.Email} Id: {_usuario.Id}");
        }

        public string NombreCompletoUsuario(string id)
        {
            var user = _repoUsuario.Get(id);


            return $"{user?.Nombres} {user?.Apellidos}";
        }

        public UsuarioModel Get(string id)
        {
            //var usuario = repository.Get(id);
            var usuario = _repoUsuario.Query().Include(c => c.Comuna).Where(u => u.Id == id).FirstOrDefault();

            if (usuario == null)
            {
                throw new NotFoundException(nameof(usuario));
            }

            return mapper.Map<UsuarioModel>(usuario);
        }
        public string Insert(UsuarioModel model)
        {
            var usuario = mapper.Map<Usuario>(model);
            usuario.UserName = usuario.Email;
            usuario.NormalizedEmail = usuario.Email.ToUpper();
            usuario.NormalizedUserName = usuario.NormalizedEmail;
            usuario.CreatedAt = DateTime.Now;
            usuario.CreatedBy = _usuario.Id;

            _repoUsuario.Insert(usuario);
            int result = _repoUsuario.SaveChanges();

            return usuario.Id;
        }

        public void Update(UsuarioModel model)
        {
            //var usuario = _repoUsuario.GetWithRoles(model.Id);
            var usuario = _repoUsuario.All()
                .Include(r => r.UsuarioRoles)
                .Include(ui => ui .UsuariosInstituciones)
                .Include(us => us.UsuariosServicios)
                .Include(ud => ud.UsuariosDivisiones)
                .SingleOrDefault(u => u.Id == model.Id);
            if (usuario == null)
            {
                throw new NotFoundException(nameof(usuario));
            }

            var roles = usuario.UsuarioRoles.ToList();
            foreach (var roleName in roles)
            {
                var rol = rolRepository.Get(roleName.RoleId);
                var task = _userManager.RemoveFromRoleAsync(usuario, rol.Name).Result;
            }

            foreach (var rol in model.Roles)
            {
                var r = rolRepository.Get(rol.Id);
                var task = _userManager.AddToRoleAsync(usuario, r.Name).Result;
            }

            var instituciones = usuario.UsuariosInstituciones.ToList();

            foreach (var item in instituciones)
                usuario.UsuariosInstituciones.Remove(item);

            foreach (var item in model.InstitucionesId)
            {
                UsuarioInstitucion relacion = new UsuarioInstitucion { InstitucionId = item, UsuarioId = model.Id };

                usuario.UsuariosInstituciones.Add(relacion);
            }

            // Deshabilita los servicios y divisiones de las instituciones no asociadas
            if (model.InstitucionesNoAsociados != null && model.InstitucionesNoAsociados.Count > 0)
                foreach (var item in model.InstitucionesNoAsociados)
                {
                    var serviciosAQuitar = _repoServicio.Query().Where(s => s.InstitucionId == item).ToList();
                    var divisionesAQuitar = _repoDivision.Query().Where(d => serviciosAQuitar.Any(s => s.Id == d.ServicioId)).ToList();

                    var usuarioServicios = usuario.UsuariosServicios.Where(us => serviciosAQuitar.Any(s => s.Id == us.ServicioId)).ToList();
                    var usuarioDivisiones = usuario.UsuariosDivisiones.Where(ud => divisionesAQuitar.Any(d => d.Id == ud.DivisionId)).ToList();

                    foreach (var itemServicio in usuarioServicios)
                        usuario.UsuariosServicios.Remove(itemServicio);

                    foreach (var itemDivision in usuarioDivisiones)
                        usuario.UsuariosDivisiones.Remove(itemDivision);
                }

            var servicios = usuario.UsuariosServicios.ToList();

            foreach (var item in servicios)
                usuario.UsuariosServicios.Remove(item);

            foreach (var item in model.ServiciosId)
            {
                UsuarioServicio relacion = new UsuarioServicio { ServicioId = item, UsuarioId = model.Id };

                usuario.UsuariosServicios.Add(relacion);
            }

            // deshabilita las divisiones de los servicios no asociados
            if (model.ServiciosNoAsociados != null && model.ServiciosNoAsociados.Count > 0)
                foreach (var item in model?.ServiciosNoAsociados)
                {
                    var divisiones = _repoDivision.Query().Where(d => d.ServicioId == item).ToList();

                    var usuarioDivisiones = usuario.UsuariosDivisiones.Where(ud => divisiones.Any(d => d.Id == ud.DivisionId)).ToList();

                    foreach (var itemDivision in usuarioDivisiones)
                        usuario.UsuariosDivisiones.Remove(itemDivision);
                }

            var createdAt = usuario.CreatedAt;
            var createdBy = usuario.CreatedBy;

            mapper.Map<UsuarioModel, Usuario>(model, usuario);

            usuario.Active = model.Active;
            usuario.EmailConfirmed = true;

            usuario.NormalizedEmail = usuario.Email.ToUpper();
            usuario.NormalizedUserName = usuario.Email.ToUpper();
            usuario.UpdatedAt = DateTime.Now;
            usuario.ModifiedBy = _usuario.Id;
            usuario.CreatedBy = createdBy;
            usuario.CreatedAt = createdAt;

            _repoUsuario.Update(usuario);
            _repoUsuario.SaveChanges();
        }

        public void UpdateSexo(string usuarioId, int sexoId) {

            var usuario = _repoUsuario.Get(usuarioId);

            usuario.SexoId = sexoId;

            _repoUsuario.Update(usuario);
            _repoUsuario.SaveChanges();
        }

        public Task<UsuarioModel> UpdateRoles(ICollection<RolModel> rols)
        {
            throw new NotImplementedException();
        }

        public PermisosModel TienePermisos(Usuario usuario, string rutaActual)
        {
            if (usuario == null) {
                return new PermisosModel();
            }

            string[] urlSplit = rutaActual.Split('/');
            bool esNumero = long.TryParse(urlSplit[urlSplit.Length - 1], out long numero);

            if (esNumero)
                rutaActual = rutaActual.Replace(urlSplit[urlSplit.Length - 1], "");
            
            var userFromRepository = _repoUsuario.Query()
                .Include(ur => ur.UsuarioRoles)
                .Where(u => u.Id == usuario.Id)
                .FirstOrDefault();
            
            PermisosModel permisosDelRol = new PermisosModel();

            if (!_repoMenu.Query().Any(r => r.Url == rutaActual))
                return new PermisosModel();

            long paginaId = _repoMenu.Query().Where(u => u.Url == rutaActual).FirstOrDefault().Id;

            var permisosDelRepo = _repoPermisos.Query()
                .Where(r => userFromRepository.UsuarioRoles.Any(rol => rol.RoleId == r.RolId)
                && (r.MenuId == paginaId))
                .FirstOrDefault();

            permisosDelRol = mapper.Map<PermisosModel>(permisosDelRepo);
    

            return permisosDelRol;
        }

        public async Task<int> AgregarAsociaciones(UsuarioModel model)
        {
            var usuario = await _repoUsuario.All()
                .Include(ui => ui.UsuariosInstituciones)
                .Include(ui => ui.UsuariosServicios)
                .Where(u => u.Id == model.Id).FirstOrDefaultAsync();

            foreach (var institucionId in model.InstitucionesId)
            {
                usuario.UsuariosInstituciones.Add(new UsuarioInstitucion { UsuarioId = model.Id, InstitucionId = institucionId });
            }

            foreach (var servicioId in model.ServiciosId)
            {
                usuario.UsuariosServicios.Add(new UsuarioServicio { UsuarioId = model.Id, ServicioId = servicioId });
            }

            return _repoUsuario.SaveChanges();

        }

        public bool ExisteEmail(string email)
        {
            return _repoUsuario.Query().Where(u => u.Email == email).Count() > 0;
        }

        public PermisosAccion GetPermisosAccion(Usuario usuario, string rutaActual)
        {
            string[] urlSplit = rutaActual.Split('/');
            bool esNumero = long.TryParse(urlSplit[urlSplit.Length - 1], out long numero);

            if (esNumero)
                rutaActual = rutaActual.Replace(urlSplit[urlSplit.Length - 1], "");

            PermisosAccion permisosDelRol = new PermisosAccion();

            string pag = $"/{rutaActual.Split('/')[1]}";

            if (!_repoMenu.Query().Any(r => r.Url == pag))
                return new PermisosAccion();

            long paginaId = _repoMenu.Query().Where(u => u.Url == pag).FirstOrDefault().Id;

            var userFromRepository = _repoUsuario.Query()
                .Include(ur => ur.UsuarioRoles)
                .Where(u => u.Id == usuario.Id)
                .FirstOrDefault();

            var permisosDelRepo = _repoPermisos.Query()
                .Where(r => userFromRepository.UsuarioRoles.Any(rol => rol.RoleId == r.RolId)
                && (r.MenuId == paginaId))
                .FirstOrDefault();

            permisosDelRol = mapper.Map<PermisosAccion>(permisosDelRepo);

            return permisosDelRol;
        }
    }
}
