using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class ServicioService : IServicioService
    {
        private IServicioRepository _repoServicio;
        private readonly IInstitucionService _servInstitucion;
        private readonly IUsuarioServicioRepository _repoUsuarioServicio;
        private readonly ITrazabilidadService trazaService;
        private readonly ILogger logger;
        private IMapper _mapper;
        protected readonly UserManager<Usuario> _manager;
        private readonly ApplicationDbContext _context;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly Usuario _usuario;
        private bool _currentUserIsAdmin;

        public ServicioService(
            ILoggerFactory factory,
            IMapper mapper,
            IServicioRepository repoServicio,
            IInstitucionService repoInstitucion,
            IUsuarioServicioRepository repoUsuarioServicio,
            ITrazabilidadService trazaService,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context
            )
        {
            _repoServicio = repoServicio;
            _servInstitucion = repoInstitucion;
            _repoUsuarioServicio = repoUsuarioServicio;
            this.trazaService = trazaService;
            logger = factory.CreateLogger<ServicioService>();
            _mapper = mapper;
            _manager = manager;
            _context = context;
            _usuario = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;

            if (_usuario != null)
            {
                _currentUserIsAdmin = _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;
            }
          
        }

        private IQueryable<Servicio> Query(ServicioRequest request)
        {
            var query = _repoServicio.Query();

            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(u => EF.Functions.Like(u.Nombre, $"%{request.Nombre}%"));
            }

            if (request.InstitucionId > 0)
            {
                query = query.Where(u => u.InstitucionId == request.InstitucionId);
            }

            return query;
        }

        public IEnumerable<ServicioModel> All()
        {
            var fromRepository = _repoServicio.All().OrderBy(o => o.Nombre);

            var pro = fromRepository.ProjectTo<ServicioModel>(_mapper.ConfigurationProvider);

            return pro.ToList();
        }

        public void Delete(long id)
        {
            Servicio serv = _repoServicio.Get(id);
            serv.ModifiedBy = _usuario.Id;

            _repoServicio.Delete(serv);
            _repoServicio.SaveChanges();
        }

        public IEnumerable<ServicioVerModel> Export(ServicioRequest request)
        {
            throw new NotImplementedException();
        }

        public ServicioModel Get(long id)
        {
            var servicio = _repoServicio.Query().Include(i => i.Institucion).FirstOrDefault(s => s.Id == id);
            if (servicio == null)
            {
                throw new NotFoundException(nameof(servicio));
            }

            return _mapper.Map<ServicioModel>(servicio);
        }

        public Task<ServicioDataModel> GetData(long id)
        {
            throw new NotImplementedException();
        }

        public long Insert(ServicioModel model)
        {
            Servicio paraGuardar = _mapper.Map<Servicio>(model);
            paraGuardar.CreatedBy = _usuario.Id;
            _repoServicio.Insert(paraGuardar);
            return _repoServicio.SaveChanges();
        }

        public PagedList<ServicioListModel> Paged(ServicioRequest request)
        {
            logger.LogInformation("Paged Servicios List");
            var queryModel = Query(request)
                .ProjectTo<ServicioListModel>(_mapper.ConfigurationProvider);
            return _repoServicio.Paged(queryModel, request.Page, request.Size);
        }

        public IEnumerable<ServicioSelectModel> Select()
        {
            throw new NotImplementedException();
        }

        public void Update(ServicioModel model, long id)
        {
            Servicio dataServicio = _mapper.Map<Servicio>(model);

            Servicio dataOriginal = _repoServicio.Query().Where(s => s.Id == id).FirstOrDefault();
            


            dataServicio.Version = dataOriginal.Version;
            dataServicio.CreatedAt = dataOriginal.CreatedAt;
            dataServicio.CreatedBy = dataOriginal.CreatedBy;
            dataServicio.Active = dataOriginal.Active;

            dataServicio.ModifiedBy = _usuario.Id;
            _repoServicio.Update(dataServicio);
            _repoServicio.SaveChanges();
        }

        public void Toogle(long id, long divisionId)
        {
            Servicio servicio = _repoServicio.Query().Where(s => s.Id == id).FirstOrDefault();
            servicio.Version = +servicio.Version;
            servicio.ModifiedBy = _usuario.Id;
            servicio.CompraActiva = !servicio.CompraActiva;
            _repoServicio.Update(servicio);
            _repoServicio.SaveChanges();
            Trazabilidad traza = new Trazabilidad();
            traza.Accion = servicio.CompraActiva ? "Activar" : "Desactivar";
            traza.DivisionId = divisionId;
            traza.NombreTabla = "Servicios";
            traza.Observacion = "Cambio estado de compras";
            trazaService.Add(traza);
        }

        public ServicioVerModel Ver(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServicioModel>> GetServiciosByUserIdAsync(string id)
        {
            var query = _repoServicio.Query().Where(s => s.Active);

            var servicios = await query
                .Where(s => s.UsuariosServicios.Any(u => u.UsuarioId == id))
                .ProjectTo<ServicioModel>(_mapper.ConfigurationProvider).ToListAsync();
            return servicios;
        }

        public async Task<IEnumerable<ServicioModel>> GetServiciosByInstitucionIdAsync(long id)
        {
            var query = _repoServicio.Query().Where(s => s.Active);


            var servicios = await query
                .Where(s => s.InstitucionId == id)
                .ProjectTo<ServicioModel>(_mapper.ConfigurationProvider).ToListAsync();
            return servicios;
        }

        public async Task<IEnumerable<ServicioListModel>> AllByUserAndInstitucion(string userId, long institucionId)
        {
            bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);

            var query = _repoServicio.Query().Where(s => s.Active);


            query = query.Where(s => s.InstitucionId == institucionId && s.Active);
            query = query.Include(us => us.UsuariosServicios).Where(s => isAdmin ? true : s.UsuariosServicios.Any(us => us.UsuarioId == userId));

            return await query.OrderBy(s => s.Nombre).ProjectTo<ServicioListModel>(_mapper.ConfigurationProvider).ToListAsync();

            //var result = await _repoServicio.AllByUserAndInstitucion(userId, institucionId);
            //return result;
        }

        public async Task<IEnumerable<ServicioListModel>> AllByUser(string id)
        {
            var result = await _repoServicio.AllByUser(id);
            return result;
        }

        public void Update(ServicioModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene los servicios segun la institucion pasada por parametro y el usuario autentificado
        /// </summary>
        /// <param name="institucionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServicioModel>> GetByInstitucionId(long institucionId)
        {
            bool isAdmin = true;
            if (_usuario != null)
            {
                isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);
            }
            

            var query = _repoServicio.Query().Where(s => s.Active);

            if (_usuario != null)
            {
                var servicios = await query
                               .Where(c => c.InstitucionId == institucionId)
                               .Include(us => us.UsuariosServicios)
                               .Where(s => isAdmin ? true : s.UsuariosServicios.Any(us => us.UsuarioId == _usuario.Id))
                               .OrderBy(x=>x.Nombre)
                               .ToListAsync();

                return _mapper.Map<IEnumerable<ServicioModel>>(servicios);
            }
            else 
            {
                var servicios = await query
               .Where(c => c.InstitucionId == institucionId)
               .Include(us => us.UsuariosServicios)
               .OrderBy(x => x.Nombre)
               .ToListAsync();

                return _mapper.Map<IEnumerable<ServicioModel>>(servicios);
            }

           
        }

        public async Task<IEnumerable<ServicioModel>> GetByInstitucionIdAndUserId(long institucionId,string userId)
        {
            var usuario = _usuario;
            bool isAdmin = true;

            if (usuario == null) {
                usuario = await _manager.FindByIdAsync(userId);
            }

            isAdmin = await _manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR);

            var query = _repoServicio.Query().Where(s => s.Active);

       
            var servicios = await query
                            .Where(c => c.InstitucionId == institucionId)
                            .Include(us => us.UsuariosServicios)
                            .Where(s => isAdmin ? true : s.UsuariosServicios.Any(us => us.UsuarioId == usuario.Id))
                            .OrderBy(x => x.Nombre)
                            .ToListAsync();

            return _mapper.Map<IEnumerable<ServicioModel>>(servicios);
        }

        /// <summary>
        /// Listar todos los servicios asociados a la institucion Id que no se encuentren asociados al id del usuario enviado por parametro
        /// </summary>
        /// <param name="institucionId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServicioListModel>> GetNoAsociados(string userId, long institucionId)
        {
            

            var query = _repoServicio.Query().Where(s => s.Active);

            if (!_currentUserIsAdmin)
            {
                var serviciosIds = _repoUsuarioServicio.Query().Where(us => us.UsuarioId == _usuario.Id).Select(us => us.ServicioId).ToList();
                query = query.Where(s => serviciosIds.Contains(s.Id));
            }

            query = query.Where(serv => serv.InstitucionId == institucionId && !serv.UsuariosServicios.Any(us => us.UsuarioId == userId)); 
            

            return await query.ProjectTo<ServicioListModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<ServicioListModel>> GetAsociadosByUserId(string userId)
        {
            var query = _repoServicio.Query().Where(s => s.Active);


            return await query.Include(us => us.UsuariosServicios)
                .Where(serv => serv.UsuariosServicios.Any(us => us.UsuarioId == userId))
                .OrderBy(n => n.Nombre).ProjectTo<ServicioListModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<ServicioListModel>> GetNoAsociadosByUserId(string userId)
        {
            var institucionesAsociados = await _servInstitucion.GetAsociados();

            var query = _repoServicio.Query().Where(s => s.Active);

            if (!_currentUserIsAdmin)
            {
                var serviciosIds = _repoUsuarioServicio.Query().Where(us => us.UsuarioId == _usuario.Id).Select(us => us.ServicioId).ToList();
                query = query.Where(s => serviciosIds.Contains(s.Id));
            }

            query = query.Include(us => us.UsuariosServicios).Where(s => 
                    !s.UsuariosServicios.Any(us => us.UsuarioId == userId) 
                &&  institucionesAsociados.Any(i => i.Id == s.InstitucionId)
            );

            var result = await query.OrderBy(n => n.Nombre).ProjectTo<ServicioListModel>(_mapper.ConfigurationProvider).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ServicioModel>> GetServicios()
        {
            var query = _repoServicio.Query().Where(s => s.Active);

            var result = await query.ProjectTo<ServicioModel>(_mapper.ConfigurationProvider).ToListAsync();

            return result;
        }

        public async Task<ServicioModel> GetAsync(long servicioId)
        {
            var query = _repoServicio.Query().Where(s => s.Active);

            var servicio = await query.Include(i => i.Institucion).FirstOrDefaultAsync(s => s.Id == servicioId);
            if (servicio == null)
            {
                throw new NotFoundException(nameof(servicio));
            }

            return _mapper.Map<ServicioModel>(servicio);
        }

        public void Toogle(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsGEV3(Usuario user)
        {
            
            var servicios = await _context.UsuariosServicios.Where(x => x.UsuarioId == user.Id).Select(x=>x.Servicio).ToListAsync();

            if (servicios.Any(x => x.GEV3))
            {
                return true;
            }

            return false;
            
        }
    }
}
