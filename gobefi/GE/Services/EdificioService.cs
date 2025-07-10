using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EdificioModels;
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
    /// <summary>
    /// 
    /// </summary>
    public class EdificioService : IEdificioService
    {
        private readonly IEdificioRepository _repoEdificio;
        private readonly IDivisionRepository _repoDivision;
        private readonly IDivisionService _servDivision;
        private readonly IUsuarioDivisionRepository _repoUsuarioDivision;
        private readonly IComunaRepository _repoComuna;
        private readonly IRegionRepository _repoRegion;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly UserManager<Usuario> _manager;
        private readonly Usuario _usuario;
        private bool _currentUserIsAdmin;

        public EdificioService(
            IEdificioRepository edificioRepository,
            IDivisionRepository repoDivision,
            IDivisionService servDivision,
            IUsuarioDivisionRepository repoUsuarioDivision,
            IComunaRepository repoComuna,
            IRegionRepository repoRegion,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repoEdificio = edificioRepository;
            _repoDivision = repoDivision;
            _servDivision = servDivision;
            _repoUsuarioDivision = repoUsuarioDivision;
            _repoComuna = repoComuna;
            _repoRegion = repoRegion;
            this.logger = loggerFactory.CreateLogger<EdificioService>();
            this.mapper = mapper;
            _manager = manager;
            _usuario = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;

            _currentUserIsAdmin = _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private IQueryable<Edificio> Query(EdificioRequest request)
        {
            bool isAdmin = _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;

            var query = _repoEdificio.Query();

            if (!isAdmin)
            {
                var edificiosIds = _repoUsuarioDivision.Query().Where(ud => ud.UsuarioId == _usuario.Id).Include(e => e.Division).Select(ud => ud.Division.EdificioId).ToList();
                query = query.Where(e => edificiosIds.Contains(e.Id));
            }

            

            if (!string.IsNullOrEmpty(request.Direccion))
            {
                query = query.Where(u => EF.Functions.Like(u.Direccion, $"%{request.Direccion}%"));
            }

            if (request.Id > 0)
            {
                query = query.Where(e => e.Id == request.Id);
            }

            if (request.RegionId > 0)
            {
                query = query.Include(e => e.Comuna);
                query = query.Where(e => e.Comuna.RegionId == request.RegionId);
            }

            if (request.ComunaId > 0)
            {
                query = query.Where(e => e.ComunaId == request.ComunaId);
            }

            return query;
        }

        public PagedList<EdificioListModel> Paged(EdificioRequest request)
        {
            
            var queryModel = Query(request)
                .ProjectTo<EdificioListModel>(mapper.ConfigurationProvider);
            return _repoEdificio.Paged(queryModel, request.Page, request.Size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <returns></returns>
        /// 
        public IEnumerable<EdificioModel> All()
        {
            var fromRepository = _repoEdificio.All().OrderBy(o => o.Direccion);

            var pro = fromRepository.ProjectTo<EdificioModel>(mapper.ConfigurationProvider);

            return pro.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id)
        {
            var edificio = _repoEdificio.Get(id);
            if (edificio == null)
            {
                throw new NotFoundException(nameof(edificio));
            }

            logger.LogInformation($"Edificio: {id} eliminado por usuario: {_usuario.Id}");

            _repoEdificio.Delete(edificio);
            _repoEdificio.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EdificioModel Get(long id)
        {
            var edificio = _repoEdificio.Query().Where(e => e.Id == id)
                .Include(c => c.Comuna)
                .Include(r => r.Comuna.Region)
                .Include(p => p.Comuna.Provincia)
                .FirstOrDefault();

            if (edificio == null)
            {
                throw new NotFoundException(nameof(edificio));
            }

            return mapper.Map<EdificioModel>(edificio);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(EdificioModel model)
        {
            var edificio = mapper.Map<Edificio>(model);

            //var comuna = _repoComuna.Query().Include(r => r.Region).Where(c => c.Id == edificio.ComunaId).FirstOrDefault();

            edificio.Direccion = GenerarDireccion(edificio.Calle, edificio.Numero); //$"{edificio.Calle}, {edificio.Numero}, {comuna.Region.Nombre}";
            edificio.CreatedBy = _usuario.Id;


            _repoEdificio.Insert(edificio);
            int result = _repoEdificio.SaveChanges();

            return edificio.Id;
        }

        public long Insert(EdificioCreateModel model)
        {
            var edificio = mapper.Map<Edificio>(model);


            edificio.Direccion = GenerarDireccion(edificio.Calle, edificio.Numero); //$"{edificio.Calle}, {edificio.Numero}, {comuna.Region.Nombre}";
            edificio.CreatedBy = _usuario.Id;


            _repoEdificio.Insert(edificio);
            int result = _repoEdificio.SaveChanges();

            return edificio.Id;
        }

        private string GenerarDireccion(string calle, string numero)
        {
            return $"{calle} {numero}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Update(EdificioModel model)
        {
            Edificio ed = _repoEdificio.Query().Where(a => a.Id == model.Id).FirstOrDefault();

            var edificioFromModel = mapper.Map<Edificio>(model);

            edificioFromModel.ModifiedBy = _usuario.Id;
            edificioFromModel.CreatedAt = ed.CreatedAt;
            edificioFromModel.CreatedBy = ed.CreatedBy;
            edificioFromModel.Active = ed.Active;
            edificioFromModel.Version = ++ed.Version;

            var comuna = _repoComuna.Query().Include(r => r.Region).Where(c => c.Id == edificioFromModel.ComunaId).FirstOrDefault();
            edificioFromModel.Direccion = GenerarDireccion(edificioFromModel.Calle, edificioFromModel.Numero); //$"{edificioFromModel.Calle}, {edificioFromModel.Numero}, {comuna.Region.Nombre}";

            

            _repoEdificio.Update(edificioFromModel);
            var result = _repoEdificio.SaveChanges();

            if (result > 0)
                _servDivision.UpdateDir(edificioFromModel.Id, edificioFromModel.Calle, edificioFromModel.Numero, comuna.Region.Nombre);

        }

        public long GetComunaByDivision(long divisionId)
        {
            var edificioId = _repoDivision.Get(divisionId).EdificioId;
            var comunaId = _repoEdificio.Get(edificioId).ComunaId;


            return comunaId ?? 0;
        }

        public async Task<IEnumerable<EdificioSelectModel>> getByComunaId(long comunaId)
        {
            var edificios = await _repoEdificio.Query().Where(c => c.ComunaId == comunaId && c.Active).OrderBy(e => e.Direccion).ToListAsync();

            return mapper.Map<IEnumerable<EdificioSelectModel>>(edificios);
        }

        public IEnumerable<EdificioSelectModel> GetEdificiosForSelect()
        {
            var fromRepository = _repoEdificio.All().OrderBy(o => o.Direccion);

            var pro = fromRepository.ProjectTo<EdificioSelectModel>(mapper.ConfigurationProvider);

            return pro.ToList();
        }

        public IEnumerable<EdificioModel> GetActivosByUser()
        {
            var edificiosQuery = _repoEdificio.Query().Where(e => e.Active);


            if (_currentUserIsAdmin)
            {
                var pro = edificiosQuery.OrderBy(o => o.Direccion).ProjectTo<EdificioModel>(mapper.ConfigurationProvider);

                return pro.ToList();
            }

            var edificiosIds = _repoUsuarioDivision.Query().Where(ud => ud.UsuarioId == _usuario.Id).Include(e => e.Division).Select(ud => ud.Division.EdificioId).ToList();
            edificiosQuery = edificiosQuery.Where(e => edificiosIds.Contains(e.Id));


            var pro2 = edificiosQuery.OrderBy(o => o.Direccion).ProjectTo<EdificioModel>(mapper.ConfigurationProvider);

            return pro2.ToList();

        }

        public async Task<EdificioSelectModel> GetForSelect(long id)
        {
            var edificio = await _repoEdificio.Query().Where(e => e.Id == id).FirstOrDefaultAsync();

            if (edificio == null)
            {
                throw new NotFoundException(nameof(edificio));
            }

            return mapper.Map<EdificioSelectModel>(edificio);
        }

        public async Task<IEnumerable<EdificioSelectModel>> getByRegionId(long regionId)
        {
            var region = await _repoRegion.Query().Where(r => r.Id == regionId).Include(r => r.Comunas).FirstOrDefaultAsync();


            List<UsuarioDivision> divisionesAsociadas = null;
            List<Division> divisiones = null;

            if (!_currentUserIsAdmin)
            {
                divisionesAsociadas = await _repoUsuarioDivision.Query().Where(ud => ud.UsuarioId == _usuario.Id).ToListAsync();
                divisiones = await _repoDivision.Query().Where(d => divisionesAsociadas.Any(ud => ud.DivisionId == d.Id)).ToListAsync();
            }
                


            var edificios = await _repoEdificio.Query().Where(e => region.Comunas.Any(c => c.Id == e.ComunaId) &&
            (_currentUserIsAdmin ? true : divisiones.Any(d => d.EdificioId == e.Id))).ToListAsync();


            return mapper.Map<IEnumerable<EdificioSelectModel>>(edificios);
        }
    }
}
