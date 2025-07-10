using AutoMapper;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs.UnidadesDTO;
using GobEfi.Web.Models.InmuebleModels;
using GobEfi.Web.Models.UnidadModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class UnidadService : IUnidadService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;
        private bool _currentUserIsAdmin;
        public UnidadService(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            if (_currentUser != null)
            {
                _currentUserIsAdmin = _userManager.IsInRoleAsync(_currentUser, Constants.Claims.ES_ADMINISTRADOR).Result;
            }
        }
        public async Task<UnidadModel> Create(UnidadToSave model)
        {
            var newUnidad = new Unidad() { 
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = _currentUser.Id,
                Nombre =  model.Nombre,
                ServicioId = model.ServicioId,
                Funcionarios = model.Funcionarios,
                ReportaPMG = model.ReportaPMG,
                IndicadorEE = model.IndicadorEE,
                AccesoFactura = model.AccesoFactura,
                InstitucionResponsableId = model.InstitucionResponsableId,
                ServicioResponsableId = model.ServicioResponsableId,
                OrganizacionResponsable = model.OrganizacionResponsable,
                Version = 1
            };

            try
            {
                _context.Unidades.Add(newUnidad);
                if (model.Inmuebles[0].Id > 0) {
                    var newUnidadInmueble = new UnidadInmueble()
                    {
                        UnidadId = newUnidad.Id,
                        InmuebleId = model.Inmuebles[0].Id
                    };

                    _context.UnidadesInmuebles.Add(newUnidadInmueble);
                }

                foreach (var edficio in model.Inmuebles[0].Edificios)
                {
                    var newUnidadEdificio = new UnidadInmueble()
                    {
                        UnidadId = newUnidad.Id,
                        InmuebleId = edficio.Id
                    };
                   
                    _context.UnidadesInmuebles.Add(newUnidadEdificio);

                    foreach (var piso in edficio.Pisos)
                    {
                        var newUnidadPiso = new UnidadPiso() 
                        { 
                            UnidadId = newUnidad.Id,
                            PisoId = piso.Id
                        };
                        
                        _context.UnidadesPisos.Add(newUnidadPiso);
                        foreach (var area in piso.Areas)
                        {
                            var newUnidadArea = new UnidadArea()
                            {
                                UnidadId = newUnidad.Id,
                                AreaId = area.Id
                            };
                            
                            _context.UnidadesAreas.Add(newUnidadArea);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return _mapper.Map<UnidadModel>(newUnidad);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Update(long id,UnidadToUpdate model)
        {
            var unidadFDB = await _context.Unidades
                .Include(x=>x.UnidadInmuebles)
                .Include(x=>x.UnidadAreas)
                .Include(x=>x.UnidadPisos)
                .FirstOrDefaultAsync(x => x.Id == id);
            unidadFDB.OldId = model.OldId;
            unidadFDB.Nombre = model.Nombre;
            unidadFDB.ServicioId = model.ServicioId;
            unidadFDB.Funcionarios = model.Funcionarios;
            unidadFDB.ReportaPMG = model.ReportaPMG;
            unidadFDB.IndicadorEE = model.IndicadorEE;
            unidadFDB.AccesoFactura = model.AccesoFactura;
            unidadFDB.InstitucionResponsableId = model.InstitucionResponsableId;
            unidadFDB.ServicioResponsableId = model.ServicioResponsableId;
            unidadFDB.OrganizacionResponsable = model.OrganizacionResponsable;
            unidadFDB.UpdatedAt = DateTime.Now;
            unidadFDB.ModifiedBy = _currentUser.Id;
            unidadFDB.Version = unidadFDB.Version++;

            _context.UnidadesInmuebles.RemoveRange(unidadFDB.UnidadInmuebles);
            _context.UnidadesPisos.RemoveRange(unidadFDB.UnidadPisos);
            _context.UnidadesAreas.RemoveRange(unidadFDB.UnidadAreas);

            try
            {
                if (model.Inmuebles[0].Id > 0)
                {
                    var newUnidadInmueble = new UnidadInmueble()
                    {
                        UnidadId = unidadFDB.Id,
                        InmuebleId = model.Inmuebles[0].Id
                    };

                    _context.UnidadesInmuebles.Add(newUnidadInmueble);
                }

                foreach (var edficio in model.Inmuebles[0].Edificios)
                {
                    var newUnidadEdificio = new UnidadInmueble()
                    {
                        UnidadId = unidadFDB.Id,
                        InmuebleId = edficio.Id
                    };

                    _context.UnidadesInmuebles.Add(newUnidadEdificio);

                    foreach (var piso in edficio.Pisos)
                    {
                        var newUnidadPiso = new UnidadPiso()
                        {
                            UnidadId = unidadFDB.Id,
                            PisoId = piso.Id
                        };

                        _context.UnidadesPisos.Add(newUnidadPiso);
                        foreach (var area in piso.Areas)
                        {
                            var newUnidadArea = new UnidadArea()
                            {
                                UnidadId = unidadFDB.Id,
                                AreaId = area.Id
                            };

                            _context.UnidadesAreas.Add(newUnidadArea);
                        }
                    }
                }

                await _context.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Unidad> Query()
        {
            var query = _context.Unidades.Where(x => x.Active).AsQueryable();
            return query;
        }

        public async Task DeleteUnidad(Unidad unidad) 
        {
            unidad.Active = false;
            _context.Entry(unidad).State = EntityState.Modified;
            var unidadAreaList = await _context.UnidadesAreas.Where(x => x.UnidadId == unidad.Id).ToListAsync();
            _context.RemoveRange(unidadAreaList);
            var unidadPisoList = await _context.UnidadesPisos.Where(x => x.UnidadId == unidad.Id).ToListAsync();
            _context.RemoveRange(unidadPisoList);
            var unidadInmuebleList = await _context.UnidadesInmuebles.Where(x => x.UnidadId == unidad.Id).ToListAsync();
            _context.RemoveRange(unidadInmuebleList);

            await _context.SaveChangesAsync();
        }


        public async Task<List<UnidadListDTO>> GetAsociadosByUserId(string userId)
        {
            var unidades =  await _context.Unidades
                .Include(x => x.UnidadAreas)
                .ThenInclude(ua=>ua.Area)
                .Include(x => x.UnidadInmuebles)
                .ThenInclude(ui => ui.Inmueble)
                .Include(x => x.UnidadPisos)
                .ThenInclude(up => up.Piso)
                .ThenInclude(p => p.NumeroPiso)
                .Include(x=>x.Servicio)
                .ThenInclude(s=>s.Institucion)
                .Where(x=>x.Active &&  _currentUserIsAdmin ? true: x.UsuarioUnidades.Any(us=>us.UsuarioId==userId))
                .OrderBy(x=>x.Nombre)
                .ToListAsync();

            var list = _mapper.Map<List<UnidadListDTO>>(unidades);

            foreach (var item in list)
            {
                var institucion = await _context.Instituciones.FirstOrDefaultAsync(x => x.Id == item.InstitucionResponsableId);
                if (institucion != null)
                {
                    item.InstitucionResponsableNombre = institucion.Nombre;
                }
                
                var servicio = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == item.ServicioResponsableId);
                if (servicio != null)
                {
                    item.ServicioResponsableNombre = servicio.Nombre;
                }
                
            }



            return list;

        }

        public async Task<List<UnidadListDTO>> GetbyFilter(string userId, long? institucionId,long? servicioId, long? regionId)
        {
            var query =  _context.Unidades
                .Include(x => x.UnidadAreas)
                .ThenInclude(ua => ua.Area)
                .Include(x => x.UnidadInmuebles)
                .ThenInclude(ui => ui.Inmueble)
                .Include(x => x.UnidadPisos)
                .ThenInclude(up => up.Piso)
                .ThenInclude(p => p.NumeroPiso)
                .Include(x=>x.Servicio)
                .ThenInclude(s=>s.Institucion)
                .Where(x => x.Active && _currentUserIsAdmin ? true : x.UsuarioUnidades.Any(us => us.UsuarioId == userId))
                .OrderBy(x => x.Nombre)
                .AsQueryable();

            if (institucionId!=null) {
                query = query.Where(x => x.Servicio.Institucion.Id == institucionId);
            }

            if (servicioId != null)
            {
                query = query.Where(x => x.ServicioId== servicioId);
            }

            if (regionId != null)
            {
                var inmueblesId = await _context.Divisiones
                    .Include(x => x.DireccionInmueble)
                    .Include(x=>x.UnidadInmuebles)
                    .Where(x => x.DireccionInmueble.RegionId == regionId)
                    .Select(x=>x.Id)
                    .ToArrayAsync();
                var unidadesId = await _context.UnidadesInmuebles.Where(x => inmueblesId.Contains(x.InmuebleId)).Select(x => x.UnidadId).ToArrayAsync(); 
                query = query.Where(x => unidadesId.Contains(x.Id));

            }

            var unidades = await query.ToListAsync();

            return _mapper.Map<List<UnidadListDTO>>(unidades);

        }

        public async Task<bool> CheckUnidadNombre(string nombre, long servicioId)
        {
            var unidad = await _context.Unidades.Where(x => x.Nombre == nombre && x.ServicioId == servicioId &&x.Active).FirstOrDefaultAsync();
            if (unidad == null)
            {
                return false;
            }

            return true;
        }

        public async Task<UnidadDTO> Get(long unidadId)
        {
            var unidadFdb = await _context.Unidades
                .Include(x=>x.UnidadInmuebles)
                .ThenInclude(ui=>ui.Inmueble)
                .ThenInclude(i=>i.DireccionInmueble)
                .ThenInclude(di=>di.Comuna)
                .Include(x => x.UnidadInmuebles)
                .ThenInclude(ui => ui.Inmueble)
                .ThenInclude(i => i.DireccionInmueble)
                .ThenInclude(di=>di.Region)
                .Include(x => x.UnidadPisos)
                .ThenInclude(up=>up.Piso)
                .Include(i => i.UnidadAreas)
                .ThenInclude(ua=>ua.Area)
                .Include(x=>x.Servicio)
                .ThenInclude(s=>s.Institucion)
                .FirstOrDefaultAsync(x => x.Id == unidadId && x.Active);
            var response = new UnidadDTO();

            response = _mapper.Map<UnidadDTO>(unidadFdb);
             
            return response;
        }

        public async Task<bool> HasInteligentMeasurement(long unidadId)
        {
            var newUnidad = await _context.Unidades.Where(x => x.OldId == unidadId).FirstOrDefaultAsync();
            if (newUnidad == null)
            {
                return false;
            }

            return true;
        }
    }
}
