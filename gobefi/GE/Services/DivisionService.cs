using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PisoModels;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using GobEfi.Web.Models.UnidadModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; // Necesario para FirstOrDefaultAsync
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;
using System; // Necesario para DateTime
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionRepository _repoDivision;
        private readonly IEnergeticoRepository _repoEnergetico;
        private readonly IUnidadMedidaRepository _repoUnidadMedida;
        private readonly IEnergeticoDivisionRepository _repoEnergeticoDivision;
        private readonly IEnergeticoUnidadMedidaRepository _repoEnergeticoUMedida;
        private readonly IServicioService _servServicio;
        private readonly IServicioRepository _repoServicio;
        private readonly IEdificioRepository _repoEdificio;
        private readonly IUsuarioDivisionRepository _repoUsuarioDivision;
        private readonly IUsuarioServicioRepository _repoUsuarioServicio;
        private readonly ILogger logger;
        private readonly UserManager<Usuario> _manager;
        private readonly ApplicationDbContext _context;

        // private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        protected readonly Usuario _usuario;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="mapper"></param>
        /// <param name="divisionRepository"></param>
        public DivisionService(
            ILoggerFactory loggerFactory,
            IMapper mapper,
            IDivisionRepository divisionRepository,
            IEnergeticoRepository repoEnergetico,
            IUnidadMedidaRepository repoUnidadMedida,
            IEnergeticoDivisionRepository repoEnergeticoDivision,
            IEnergeticoUnidadMedidaRepository repoEnergeticoUMedida,
            IServicioService servServicio,
            IServicioRepository repoServicio,
            IEdificioRepository repoEdificio,
            IUsuarioDivisionRepository repoUsuarioDivision,
            IUsuarioServicioRepository repoUsuarioServicio,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext context )
        {
            _repoDivision = divisionRepository;
            _repoEnergetico = repoEnergetico;
            _repoUnidadMedida = repoUnidadMedida;
            _repoEnergeticoDivision = repoEnergeticoDivision;
            _repoEnergeticoUMedida = repoEnergeticoUMedida;
            _servServicio = servServicio;
            _repoServicio = repoServicio;
            _repoEdificio = repoEdificio;
            _repoUsuarioDivision = repoUsuarioDivision;
            _repoUsuarioServicio = repoUsuarioServicio;
            logger = loggerFactory.CreateLogger<DivisionService>();
            _mapper = mapper;
            _manager = manager;
            _context = context;
            _usuario = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private IQueryable<Division> Query(DivisionRequest request)
        {
            bool isAdmin = _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR).Result;
            var query = _repoDivision.Query();

            if (!isAdmin)
            {
                var divisionesIds = _repoUsuarioDivision.Query().Where(ud => ud.UsuarioId == _usuario.Id).Select(ud => ud.DivisionId).ToList();
                query = query.Where(d => divisionesIds.Contains(d.Id));
            }

            if (!string.IsNullOrEmpty(request.Direccion))
            {
                query = query.Where(u => EF.Functions.Like(u.Direccion, $"%{request.Direccion}%"));
            }

            if (!string.IsNullOrEmpty(request.Nombre))
            {
                query = query.Where(u => EF.Functions.Like(u.Nombre, $"%{request.Nombre}%"));
            }

            query = query.Include(d => d.Servicio);
            if (request.InstitucionId > 0)
            {
                query = query.Where(d => d.Servicio.InstitucionId == request.InstitucionId);
            }

            if (request.ServicioId > 0)
            {
                query = query.Where(d => d.ServicioId == request.ServicioId);
            }

            query = query.Where(d => d.Active == request.Activo);
        
            query = query.OrderBy(x => x.Id); // Default sorting

            if (!string.IsNullOrEmpty(request.FieldName))
            {
                switch (request.FieldName)
                {
                    case "id":
                        query = query.OrderBy(x => x.Id);
                        break;
                    case "nombre":
                        query = query.OrderBy(x => x.Nombre);
                        break;
                }
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DivisionModel> All()
        {
            return _repoDivision
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<DivisionModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(long id)
        {
            var division = _repoDivision.Get(id);
            if (division == null)
            {
                throw new NotFoundException(nameof(division));
            }

            division.ModifiedBy = this._usuario.Id;
            _repoDivision.Delete(division);
            _repoDivision.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DivisionModel Get(long id)
        {
            var division = _repoDivision.Query().Where(d => d.Id == id).Include(d => d.Edificio)
                .Include(d => d.Servicio)
                .Include(d => d.TipoPropiedad)
                .Include(d => d.TipoUnidad)
                .Include(d => d.TipoUso).FirstOrDefault();

            if (division == null)
            {
                throw new NotFoundException(nameof(division));
            }

            return _mapper.Map<DivisionModel>(division);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(DivisionModel model)
        {
            var division = _mapper.Map<Division>(model);
            division.CreatedBy = this._usuario.Id;


            _repoDivision.Insert(division);
            int result = _repoDivision.SaveChanges();

            return division.Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Update(DivisionEditInfGeneralModel model)
        {
            var division = _repoDivision.Query().Where(d => d.Id == model.Id).FirstOrDefault();
            if (division == null)
            {
                throw new NotFoundException(nameof(division));
            }

            division.Nombre = model.Nombre;
            division.ModifiedBy = _usuario.Id;

            bool activo = division.Active;
            DateTime creacion = division.CreatedAt;
            string creadoPor = division.CreatedBy;

            _mapper.Map<DivisionEditInfGeneralModel, Division>(model, division);

            division.ModifiedBy = this._usuario.Id;
            division.Active = activo;
            division.CreatedAt = creacion;
            division.CreatedBy = creadoPor;

            var edificio = _repoEdificio.Query().Where(e => e.Id == division.EdificioId).Include(e => e.Comuna).Include(e => e.Comuna.Region).FirstOrDefault();
            division.Direccion = GetDireccionUnidad(edificio, division.Pisos);

            _repoDivision.Update(division);
            _repoDivision.SaveChanges();
        }

        public void DetailsUpdate(DivisionDetailsModel model)
        {
            var division = _repoDivision.Query().Where(d => d.Id == model.Id).FirstOrDefault();
            if (division == null)
            {
                throw new NotFoundException(nameof(division));
            }

            division.NroRol = model.NroRol;
            division.SinRol = model.SinRol;
            division.JustificaRol = model.JustificaRol;
            division.ModifiedBy = _usuario.Id;

            bool activo = division.Active;
            DateTime creacion = division.CreatedAt;
            string creadoPor = division.CreatedBy;

           

            division.ModifiedBy = this._usuario.Id;
            division.Active = activo;
            division.CreatedAt = creacion;
            division.CreatedBy = creadoPor;

            _repoDivision.Update(division);
            _repoDivision.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DivisionVerModel Ver(long id)
        {
            //var divisionModel = divisionRepository
            //    .Query()
            //    .Where(x => x.Id == id).FirstOrDefault();

            //var divisionFromRepository = divisionRepository
            //    .Query()
            //    .Where(x => x.Id == id);

            //var divisionFromRepository = divisionRepository.Get(id);
            var divisionFromRepository = _repoDivision.Query()
                .Where(d => d.Id == id)
                .Include(e => e.Edificio)
                .Include(c => c.Edificio.Comuna)
                .Include(r => r.Edificio.Comuna.Region)
                .Include(p => p.Edificio.Comuna.Provincia)
                .Include(t => t.Edificio.TipoEdificio)
                .Include(tu => tu.TipoUso)
                .Include(tp => tp.TipoPropiedad)
                .Include(d => d.Servicio).FirstOrDefault();

            var division = _mapper.Map<DivisionVerModel>(divisionFromRepository);

            if (divisionFromRepository.AccesoFactura == 2) {
                var servicio = _repoServicio.Get(Convert.ToInt64(divisionFromRepository.ServicioResponsableId));
                division.Responsable = servicio.Nombre;
            }

            if (divisionFromRepository.AccesoFactura == 3) {
                division.Responsable = divisionFromRepository.OrganizacionResponsable;
            }

            // var divisionMapeado = divisionFromRepository.ProjectTo<DivisionVerModel>(mapper.ConfigurationProvider);
            // var division = divisionMapeado.FirstOrDefault();

            //var division = mapper.Map<DivisionVerModel>(divisionModel);

            if (division == null)
            {
                throw new NotFoundException(nameof(division));
            }

            return division;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedList<DivisionListModel>> PagedAsync(DivisionRequest request)
        {
            //logger.LogInformation("Pagina DivisionList");
            //logger.LogInformation("User: {ID}", this.usuario.Id);

            var queryModel = Query(request)
                .ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider); // TODO: determine user role for filtering

            return _repoDivision.Paged(queryModel, request.Page, request.Size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IEnumerable<DivisionVerModel> Export(DivisionRequest request)
        {
            return Query(request).ProjectTo<DivisionVerModel>().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DivisionSelectModel> Select()
        {
            return _repoDivision
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<DivisionSelectModel>()
                .ToList();
        }

        public async Task<IEnumerable<DivisionListModel>> AllByUser(string id)
        {
            bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);
            IQueryable<Division> divisionesIdsDelUsuario = null;

            divisionesIdsDelUsuario = isAdmin ? _repoDivision.Query().Distinct() : _repoUsuarioDivision.Query().Where(ud => ud.UsuarioId == id).Include(ud => ud.Division).Select(d => d.Division).Distinct();

            divisionesIdsDelUsuario = divisionesIdsDelUsuario.Where(d => d.Active);

            return await divisionesIdsDelUsuario.ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task<IEnumerable<DivisionListModel>> AllByUserAndInstitucion(string userId, long institucionId)
        {
            var result = await _repoDivision.AllByUserAndInstitucion(userId, institucionId);
            return result;
        }

        public async Task<IEnumerable<DivisionListModel>> AllByUserAndServicio(string userId, long servicioId)
        {
            bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);

            var query = _repoDivision.Query().Include(ud => ud.UsuariosDivisiones).Where(d =>
            d.ServicioId == servicioId);

            query = query.Where(d => isAdmin ? true : d.UsuariosDivisiones.Any(ud => ud.UsuarioId == userId));

            query = query.Where(d => d.Active);

            return await query.ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();

            //var result = await _repoDivision.AllByUserAndServicio(userId, servicioId);
            //return result;
        }

        public async Task<IEnumerable<DivisionListModel>> GetByFilters(string userId, long servicioId, long? regionId)
        {
            bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);

            var query = _repoUsuarioDivision.Query().Where(ud => isAdmin ? true : ud.UsuarioId == userId).Include(ud => ud.Division).Select(d => d.Division).Distinct();

            query = query.Where(d => d.ServicioId == servicioId && d.Active);

            if (regionId != null)
            {
                query = query.Include(d => d.Edificio);
                query = query.Include(d => d.Edificio.Comuna);
                query = query.Where(d => d.Edificio.Comuna.RegionId == regionId);
            }

            return await query.ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();


            //var query = _repoDivision.Query().Where(d => d.ServicioId == servicioId);
            //query = query.Include(d => d.Edificio).Include(d => d.Edificio.Comuna).Where(d => d.Edificio.Comuna.RegionId == (regionId == null ? d.Edificio.Comuna.RegionId : regionId));

            //query = query.Include(d => d.UsuariosDivisiones).Where(d => isAdmin ? true : d.UsuariosDivisiones.Any(ud => ud.UsuarioId == userId));

            //return await query.ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();

            //var result = await _repoDivision.GetByFilters(userId, servicioId, comunaId);
            //return result;
        }

        public async Task<DivisionModel> GetAsync(long divisionId)
        {
            var divi = await _repoDivision.Query().Include(s => s.Servicio).FirstOrDefaultAsync(d => d.Id == divisionId);

            return _mapper.Map<DivisionModel>(divi);
        }

        public void Update(DivisionModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<long> InsertAsync(DivisionCreateModel division)
        {
            
            var divisionToDB = _mapper.Map<Division>(division);
            divisionToDB.CreatedBy = this._usuario.Id;
            divisionToDB.VehiculosIds = division.VehiculosIds == null ? null : string.Join(",", division.VehiculosIds);
            var edificio = _repoEdificio.Query().Where(e => e.Id == division.EdificioId).Include(e => e.Comuna).Include(e => e.Comuna.Region).FirstOrDefault();

            divisionToDB.Direccion = GetDireccionUnidad(edificio, divisionToDB.Pisos);

            // Aplicar reglas de negocio ANTES de insertar
            await AplicarReglasNegocio(divisionToDB, true);

            _repoDivision.Insert(divisionToDB);
            int result = _repoDivision.SaveChanges();


            // al crear una unidad se asigna a todos los usuarios gestores (consulta y servicio) que tengan asociado el mismo servicio asignado a la unidad recien creada
            var userServices = _repoUsuarioServicio.Query().Where(us => us.ServicioId == divisionToDB.ServicioId);//.ToList();
            var usuarios = userServices.Include(us => us.Usuario).Select(us => us.Usuario).Include(u => u.UsuarioRoles).ToList();

            foreach (Usuario user in usuarios)
            {
                var role = await _manager.GetRolesAsync(user);

                if (role.Contains("GESTOR_SERVICIO") || role.Contains("GESTOR DE CONSULTA"))
                {
                    _repoUsuarioDivision.Insert(new UsuarioDivision { UsuarioId = user.Id, DivisionId = divisionToDB.Id });
                }
                
            }

            
            _repoUsuarioDivision.SaveChanges();


            return divisionToDB.Id;
        }

        private string GetDireccionUnidad(Edificio edificio, string pisos)
        {
            //return $"{edificio.Calle}, Nro. {edificio.Numero}, Pisos {pisos}, {edificio.Comuna.Region.Nombre}";
            return GetDireccionUnidad(edificio.Calle, edificio.Numero, pisos, edificio.Comuna.Region.Nombre);
        } 

        private string GetDireccionUnidad(string calle, string numero, string pisos, string nombreRegion)
        {
            return $"{calle}, Nro. {numero}, Pisos {pisos}, {nombreRegion}";
        } 

        public bool UpdateDir(long edificioId, string edificioCalle, string edificioNumero, string nombreRegion)
        {
            List<Division> divisiones = _repoDivision.Query().Where(d => d.EdificioId == edificioId).ToList();

            foreach (Division itemDivision in divisiones)
            {
                itemDivision.Direccion = GetDireccionUnidad(edificioCalle, edificioNumero, itemDivision.Pisos, nombreRegion);

                _repoDivision.Update(itemDivision);
            }

            try
            {
                _repoDivision.SaveChanges();
            } 
            catch 
            {
                return false;
            }
            
            return true;
            
        }

        public DivisionDeleteModel GetForDelete(long id)
        {
            var division = _repoDivision.Query()
                .Where(d => d.Id == id)
                .Include(e => e.Edificio)
                .Include(s => s.Servicio)
                .Include(tu => tu.TipoUso)
                .Include(tp => tp.TipoPropiedad)
                .FirstOrDefault();

            return _mapper.Map<DivisionDeleteModel>(division);
        }

        public DivisionEditModel GetForEdit(long id)
        {
            var division = _repoDivision.Query()
                .Where(d => d.Id == id)
                .Include(e => e.Edificio)
                .Include(s => s.Servicio)
                .Include(tu => tu.TipoUso)
                .Include(tp => tp.TipoPropiedad)
                .Include(c => c.Edificio.Comuna)
                .FirstOrDefault();
            var result = _mapper.Map<DivisionEditModel>(division);
            result.VehiculosIds = division.VehiculosIds?.Split(',').ToList();


            return result;
        }

        public async Task Update(DivisionEditModel divisionUpdate) // Cambiado de void a async Task
        {
            var division = _repoDivision.Query().Where(d => d.Id == divisionUpdate.Id).FirstOrDefault();
            if (division == null)
            {
                throw new NotFoundException(nameof(division));
            }

            division.Nombre = divisionUpdate.NombreUnidad;

            bool activo = division.Active;
            DateTime creacion = division.CreatedAt;

            _mapper.Map<DivisionEditModel, Division>(divisionUpdate, division);

            division.VehiculosIds = divisionUpdate.VehiculosIds == null ? null : string.Join(",", divisionUpdate.VehiculosIds);
            division.ModifiedBy = this._usuario.Id;
            division.Active = divisionUpdate.Active;
            division.CreatedAt = creacion;
            division.CreatedBy = division.CreatedBy;

            var edificio = _repoEdificio.Query().Where(e => e.Id == division.EdificioId).Include(e => e.Comuna).Include(e => e.Comuna.Region).FirstOrDefault();

            division.Direccion = GetDireccionUnidad(edificio, division.Pisos);

            // Aplicar reglas de negocio ANTES de actualizar
            await AplicarReglasNegocio(division, false);

            _repoDivision.Update(division);
            _repoDivision.SaveChanges();

            logger.LogInformation($"Division [{division.Id}] actualizada por el usuario [{this._usuario.Id}] con reglas aplicadas.");
        }

        /// <summary>
        /// Listar todos las divisiones asociados al servicio Id que no se encuentren asociados al id del usuario enviado por parametro
        /// </summary>
        /// <param name="servicioId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DivisionListModel>> GetNoAsociados(string userId, long servicioId)
        {

            return await _repoDivision.Query().Where(
                divi => divi.ServicioId == servicioId &&
                !divi.UsuariosDivisiones.Any(
                    us => us.UsuarioId == userId)).ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<DivisionListModel>> GetByServicioId(long servicioId)
        {
            var servicios = await _repoDivision.Query().Where(c => c.ServicioId == servicioId).ToListAsync();

            return _mapper.Map<IEnumerable<DivisionListModel>>(servicios);


        }

        public async Task<IEnumerable<DivisionListModel>> GetByEdificioId(long edificioId)
        {
            var servicios = await _repoDivision.Query().Where(c => c.EdificioId == edificioId).ToListAsync();

            return _mapper.Map<IEnumerable<DivisionListModel>>(servicios);


        }

        public async Task<IEnumerable<DivisionListModel>> GetAsociadosByUserId(string userId)
        {
            return await _repoDivision.Query().Include(us => us.UsuariosDivisiones)
                .Where(divi => divi.UsuariosDivisiones.Any(us => us.UsuarioId == userId))
                .OrderBy(n => n.Nombre).ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IEnumerable<DivisionListModel>> GetNoAsociadosByUserId(string userId)
        {
            var serviciosAsociados = await _servServicio.GetAsociadosByUserId(userId);

            return await _repoDivision.Query().Include(us => us.UsuariosDivisiones)
                .Where(divi => !divi.UsuariosDivisiones.Any(us => us.UsuarioId == userId) && serviciosAsociados.Any(i => i.Id == divi.ServicioId))
                .OrderBy(n => n.Nombre).ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<ICollection<DivisionesToAssociate>> GetToAssociate(ICollection<long> serviciosId, string userId)
        {
            bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);

            var fromDB = _repoDivision.Query()
                .Where(d => serviciosId.Contains(d.ServicioId) && d.Active)
                .Include(d => d.UsuariosDivisiones)
                .Where(d => isAdmin ? true : d.UsuariosDivisiones.Any(ud => ud.UsuarioId == _usuario.Id))
                .Include(e => e.Edificio)
                .Include(c => c.Edificio.Comuna)
                .Include(r => r.Edificio.Comuna.Region)
                .OrderBy(n => n.Nombre);

            var data = await fromDB.ProjectTo<DivisionesToAssociate>(_mapper.ConfigurationProvider).ToListAsync();

            foreach (var item in data)
            {
                var division = _repoDivision.Query().Where(d => d.Id == item.Id).Include(u => u.UsuariosDivisiones).FirstOrDefault();


                foreach (var udItem in division.UsuariosDivisiones)
                {
                    var usuario = await _manager.FindByIdAsync(udItem.UsuarioId);

                    var role = await _manager.GetRolesAsync(usuario);

                    if (role.Contains("GESTOR_UNIDAD"))
                        item.NumGestores = ++item.NumGestores;
                }

                //item.NumGestores = division.UsuariosDivisiones.Count.ToString();

                item.MideIntensidadConsumo = division.ReportaPMG ? "checked" : "";

                var exist = division.UsuariosDivisiones.ToList().Exists(a => a.UsuarioId == userId);
                item.UnidadAsociada = exist ? "checked" : "";
            }


            return data;

        }

        public async Task<ICollection<UnidadToAssociate>> GetToAssociatev2(ICollection<long> serviciosId, string userId)
        {
            bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);

            //var fromDB = _repoDivision.Query()
            //    .Where(d => serviciosId.Contains(d.ServicioId) && d.Active)
            //    .Include(d => d.UsuariosDivisiones)
            //    .Where(d => isAdmin ? true : d.UsuariosDivisiones.Any(ud => ud.UsuarioId == _usuario.Id))
            //    .Include(e => e.Edificio)
            //    .Include(c => c.Edificio.Comuna)
            //    .Include(r => r.Edificio.Comuna.Region)
            //    .OrderBy(n => n.Nombre);

            var fromDb = await _context
                                .Unidades.Where(u => serviciosId.Contains(u.ServicioId) && u.Active)
                                .Include(u => u.UsuarioUnidades)
                                .Where(u => isAdmin ? true : u.UsuarioUnidades.Any(uu => uu.UsuarioId == _usuario.Id))
                                .Include(ui => ui.UnidadInmuebles)
                                .ThenInclude(i=>i.Inmueble)
                                .ThenInclude(di=>di.DireccionInmueble)
                                .ThenInclude(r=>r.Region)
                                .ToListAsync();

            var data =  _mapper.Map<List<UnidadToAssociate>>(fromDb);

            foreach (var item in data)
            {
                var unidad = await _context.Unidades.Where(d => d.Id == item.Id).Include(u => u.UsuarioUnidades).FirstOrDefaultAsync();

                //foreach (var udItem in division.UsuariosDivisiones)
                //{
                //    var usuario = await _manager.FindByIdAsync(udItem.UsuarioId);

                //    var role = await _manager.GetRolesAsync(usuario);

                //    if (role.Contains("GESTOR_UNIDAD"))
                //        item.NumGestores = ++item.NumGestores;
                //}

                //item.NumGestores = division.UsuariosDivisiones.Count.ToString();

                item.MideIntensidadConsumo = unidad.ReportaPMG ? "checked" : "";

                var exist = unidad.UsuarioUnidades.ToList().Exists(a => a.UsuarioId == userId);
                item.UnidadAsociada = exist ? "checked" : "";
            }


            return data;

        }

        public async Task<int> AsociarUsuario(string usuarioId, long divisionId)
        {
            var division = await _repoDivision.All().Where(d => d.Id == divisionId).Include(u => u.UsuariosDivisiones).FirstOrDefaultAsync();


            division.UsuariosDivisiones.Add(new UsuarioDivision { UsuarioId = usuarioId, DivisionId = divisionId });

            _repoDivision.SaveChanges();

            var result = _repoDivision.SaveChanges();

            logger.LogInformation($"Division [{division.Id}] asociada al usuario [{usuarioId}]; accion realizada por [{this._usuario.Id}]");

            return result;
        }

        public async Task<int> AsociarUsuariov2(string usuarioId, long unidadId)
        {
            var unidad = await _context.Unidades.Where(d => d.Id == unidadId).Include(u => u.UsuarioUnidades).FirstOrDefaultAsync();


            unidad.UsuarioUnidades.Add(new UsuarioUnidad { UsuarioId = usuarioId, UnidadId = unidadId });

            var result = await _context.SaveChangesAsync();

            logger.LogInformation($"Division [{unidad.Id}] asociada al usuario [{usuarioId}]; accion realizada por [{this._usuario.Id}]");

            return result;
        }

        public bool SePuedenAsociar(string usuarioId, long divisionId)
        {
            long idServicio = _repoDivision.Query().Where(d => d.Id == divisionId).FirstOrDefault().ServicioId;

            var servicio = _repoServicio.Query().Where(s => s.Id == idServicio).Include(us => us.UsuariosServicios).FirstOrDefault();

            return servicio.UsuariosServicios.ToList().Exists(u => u.UsuarioId == usuarioId);
        }

        public async Task<bool> SePuedenAsociarv2(string usuarioId, long unidadId)
        {
            //long idServicio = _repoDivision.Query().Where(d => d.Id == divisionId).FirstOrDefault().ServicioId;

            long idServicio = await _context.Unidades.Where(d => d.Id == unidadId).Select(x=>x.ServicioId).FirstOrDefaultAsync();

            var servicio = _repoServicio.Query().Where(s => s.Id == idServicio).Include(us => us.UsuariosServicios).FirstOrDefault();

            return servicio.UsuariosServicios.ToList().Exists(u => u.UsuarioId == usuarioId);
        }

        public async Task<int> EliminarAsociacionConUsuario(string usuarioId, long divisionId)
        {
            var division = await _repoDivision.All().Where(d => d.Id == divisionId).Include(u => u.UsuariosDivisiones).FirstOrDefaultAsync();


            var asociacion = division.UsuariosDivisiones.Where(u => u.UsuarioId == usuarioId).FirstOrDefault();

            division.UsuariosDivisiones.Remove(asociacion);

            var result = _repoDivision.SaveChanges();

            logger.LogInformation($"Division [{division.Id}] desasociada al usuario [{usuarioId}]; accion realizada por [{this._usuario.Id}]");

            return result;
        }

        public async Task<int> EliminarAsociacionConUsuariov2(string usuarioId, long unidadId)
        {
            var unidad = await _context.Unidades.Where(d => d.Id == unidadId).Include(u => u.UsuarioUnidades).FirstOrDefaultAsync();


            var asociacion = unidad.UsuarioUnidades.Where(u => u.UsuarioId == usuarioId).FirstOrDefault();

            unidad.UsuarioUnidades.Remove(asociacion);

            var result = await _context.SaveChangesAsync();

            logger.LogInformation($"Division [{unidad.Id}] desasociada al usuario [{usuarioId}]; accion realizada por [{this._usuario.Id}]");

            return result;
        }

        public async Task<bool> RoleActualPermiteAsociar()
        {
            IList<string> roles = await _manager.GetRolesAsync(_usuario);

            string[] restricted = { "consulta", "auditor" };

            string role = roles.FirstOrDefault();

            return (checkPermisions(restricted,role));
        }

        private bool checkPermisions(string[] list, string role) {

            var result = true;

            foreach (var item in list)
            {
                if (role.ToLower().Contains(item)) {

                    result = false;
                    break;
                }
            }

            return result;
            
        }

        public async Task<IEnumerable<DivisionListModel>> GetByServicioRegion(long? servicioId, long regionId, bool pmg)
        {
             bool isAdmin = await _manager.IsInRoleAsync(_usuario, Constants.Claims.ES_ADMINISTRADOR);

            var query = _repoUsuarioDivision.Query().Where(ud => isAdmin ? true : ud.UsuarioId == _usuario.Id).Include(ud => ud.Division).Select(d => d.Division).Distinct();

            query = query.Where(d => d.ServicioId == servicioId && d.Active && d.ReportaPMG == pmg);

            if (regionId != 0)
            {
                query = query.Include(d => d.Edificio);
                query = query.Include(d => d.Edificio.Comuna);
                query = query.Where(d => d.Edificio.Comuna.RegionId == regionId);
            }

            return await query.ProjectTo<DivisionListModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task SetPisosIguales(long divisioId, bool value)
        {
            var divisionFromDb = _repoDivision.Get(divisioId);
            divisionFromDb.UpdatedAt = DateTime.Now;
            divisionFromDb.ModifiedBy = _usuario.Id;
            divisionFromDb.Version = ++divisionFromDb.Version;
            divisionFromDb.PisosIguales = value;
            await _repoDivision.SaveChangesAsync();
        }

        //public async Task<List<PisoModel>> GetDivisionPisos(long divisionId)
        //{
        //    var division = await _repoDivision.Query().Where(d => d.Id == divisionId)
        //                        .Include(d => d.DisenioPisos)
        //                        .ThenInclude(dp=>dp.NumeroPiso)
        //                        .Include(d => d.DisenioPisos)
        //                        .ThenInclude(p=>p.Muros)
        //                        .FirstOrDefaultAsync();

        //    var pisosList = division.DisenioPisos.Where(d=>d.Active);

        //    var result = _mapper.Map<List<PisoModel>>(pisosList);


        //    return result;
        //}

        public async Task<UnidadReporteConsumoModel> GetReporteConsumo(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            var isAdmin = await _manager.IsInRoleAsync(user, "ADMINISTRADOR");
            var servicioId =await _context.UsuariosServicios.Where(x => x.UsuarioId == id).Select(x=>x.ServicioId).FirstOrDefaultAsync();

            var result = new UnidadReporteConsumoModel();

            //var unidadesCount =  _context.Divisiones.Where(
            //    x => x.Active &&
            //    x.Servicio.ReportaPMG &&
            //    x.GeVersion == 0
            //    && isAdmin ? true : x.ServicioId == servicioId
            //    ).Count();

            //var unidadesPMGCount = _context.Divisiones.Where(
            //   x => x.Active &&
            //   x.Servicio.ReportaPMG &&
            //   x.GeVersion == 0 &&
            //   x.ReportaPMG == true
            //   && isAdmin ? true : x.ServicioId == servicioId
            //   ).Count();

            var query = _context.Divisiones.Where(
                    x => x.Active &&
                    x.Servicio.ReportaPMG &&
                    x.GeVersion == 0
                ).AsQueryable();

            query = query.Where(x => isAdmin ? true : x.ServicioId == servicioId);

            var unidadesCount = query.Count();

            //HL-20230630 Se quita el filtro para que todas las unidades reporten
            //query = query.Where(x => x.ReportaPMG==false);

            var unidadesPMGCount = unidadesCount - query.Count();

            //var unidades = await _context.Divisiones.Where(
            //    x => x.Active &&
            //    x.Servicio.ReportaPMG &&
            //    x.GeVersion == 0 &&
            //    x.ReportaPMG == false
            //    && isAdmin ? true : x.ServicioId == servicioId 
            //    ).ToListAsync();

            var unidades = await query.ToListAsync();

            result.Total = unidadesCount;
            result.TotalPMG = unidadesPMGCount;
            result.Unidades = _mapper.Map<List<UnidadReporteConsumoDTO>>(unidades);
            return result;
        }

        public async Task JustificaReporteConsumo(JustificaModel model)
        {
            var unidad = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == model.UnidadId);
            unidad.Compromiso2022 = model.Check;
            unidad.EstadoCompromiso2022 = 1;
            unidad.Justificacion = model.Text;
            await _context.SaveChangesAsync();
        }

        public async Task ObservarJustificacion(JustificaModel model, string userId)
        {
            var unidad = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == model.UnidadId);
            unidad.EstadoCompromiso2022 = 2;
            unidad.ObservacionCompromiso2022 = model.ObservacionCompromiso2022;
            unidad.UpdatedAt = DateTime.Now;
            unidad.ModifiedBy = userId;
            await _context.SaveChangesAsync();
        }
        public async Task ValidarJustificacion(JustificaModel model, string userId)
        {
            var unidad = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == model.UnidadId);
            unidad.EstadoCompromiso2022 = 3;
            unidad.UpdatedAt = DateTime.Now;
            unidad.ModifiedBy = userId;
            await _context.SaveChangesAsync();
        }

        public Task AplicarReglasNegocio(Division division, bool esCreacion) // Quitamos async
        {
            int anioActual = DateTime.Now.Year;
            logger.LogInformation("Aplicando reglas de negocio para División ID: {DivisionId}. Es Creación: {EsCreacion}, Año Actual: {AnioActual}", division.Id, esCreacion, anioActual);

            if (!esCreacion)
            {
                logger.LogInformation("Operación de Edición detectada. Verificando condiciones...");
                // Usamos .Result - PRECAUCIÓN: Riesgo potencial de deadlock en escenarios complejos.
                var ajuste = _context.Ajustes.FirstOrDefaultAsync().Result;

                if (ajuste == null)
                {
                    logger.LogWarning("No se encontró registro en la tabla Ajustes. No se aplicarán reglas de edición.");
                    return Task.CompletedTask; // Devolvemos Task
                }

                if (!ajuste.EditUnidadPMG)
                {
                    logger.LogInformation("Ajuste EditUnidadPMG es false. No se aplicarán reglas de edición.");
                    return Task.CompletedTask; // Devolvemos Task
                }
                logger.LogInformation("Ajuste EditUnidadPMG es true.");

                if (division.CreatedAt.Year != anioActual)
                {
                    logger.LogInformation("El año de creación ({AnioCreacion}) no coincide con el año actual ({AnioActual}). No se aplicarán reglas de edición.", division.CreatedAt.Year, anioActual);
                    return Task.CompletedTask; // Devolvemos Task
                }
                logger.LogInformation("El año de creación ({AnioCreacion}) coincide con el año actual ({AnioActual}). Procediendo a aplicar reglas.", division.CreatedAt.Year, anioActual);
            }
            else
            {
                logger.LogInformation("Operación de Creación detectada. Aplicando reglas...");
            }

            // Aplicar Reglas
            // Regla 1: ReportaPMG
            division.ReportaPMG = division.AnioInicioGestionEnergetica.HasValue && division.AnioInicioGestionEnergetica.Value <= anioActual;
            logger.LogDebug("Regla 1 - ReportaPMG calculado: {ReportaPMG} , AnioInicioGestionEnergetica={AnioInicioGestionEnergetica})",
                division.ReportaPMG, division.AnioInicioGestionEnergetica);

            // Regla 2: IndicadorEE
            division.IndicadorEE = (division.ReportaPMG && !division.ComparteMedidorElectricidad && !division.ComparteMedidorGasCanieria);
            logger.LogDebug("Regla 2 - IndicadorEE calculado: {IndicadorEE} (ReportaPMG={ReportaPMG}, ComparteMedidorElectricidad={ComparteMedidorElectricidad}, ComparteMedidorGasCanieria={ComparteMedidorGasCanieria})",
                division.IndicadorEE, division.ReportaPMG, division.ComparteMedidorElectricidad, division.ComparteMedidorGasCanieria);

            // Regla 3: ReportaEV
            division.ReportaEV = (division.AnioInicioRestoItems.HasValue && division.AnioInicioRestoItems.Value <= anioActual);
            logger.LogDebug("Regla 3 - ReportaEV calculado: {ReportaEV} (AnioInicioRestoItems={AnioInicioRestoItems})",
                division.ReportaEV, division.AnioInicioRestoItems);

            logger.LogInformation("Reglas de negocio aplicadas para División ID: {DivisionId}. Valores finales: ReportaPMG={ReportaPMG}, IndicadorEE={IndicadorEE}, ReportaEV={ReportaEV}",
                division.Id, division.ReportaPMG, division.IndicadorEE, division.ReportaEV);

            return Task.CompletedTask; // Devolvemos Task completado
        }
    }
}
