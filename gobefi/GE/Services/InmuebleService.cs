using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InmuebleModels;
using GobEfi.Web.Models.PisoModels;
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
    public class InmuebleService : IInmuebleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;

        public InmuebleService(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public async Task<InmuebleModel> GetById(long id)
        {
            var inmuebleFromDb = await _context.Divisiones
                .Where(d=>d.Id== id && d.Active)
                .Include(d=>d.DireccionInmueble)
                .ThenInclude(di => di.Region)
                .Include(d => d.DireccionInmueble)
                .ThenInclude(di => di.Comuna)
                .Include(d => d.AdministracionServicio)
                .ThenInclude(a => a.Institucion)
                .Include(a =>a.UnidadInmuebles)
                .FirstOrDefaultAsync();

            var InmuebleToReturn = _mapper.Map<InmuebleModel>(inmuebleFromDb);


            await SetChild(InmuebleToReturn);
            return InmuebleToReturn;
        }

        private async Task<InmuebleModel> SetChild(InmuebleModel inmueble) 
        {
            if (inmueble .TipoInmueble == 1)
            {
                var listChildFromDb = await _context.Divisiones
                .Where(d => d.ParentId == inmueble.Id && d.Active)
                .Include(d => d.DireccionInmueble)
                .ThenInclude(di => di.Comuna)
                .Include(d => d.DireccionInmueble)
                .ThenInclude(di => di.Region)
                .Include(d => d.AdministracionServicio)
                .ThenInclude(a => a.Institucion)
                .Include(d => d.UnidadInmuebles)
                .ToListAsync();



                var listChild = _mapper.Map<List<InmuebleModel>>(listChildFromDb);

                foreach (var child in listChild)
                {
                    var listPisoFromDb = await _context.Pisos
                    .Where(p => p.DivisionId == child.Id && p.Active)
                    .Include(p => p.NumeroPiso)
                    .Include(p => p.Areas)
                    .ThenInclude(a => a.UnidadAreas)
                    .Include(p => p.UnidadPisos)
                    .ToListAsync();


                    var listPiso = _mapper.Map<List<PisoModel>>(listPisoFromDb);

                    foreach (var piso in listPiso)
                    {
                        foreach (var area in piso.Areas.ToList())
                        {
                            if (area.Active == false) 
                            {
                                piso.Areas.Remove(area);
                            }
                        }
                    }

                    child.Pisos = listPiso;

                }

                inmueble.Children = listChild;
            }

            if (inmueble.TipoInmueble == 2)
            {
                var listPisoFromDb = await _context.Pisos
                .Where(p => p.DivisionId == inmueble.Id && p.Active)
                .Include(p => p.NumeroPiso)
                .Include(p => p.Areas)
                .ThenInclude(a => a.UnidadAreas)
                .Include(p => p.UnidadPisos)
                .ToListAsync();

                var listPiso = _mapper.Map<List<PisoModel>>(listPisoFromDb);

                foreach (var piso in listPiso)
                {
                    foreach (var area in piso.Areas.ToList())
                    {
                        if (area.Active == false)
                        {
                            piso.Areas.Remove(area);
                        }
                    }
                }

                inmueble.Pisos = listPiso.OrderBy(x=>x.PisoNumero).ToList();
            }

            return inmueble;
        }

        public async Task<List<InmuebleModel>> GetByAddress(InmuebleByAddressRequest request)
        {
            var listFromDb = await _context.Direcciones
                .Where(x => x.Calle == request.Calle && x.Numero == request.Numero && x.ComunaId == request.ComunaId)
                .Include(x => x.Inmuebles)
                .ThenInclude(y=>y.DireccionInmueble)
                .ThenInclude(d => d.Region)
                .Include(x => x.Inmuebles)
                .ThenInclude(y => y.DireccionInmueble)
                .ThenInclude(d => d.Comuna)
                .Include(x => x.Inmuebles)
                .ThenInclude(y => y.AdministracionServicio)
                .ThenInclude(a => a.Institucion)
                .FirstOrDefaultAsync();

            if (listFromDb == null) { return new List<InmuebleModel>(); }

            var listToReturn = _mapper.Map<List<InmuebleModel>>(listFromDb.Inmuebles.Where(x=>x.Active));

            foreach (var inm in listToReturn)
            {
                await SetChild(inm);

            }

            return listToReturn;
        }

        public async Task Update(InmuebleToUpdateRequest model)
        {
            try
            {

                var entity = await _context.Divisiones
                .Where(d => d.Id == model.Id)
                .Include(d => d.DireccionInmueble)
                .ThenInclude(di => di.Region)
                .Include(d => d.DireccionInmueble)
                .ThenInclude(di => di.Comuna)
                .Include(d => d.AdministracionServicio)
                .ThenInclude(a => a.Institucion)
                .FirstOrDefaultAsync();


                entity.UpdatedAt = DateTime.Now;
                entity.ModifiedBy = _currentUser.Id;
                entity.Version++;
                entity.TipoInmueble = model.TipoInmueble;
                entity.Nombre = model.Nombre;
                entity.Superficie = model.Superficie;
                entity.TipoUsoId = model.TipoUsoId;
                entity.TipoAdministracionId = model.TipoAdministracionId;
                entity.AdministracionServicioId = model.AdministracionServicioId;
                entity.AnyoConstruccion = model.AnyoConstruccion;
                entity.NroRol = model.NroRol;

                _context.Entry(entity).State = EntityState.Modified;


                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            

        }

        public async Task<InmuebleModel> Create(InmuebleToSaveRequest model)
        {

            if (model.TipoInmueble == 2 && model.ParentId > 0) {

                var parentFromDb = await _context.Divisiones
                    .Include(d => d.DireccionInmueble)
                    .FirstOrDefaultAsync(d => d.Id == model.ParentId);
                var parentAdress = _mapper.Map<DireccionModel>(parentFromDb.DireccionInmueble);
                model.Direccion = parentAdress;
                model.Direccion.Latitud =  Convert.ToDouble(parentFromDb.Latitud);
                model.Direccion.Longitud = Convert.ToDouble(parentFromDb.Longitud);


            }

            if (model.Direccion == null) {
                model.Direccion = new DireccionModel();
            }

            var direccionFromDb = await _context.Direcciones.FirstOrDefaultAsync(x => x.Calle == model.Direccion.Calle 
                && x.Numero == model.Direccion.Numero
                && x.ComunaId == model.Direccion.ComunaId
                );
            var newImueble = new Division() { 
               CreatedAt = DateTime.Now,
               Version = 1,
               Active = true,
               CreatedBy = _currentUser.Id,
               Nombre = model.Nombre,
               EdificioId = 1,
               ServicioId = 3,
               TipoPropiedadId = 1,
               Superficie = model.Superficie,
               TipoUsoId = 1,
               GeVersion = 3,
               TipoAdministracionId = model.TipoAdministracionId,
               TipoInmueble= model.TipoInmueble,
               AdministracionServicioId = model.AdministracionServicioId,
               AnyoConstruccion = model.AnyoConstruccion,
               ParentId = model.ParentId,
               Latitud = model.Direccion.Latitud,
               Longitud = model.Direccion.Longitud,
               NroRol = model.NroRol
            };

            if (direccionFromDb == null)
            {

                var newDireccion = _mapper.Map<Direccion>(model.Direccion);
                _context.Direcciones.Add(newDireccion);
                await _context.SaveChangesAsync();
                newImueble.DireccionInmuebleId = newDireccion.Id;
            }
            else {
                newImueble.DireccionInmuebleId = direccionFromDb.Id;
            }

            try
            {
                _context.Divisiones.Add(newImueble);
                await _context.SaveChangesAsync();
                var inmuebleObj = await GetById(newImueble.Id);
                return _mapper.Map<InmuebleModel>(inmuebleObj);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

           
        }

        public async Task<List<UnidadModel>> GetUnidades(long id) 
        {
            var inmueble = await _context.Divisiones
                .Where(d => d.Id == id && d.Active)
                .Include(d => d.UnidadInmuebles)
                .ThenInclude(u => u.Unidad)
                .FirstOrDefaultAsync();
            var unidades = new List<Unidad>();
            foreach (var unidad in inmueble.UnidadInmuebles)
            {
                if (unidad.Unidad.Active) {
                    unidades.Add(unidad.Unidad);
                }
            }
               

            return _mapper.Map<List<UnidadModel>>(unidades);

        }

        public async Task AddUnidad(InmuebleUnidadRequest model) 
        {
            var ui = new UnidadInmueble() 
            {
                InmuebleId = model.InmuebleId,
                UnidadId = model.UnidadId
            };

            _context.UnidadesInmuebles.Add(ui);
            await _context.SaveChangesAsync();
        
        }

        public async Task RemUnidad(InmuebleUnidadRequest model)
        {
            var ui = await _context.UnidadesInmuebles
                .Where(uni => uni.InmuebleId == model.InmuebleId && uni.UnidadId == model.UnidadId)
                .FirstOrDefaultAsync();

            _context.UnidadesInmuebles.Remove(ui);
            await _context.SaveChangesAsync();

        }

        public  IQueryable<Division> Query() 
        {
            var query =  _context.Divisiones.Include(i => i.DireccionInmueble).Where(x => x.Active && x.GeVersion == 3).AsQueryable();
            return query;
        }

        public async Task DeleteInmueble(long id)
        {
            var inmFromDb = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == id);
            if (inmFromDb.TipoInmueble == 1)
            {
                inmFromDb.Active = false;
                inmFromDb.ModifiedBy = _currentUser.Id;
                inmFromDb.UpdatedAt = DateTime.Now;
                inmFromDb.Version++;
                _context.Entry(inmFromDb).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var childsFromDb = await _context.Divisiones.Where(x => x.ParentId == id).ToListAsync();
                foreach (var edificio in childsFromDb)
                {
                    await DeleteEdificio(edificio);
                }
            }

            else 
            {
                await DeleteEdificio(inmFromDb);
            }
        }

        private async Task DeleteEdificio(Division inmueble) 
        {
            
            inmueble.Active = false;
            inmueble.ModifiedBy = _currentUser.Id;
            inmueble.UpdatedAt = DateTime.Now;
            inmueble.Version++;
            _context.Entry(inmueble).State = EntityState.Modified;
            var pisosFromDb = await _context.Pisos.Where(x => x.DivisionId == inmueble.Id).ToListAsync();
            foreach (var piso in pisosFromDb)
            {
                piso.Active = false;
                piso.ModifiedBy = _currentUser.Id;
                piso.UpdatedAt = DateTime.Now;
                piso.Version++;
                _context.Entry(piso).State = EntityState.Modified;

                var areasFromDb = await _context.Areas.Where(x => x.PisoId == piso.Id).ToListAsync();
                foreach (var area in areasFromDb)
                {
                    area.Active = false;
                    area.ModifiedBy = _currentUser.Id;
                    area.UpdatedAt = DateTime.Now;
                    area.Version++;
                    _context.Entry(area).State = EntityState.Modified;
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}
