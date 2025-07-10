using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
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
    public class InstitucionService : IInstitucionService
    {
        private IInstitucionRepository _repoInstitucion;
        private ILogger _logger;
        private IMapper _mapper;
        protected readonly UserManager<Usuario> _userManager;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly Usuario _usuario;
        private bool _currentUserIsAdmin;

        public InstitucionService(
            IInstitucionRepository repository,
            ILoggerFactory factory,
            IMapper mapper,
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repoInstitucion = repository;
            _logger = factory.CreateLogger<InstitucionService>();
            _mapper = mapper;
            _userManager = userManager;

            _usuario = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            if (_usuario != null)
            {
                _currentUserIsAdmin = _userManager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;
            }

           
        }

        private IQueryable<Institucion> Query(InstitucionRequest request)
        {
            var query = _repoInstitucion.Query();

            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(u => EF.Functions.Like(u.Nombre, $"%{request.Nombre}%"));
            }

            query.OrderBy(o => o.Nombre);
            return query;
        }

        public PagedList<InstitucionListModel> Paged(InstitucionRequest request)
        {
            var queryModel = Query(request)
                .ProjectTo<InstitucionListModel>(_mapper.ConfigurationProvider);
            return _repoInstitucion.Paged(
                queryModel,
                request.Page,
                request.Size);
        }

        public IEnumerable<InstitucionModel> All()
        {
            return _repoInstitucion
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<InstitucionModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task<IEnumerable<InstitucionListModel>> AllByUser(string id)
        {
            var result = await _repoInstitucion.AllByUser(id);
            return result.OrderBy(i => i.Nombre);
        }

        public void Delete(long id)
        {
            var paraEliminar = _repoInstitucion.Get(id);
            paraEliminar.ModifiedBy = _usuario.Id;
            _repoInstitucion.Delete(paraEliminar);
            _repoInstitucion.SaveChanges();
        }

        public InstitucionModel Get(long id)
        {
            var institucion = _repoInstitucion.Get(id);

            if (institucion == null)
            {
                throw new NotFoundException(nameof(institucion));
            }

            var InstitucionParaRetornar = _mapper.Map<InstitucionModel>(institucion);


            return InstitucionParaRetornar;
        }

        long IService<InstitucionModel, long>.Insert(InstitucionModel model)
        {
            var institucion = _mapper.Map<Institucion>(model);
            institucion.CreatedBy = _usuario.Id;

            _repoInstitucion.Insert(institucion);

            return _repoInstitucion.SaveChanges();
        }

        public void Update(InstitucionModel model)
        {
            var institucionFromModel = _mapper.Map<Institucion>(model);

            institucionFromModel.ModifiedBy = _usuario.Id;

            _repoInstitucion.Update(institucionFromModel);
            _repoInstitucion.SaveChanges();

        }

        /// <summary>
        /// Obtiene las instituciones segun el id del usuario enviado por parametro, utilizado para la edicion de distintos usuarios
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InstitucionListModel>> GetNoAsociadosByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            bool isAdmin = await _userManager.IsInRoleAsync(user, Constants.Claims.ES_ADMINISTRADOR);

            if (isAdmin)
            {
                // asume que tiene todos los servicios asociados
                return new List<InstitucionListModel>();
            }

            var query = _repoInstitucion.Query().Where(i => i.Active);

            query = query.Include(ur => ur.UsuariosInstituciones).Where(i => _currentUserIsAdmin ? true : i.UsuariosInstituciones.Any(ui => ui.UsuarioId == _usuario.Id));
            query = query.Where(i => !i.UsuariosInstituciones.Any(ui => ui.UsuarioId == userId));


            var instituciones = await query.OrderBy(o => o.Nombre).ProjectTo<InstitucionListModel>(_mapper.ConfigurationProvider).ToListAsync();

            return instituciones;
        }

        /// <summary>
        /// Obtiene las instituciones asociadas segun el usuario id enviado por parametro
        /// </summary>
        /// <param name="userId">Id del usuario que desea obtener las instituciones</param>
        /// <returns></returns>
        public async Task<IEnumerable<InstitucionListModel>> GetAsociadosByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            

            bool isAdmin = await _userManager.IsInRoleAsync(user, Constants.Claims.ES_ADMINISTRADOR);

            if (isAdmin)
            {
                // asume que tiene todos los servicios asociados
                return new List<InstitucionListModel>();
            }

            var query = _repoInstitucion.Query().Where(i => i.Active);

            query = query.Include(ur => ur.UsuariosInstituciones).Where(i => isAdmin ? true : i.UsuariosInstituciones.Any(ui => ui.UsuarioId == user.Id));
            query = query.Where(i => i.UsuariosInstituciones.Any(ui => ui.UsuarioId == userId));
            
            var list = await query.OrderBy(o => o.Nombre).ToListAsync();
            
           
            

            var instituciones = await query.OrderBy(o => o.Nombre).ProjectTo<InstitucionListModel>(_mapper.ConfigurationProvider).ToListAsync();

            return instituciones;
        }


        /// <summary>
        /// Obtiene las instituciones del usuario actual
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<InstitucionListModel>> GetAsociados()
        {
            var query = _repoInstitucion.Query().Where(i => i.Active);

            if (_currentUserIsAdmin)
            { 
                // como es administador el sistema asume que tiene todas las intituciones asociadas 
                return await query.ProjectTo<InstitucionListModel>(_mapper.ConfigurationProvider).OrderBy(n => n.Nombre).ToListAsync();
            }

            query = query.Include(ur => ur.UsuariosInstituciones).Where(i => i.UsuariosInstituciones.Any(ui => ui.UsuarioId == _usuario.Id));

            var instituciones = await query.OrderBy(o => o.Nombre).ProjectTo<InstitucionListModel>(_mapper.ConfigurationProvider).ToListAsync();



            return instituciones;
        }

        /// <summary>
        /// Obtiene las instituciones segun el usuario autentificado
        /// </summary>
        /// <returns>Lista de instituciones</returns>
        [Obsolete("Utilize GetAsociados()")]
        public async Task<IEnumerable<InstitucionModel>> GetInstituciones()
        {
            bool isAdmin = true;
            if (_usuario != null) 
            {
                isAdmin = await _userManager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);
            }
            

            var query = _repoInstitucion.Query().Where(i => i.Active);

            if (_usuario != null)
            {
                var result = await query
                .Include(i => i.UsuariosInstituciones)
                .Where(i => isAdmin ? true : i.UsuariosInstituciones.Any(ui => ui.UsuarioId == _usuario.Id))
                .OrderBy(i => i.Nombre)
                .ProjectTo<InstitucionModel>(_mapper.ConfigurationProvider).ToListAsync();
                return result;
            }
            else
            {
                var result = await query
                .Include(i => i.UsuariosInstituciones)
                .OrderBy(i => i.Nombre)
                .ProjectTo<InstitucionModel>(_mapper.ConfigurationProvider).ToListAsync();
                return result;
            }
            

            

        }

    }
}
