using api_gestiona.DTOs.Divisiones;
using api_gestiona.DTOs.PlanGestion;
using api_gestiona.DTOs.Servicios;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.Security;

namespace api_gestiona.Services
{
    public class PlanGestionService: IPlanGestionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IServicioService _servicioService;
        private readonly IFileService _fileService;

        public PlanGestionService(ApplicationDbContext context, IMapper mapper, IServicioService servicioService, IFileService fileService)
        {
           _context = context;
           _mapper = mapper;
           _servicioService = servicioService;
            _fileService = fileService;
        }
        public async Task<PlanGestionDTO> GetPlanGetion(long servicioId)
        {
            try
            {
                var response = new PlanGestionDTO();
                var servicio = await _context.Servicios.Include(x => x.DimensionServicios).FirstOrDefaultAsync(x => x.Id == servicioId)
                    ?? throw new Exception("no existe el recurso solcitado");
                var dimensiones = await _context.DimensionBrechas.ToListAsync();
                var listDimensiones = _mapper.Map<List<DimensionDTO>>(dimensiones);
                if (servicio.DimensionServicios != null && servicio.DimensionServicios.Count > 0)
                {
                    foreach (var dimension in servicio.DimensionServicios)
                    {
                        var dim = listDimensiones.Where(x => x.Id == dimension.DimensionBrechaId).FirstOrDefault();
                        dim!.IngresoObservacion = dimension.IngresoObservacion;
                        dim!.Observacion = dimension.Observacion;
                        dim!.IngresoObservacionObjetivos = dimension.IngresoObservacionObjetivos;
                        dim!.ObservacionObjetivos = dimension.ObservacionObjetivos;
                        dim!.IngresoObservacionAcciones = dimension.IngresoObservacionAcciones;
                        dim!.ObservacionAcciones = dimension.ObservacionAcciones;
                        dim!.IngresoObservacionIndicadores = dimension.IngresoObservacionIndicadores;
                        dim!.ObservacionIndicadores = dimension.ObservacionIndicadores;
                    }
                }
                response.Dimensiones = listDimensiones;
                return response;
            }
            catch (Exception e)
            {

                throw  new  Exception(e.Message);
            }
            
        }

        public async Task SaveDimensionServicio(DimensionDTO dimension, long servicioId)
        {
            var servicio = await _context.Servicios.Include(x => x.DimensionServicios).FirstOrDefaultAsync(x => x.Id == servicioId)
                ?? throw new Exception("no existe el recurso solcitado");
            if (servicio.DimensionServicios != null && servicio.DimensionServicios.Count > 0)
            {
                var existingDim = servicio.DimensionServicios.FirstOrDefault(x=>x.Id == dimension.Id);
                if (existingDim != null)
                {
                   _mapper.Map(dimension,existingDim);
                    await _context.SaveChangesAsync(); 
                }
                else {
                    var newDim = _mapper.Map<DimensionServicio>(dimension);
                    newDim.ServicioId = servicioId;
                    newDim.Id = 0;
                    _context.DimensionServicios.Add(newDim);
                    _context.SaveChanges();
                }
            }
            else
            {
                var newDim = _mapper.Map<DimensionServicio>(dimension);
                newDim.ServicioId = servicioId;
                newDim.Id = 0;
                _context.DimensionServicios.Add(newDim);
                _context.SaveChanges();

            }
            
        }

        public async Task SaveBrecha(long servicioId, BrechaToSaveDTO brecha, string userId)
        {
            try
            {
               
                var entity = _mapper.Map<Brecha>(brecha);
                entity.ServicioId = servicioId;
                entity.CreatedBy = userId;
                entity.CreatedAt = DateTime.Now;
                entity.Active = true;
                entity.Version = 1;
                _context.Brechas.Add(entity);
                await _context.SaveChangesAsync();
                if (brecha.Unidades != null && brecha.Unidades.Count > 0)
                {
                    foreach (var item in brecha.Unidades)
                    {
                        var brechaUnidad = _mapper.Map<BrechaUnidad>(item);
                        brechaUnidad.BrechaId = entity.Id;
                        _context.BrechaUnidades.Add(brechaUnidad);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }

        public async Task<List<BrechaListDTO>> GetBrechas(long servicioId)
        {
            var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
            if (!exist) 
            {
                throw new Exception("El recurso solicitado no existe");
            }
            var brechas = await _context.Brechas.Where(x=>x.ServicioId == servicioId && x.Active).ToListAsync();
            var response = _mapper.Map<List<BrechaListDTO>>(brechas);
            return response;
        }

        public async Task<BrechaToEditDTO> GetBrechaById(long id)
        {
            var brecha = await _context.Brechas
                .Include(x=>x.BrechaUnidades!)
                .ThenInclude(bu=>bu.Division)
                .FirstOrDefaultAsync(x=>x.Id == id)?? throw new Exception("No se encuentra el recurso solicitado");
            var response = _mapper.Map<BrechaToEditDTO>(brecha);
            if (brecha.BrechaUnidades != null && brecha.BrechaUnidades.Count() > 0)
            {
                var unidades = brecha.BrechaUnidades.Select(x => x.Division).ToList();
                response.Unidades = _mapper.Map<List<DivisionListDTO>>(unidades);
            }

            return response;
        }

        public async Task EditBrecha(long id, BrechaToEditDTO brecha, string userId)
        {
            try
            {
                var brechaFromDb = await _context.Brechas.Include(x=>x.BrechaUnidades).FirstOrDefaultAsync(x => x.Id == id)??throw new Exception("No se encuentra el recurso solcitado");
                if (brechaFromDb.BrechaUnidades != null && brechaFromDb.BrechaUnidades.Count > 0)
                {
                    _context.BrechaUnidades.RemoveRange(brechaFromDb.BrechaUnidades);
                }

                var entityToUpdate = _mapper.Map(brecha, brechaFromDb);
                entityToUpdate.ModifiedBy = userId;
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.Version = brechaFromDb.Version+1;
                _context.Brechas.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                if (brecha.Unidades != null && brecha.Unidades.Count > 0)
                {
                    foreach (var item in brecha.Unidades)
                    {
                        var brechaUnidad = _mapper.Map<BrechaUnidad>(item);
                        brechaUnidad.BrechaId = entityToUpdate.Id;
                        _context.BrechaUnidades.Add(brechaUnidad);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task DeleteBrecha(long id,string userId)
        {
            try
            {
                var brechaFromDb = await _context.Brechas.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No se encuentra el recurso solcitado");
                brechaFromDb.Active = false;
                brechaFromDb.ModifiedBy = userId;
                brechaFromDb.UpdatedAt = DateTime.Now;
                brechaFromDb.Version = brechaFromDb.Version + 1;
                _context.Brechas.Update(brechaFromDb);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task SaveObjetivo(ObjetivoToSaveDTO objetivo, string userId)
        {
            try
            {

                var entity = _mapper.Map<Objetivo>(objetivo);
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = userId;
                entity.Active = true;
                entity.Version = 1;
                _context.Objetivos.Add(entity);
                await _context.SaveChangesAsync();
                if (objetivo.BrechasSelected != null && objetivo.BrechasSelected.Count > 0)
                {
                    foreach (var item in objetivo.BrechasSelected)
                    {
                        var brechaFdb = await _context.Brechas.FirstOrDefaultAsync(x => x.Id == item.Id);
                        if(brechaFdb != null) 
                        {
                            brechaFdb.ObjetivoId = entity.Id;
                            _context.Update(brechaFdb);
                            await _context.SaveChangesAsync();
                        } 
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<List<ObjetivoListDTO>> GetObjetivos(long servicioId)
        {
            var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
            if (!exist)
            {
                throw new Exception("El recurso solicitado no existe");
            }
            var objetivos = await _context.Brechas.Include(x=>x.Objetivo)
                .ThenInclude(objetivo => objetivo!.Brechas)
                .Where(x => x.ServicioId == servicioId && x.Active)
                .Select(x=>x.Objetivo!)
                .Distinct()
                .ToListAsync();
            var objetivosFiltrados = objetivos.Where(objetivo => objetivo != null).ToList();
            var response = _mapper.Map<List<ObjetivoListDTO>>(objetivosFiltrados);
            
            return response;
        }

        public async Task<ObjetivoToEditDTO> GetObjetivoById(long id)
        {
            var objetivo = await _context.Objetivos
                .Include(x => x.Brechas!)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No se encuentra el recurso solicitado");
            var response = _mapper.Map<ObjetivoToEditDTO>(objetivo);
            return response;
        }

        public async Task EditObjetivo(long id, ObjetivoToEditDTO objetivo, string userId)
        {
            try
            {
                var objetivoFromDb = await _context.Objetivos.Include(x => x.Brechas).FirstOrDefaultAsync(x => x.Id == id) 
                    ?? throw new Exception("No se encuentra el recurso solcitado");
                var entityToUpdate = _mapper.Map(objetivo, objetivoFromDb);
                entityToUpdate.Id = id;
                entityToUpdate.ModifiedBy = userId;
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.Version = objetivoFromDb.Version + 1;
                _context.Objetivos.Update(entityToUpdate);
                await _context.SaveChangesAsync();
                foreach (var item in objetivoFromDb.Brechas!)
                {
                    item.ObjetivoId = null;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }

                foreach (var item in objetivo.BrechasSelected!)
                {
                    var brechaFdb = await _context.Brechas.FirstOrDefaultAsync(x => x.Id == item.Id);
                    brechaFdb!.ObjetivoId = id;
                    _context.Update(brechaFdb);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task DeleteObjetivo(long id, string userId)
        {
            try
            {
                var objetivoFromDb = await _context.Objetivos.Include(x=>x.Brechas).FirstOrDefaultAsync(x => x.Id == id) 
                    ?? throw new Exception("No se encuentra el recurso solcitado");
                objetivoFromDb.Active = false;
                objetivoFromDb.ModifiedBy = userId;
                objetivoFromDb.UpdatedAt = DateTime.Now;
                objetivoFromDb.Version = objetivoFromDb.Version + 1;
                _context.Objetivos.Update(objetivoFromDb);
                await _context.SaveChangesAsync();
                foreach (var item in objetivoFromDb.Brechas!)
                {
                    item.ObjetivoId = null;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<List<MedidaListDTO>> GetMedidas(long dimId)
        {
            var medidas = await _context.Medidas.Where(x => x.DimensionbrechaId == dimId).OrderBy(x=>x.Nombre).ToListAsync();
            var response = _mapper.Map<List<MedidaListDTO>>(medidas);
            return response;
        }

        public async Task<List<DivisionListDTO>> GetDivisionesByObjetivId(long objetivoId)
        {
            var list = await _context.Objetivos
                .Where(x=>x.Id == objetivoId)
                .Include(x=>x.Brechas!)
                    .ThenInclude(x=>x.BrechaUnidades!)
                    .ThenInclude(bu=>bu.Division)
                .SelectMany(x => x.Brechas!)
                .SelectMany(b => b.BrechaUnidades!)
                .Select(bu => bu.Division)
                .Distinct()
                .ToListAsync();

            var resp = _mapper.Map<List<DivisionListDTO>>(list);

            return resp;
        }

        public async Task<List<UserListDTO>> GetUserList(long servicioId)
        {
            var entities = await _context.Usuarios
                .Where(x=>x.Active == true)
                .Where(x => x.UsuarioServicio!.Any(x => x.ServicioId == servicioId))
                .OrderBy(x=>x.Nombres).ToListAsync();
            var resp = _mapper.Map<List<UserListDTO>>(entities);
            return resp;
        }

        public async Task<List<ServicioListDTO>> GetServicios(long servicioId)
        {
            var entitites = await _servicioService.GetAll();
            entitites = entitites.Where(x=>x.Id != servicioId).OrderBy(x=>x.Nombre).ToList();
            var response = _mapper.Map<List<ServicioListDTO>>(entitites);
            return response;
        }
        public async Task SaveAccion(AccionToSaveDTO model)
        {
            try
            {
                var entityToSave = _mapper.Map<Accion>(model);
                entityToSave.CreatedAt = DateTime.Now;
                entityToSave.CreatedBy = model.UserId;
                entityToSave.Active = true;
                entityToSave.Version = 1;
                if (model.Adjunto != null)
                {
                    var fileBytes = Convert.FromBase64String(model.Adjunto);
                    var filename = Path.GetFileName(model.AdjuntoNombre);
                    MemoryStream stream = new MemoryStream(fileBytes);
                    IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                    var fileDto = _fileService.SaveFile(file);
                    entityToSave.AdjuntoNombre = fileDto.NombreOriginal;
                    entityToSave.AdjuntoUrl = fileDto.Nombre;
                }
                _context.Acciones.Add(entityToSave);
                await _context.SaveChangesAsync();
                if (model.Unidades != null && model.Unidades.Count > 0)
                {
                    foreach (var item in model.Unidades)
                    {
                        var au = new AccionUnidad { AccionId = entityToSave.Id, DivisionId = item.Id };
                        _context.AccionUnidades.Add(au);
                    }
                    await _context.SaveChangesAsync();
                }
                if (model.Servicios != null && model.Servicios.Count > 0)
                {
                    foreach (var item in model.Servicios)
                    {
                        var acs = new AccionServicio { AccionId = entityToSave.Id, ServicioId = item.Id };
                        _context.AccionServicios.Add(acs);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e )
            {

                throw new Exception(e.Message);
            }
            
        }

        public async Task<List<AccionListDTO>> GetAcciones(long servicioId, bool includeTareas = false)
        {
            var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
            if (!exist)
            {
                throw new Exception("El recurso solicitado no existe");
            }
            try
            {
                
                var objetivos = await _context.Brechas
                .Include(x => x.Objetivo)
                .ThenInclude(x=>x.Acciones!)
                .ThenInclude(a=>a.Medida)
                .Include(x => x.Objetivo)
                .ThenInclude(x=>x.Acciones!)
                .ThenInclude(a=>a.Tareas)
                .Include(x => x.Objetivo)
                .ThenInclude(objetivo => objetivo!.Brechas)
                .Where(x => x.ServicioId == servicioId && x.Active && x.Objetivo != null)
                .Select(x => x.Objetivo!)
                .Distinct()
                .ToListAsync();

                var listAcciones = new List<Accion>();
                foreach (var item in objetivos)
                {
                    foreach (var accion in item.Acciones!)
                    {
                        if (accion.Active)
                        {
                            listAcciones.Add(accion);
                        }
                    }
                }

                listAcciones = listAcciones.Distinct().ToList();
                
                // Mapear las acciones con contexto para incluir tareas si es necesario
                var response = _mapper.Map<List<AccionListDTO>>(listAcciones, opt => {
                    opt.Items["IncludeTareas"] = includeTareas;
                });

                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<AccionToEditDTO> GetAccionId(long id)
        {
            IQueryable<Accion> accionQuery = _context.Acciones
                .Include(x => x.Medida); // Incluir la entidad Medida para el mapeo
                
            if (_context.Acciones.Any(x => x.Id == id && x.AccionServicios != null))
            {
                accionQuery = accionQuery
                    .Include(x => x.AccionServicios!)
                    .ThenInclude(acs => acs.Servicio);
            }
            if (_context.Acciones.Any(x => x.Id == id && x.AcionUnidades != null))
            {
                accionQuery = accionQuery
                    .Include(x => x.AcionUnidades!)
                    .ThenInclude(au => au.Division);
            }

            var accion = await accionQuery
            .FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("No se encuentra el recurso solicitado");

            //var accion = await _context.Acciones
            //    .Include(x => x.AccionServicios)
            //    .ThenInclude(acs=>acs.Servicio)
            //    .Include(x=>x.AcionUnidades!)
            //    .ThenInclude(au=>au.Division)
            //    .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No se encuentra el recurso solicitado");

            var response = _mapper.Map<AccionToEditDTO>(accion);
            var unidades = accion.AcionUnidades!.Select(x=>x.Division).ToList();
            var servicios = accion.AccionServicios!.Select(x => x.Servicio).ToList();
            response.Unidades = _mapper.Map<List<DivisionListDTO>>(unidades);
            response.Servicios = _mapper.Map<List<ServicioListDTO>>(servicios);

            return response;
        }

        public async Task UpdateAccion(long id,AccionToSaveDTO model)
        {
            var accionFromDb = await _context.Acciones
                .Include(x=>x.AcionUnidades)
                .Include(x=>x.AccionServicios).FirstOrDefaultAsync(x => x.Id == id)
                   ?? throw new Exception("No se encuentra el recurso solcitado");
            var entityToUpdate = _mapper.Map(model, accionFromDb);
            entityToUpdate.Id = id;
            entityToUpdate.ModifiedBy = model.UserId;
            entityToUpdate.UpdatedAt = DateTime.Now;
            entityToUpdate.Version = accionFromDb.Version + 1;
            
            if (model.Adjunto is not null)
            {
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoNombre);
                MemoryStream stream = new MemoryStream(fileBytes);
                IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                var fileDto = _fileService.SaveFile(file);
                entityToUpdate.AdjuntoNombre = fileDto.NombreOriginal;
                entityToUpdate.AdjuntoUrl = fileDto.Nombre;
            }

            if (model.Unidades is not null && model.Unidades.Count > 0)
            {
                _context.AccionUnidades.RemoveRange(entityToUpdate.AcionUnidades!);
            }
            if (model.Servicios is not null && model.Servicios.Count > 0)
            {
                _context.AccionServicios.RemoveRange(entityToUpdate.AccionServicios!);
            }

            _context.Acciones.Update(accionFromDb);
            await _context.SaveChangesAsync();
            if (model.Unidades != null && model.Unidades.Count > 0)
            {
                foreach (var item in model.Unidades)
                {
                    var au = new AccionUnidad { AccionId = entityToUpdate.Id, DivisionId = item.Id };
                    _context.AccionUnidades.Add(au);
                }
                await _context.SaveChangesAsync();
            }
            if (model.Servicios != null && model.Servicios.Count > 0)
            {
                foreach (var item in model.Servicios)
                {
                    var acs = new AccionServicio { AccionId = entityToUpdate.Id, ServicioId = item.Id };
                    _context.AccionServicios.Add(acs);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePgaInfo(long id,PgaInfoDTO model)
        {
            var servicioFromDb = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);

            if (servicioFromDb == null)
            {
                throw new Exception("No se encuentra el recurso solicitado");
            }

            // Actualizar solo los campos PgaInfo
            servicioFromDb.PgaRevisionRed = model.PgaRevisionRed;
            servicioFromDb.PgaObservacionRed = model.PgaObservacionRed;
            servicioFromDb.PgaRespuestaRed = model.PgaRespuestaRed;

            // Actualizar campos de auditor�a
            servicioFromDb.ModifiedBy = model.UserId;
            servicioFromDb.UpdatedAt = DateTime.Now;
            // Si la entidad Servicio tiene un campo Version y se requiere su actualizaci�n:
            // servicioFromDb.Version = servicioFromDb.Version + 1;

            _context.Servicios.Update(servicioFromDb);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccion(long id, string userId)
        {
            try
            {
                var accionFromDb = await _context.Acciones
                    .Include(x=>x.Tareas)
                    .FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new Exception("No se encuentra el recurso solcitado");
                accionFromDb.Active = false;
                accionFromDb.ModifiedBy = userId;
                accionFromDb.UpdatedAt = DateTime.Now;
                accionFromDb.Version = accionFromDb.Version + 1;
                _context.Acciones.Update(accionFromDb);

                foreach (var item in accionFromDb.Tareas)
                {
                    item.Active = false;
                    item.ModifiedBy = userId;
                    item.UpdatedAt = DateTime.Now;
                    item.Version = item.Version + 1;
                    _context.Tareas.Update(item);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task SaveIndicador(IndicadorToSaveDTO indicador)
        {
            try
            {
                var entity = _mapper.Map<Indicador>(indicador);
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = indicador.UserId;
                entity.Active = true;
                entity.Version = 1;
                _context.Indicadores.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<IndicadorListDTO>> GetIndicadores(long servicioId)
        {
            var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
            if (!exist)
            {
                throw new Exception("El recurso solicitado no existe");
            }
            var indicadores = await _context.Brechas.Include(x => x.Objetivo)
                .ThenInclude(objetivo => objetivo!.Indicadores)
                .Where(x => x.ServicioId == servicioId && x.Active)
                .Select(x => x.Objetivo!.Indicadores).ToListAsync();

            var listaIndicadores = new List<Indicador>();
            foreach (var item in indicadores)
            {
                if (item is not null)
                {
                    listaIndicadores.AddRange(item);
                }
            }

            listaIndicadores = listaIndicadores.Where(x=>x.Active).Distinct().ToList();
            var response = _mapper.Map<List<IndicadorListDTO>>(listaIndicadores);

            return response;
        }

        public async Task<IndicadorToEditDTO> GetIndicadorById(long id)
        {
            var indicador = await _context.Indicadores
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No se encuentra el recurso solicitado");
            var response = _mapper.Map<IndicadorToEditDTO>(indicador);
            return response;
        }

        public async Task EditIndicador(long id, IndicadorToEditDTO indicador)
        {
            try
            {
                var indicadorFromDb = await _context.Indicadores.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new Exception("No se encuentra el recurso solcitado");
                var entityToUpdate = _mapper.Map(indicador, indicadorFromDb);
                entityToUpdate.Id = id;
                entityToUpdate.ModifiedBy = indicador.UserId;
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.Version = indicadorFromDb.Version + 1;
                _context.Indicadores.Update(entityToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task DeleteIndicador(long id, string userId)
        {
            try
            {
                var entity = await _context.Indicadores.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new Exception("No se encuentra el recurso solcitado");
                entity.Active = false;
                entity.ModifiedBy = userId;
                entity.UpdatedAt = DateTime.Now;
                entity.Version = entity.Version + 1;
                _context.Indicadores.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task SavePrograma(ProgramaToSaveDTO model)
        {
            try
            {
                var entityToSave = _mapper.Map<Programa>(model);
                entityToSave.CreatedAt = DateTime.Now;
                entityToSave.CreatedBy = model.UserId;
                entityToSave.Active = true;
                entityToSave.Version = 1;
                var fileBytes = Convert.FromBase64String(model.Adjunto);
                var filename = Path.GetFileName(model.AdjuntoNombre);
                MemoryStream stream = new MemoryStream(fileBytes);
                IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                var fileDto = _fileService.SaveFile(file);
                entityToSave.AdjuntoNombre = fileDto.NombreOriginal;
                entityToSave.AdjuntoUrl = fileDto.Nombre;
                _context.Programas.Add(entityToSave);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task<List<ProgramaListDTO>> GetProgramas(long servicioId)
        {
            var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
            if (!exist)
            {
                throw new Exception("El recurso solicitado no existe");
            }
            try
            {
                var programas = await _context.Programas.Where(x => x.ServicioId == servicioId && x.Active).ToListAsync();
                var response = _mapper.Map<List<ProgramaListDTO>>(programas);

                return response;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public async Task<ProgramaToEditDTO> GetProgramaById(long id)
        {
            var programa = await _context.Programas
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("No se encuentra el recurso solicitado");
            var response = _mapper.Map<ProgramaToEditDTO>(programa);
            return response;
        }
        public async Task EditPrograma(long id, ProgramaToEditDTO model)
        {
            try
            {

                var programaFromDb = await _context.Programas.FirstOrDefaultAsync(x => x.Id == id)
                   ?? throw new Exception("No se encuentra el recurso solcitado");
                var entityToUpdate = _mapper.Map(model, programaFromDb);
                entityToUpdate.Id = id;
                entityToUpdate.ModifiedBy = model.UserId;
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.Version = programaFromDb.Version + 1;
               
                if (model.Adjunto is not null)
                {
                    var fileBytes = Convert.FromBase64String(model.Adjunto);
                    var filename = Path.GetFileName(model.AdjuntoNombre);
                    MemoryStream stream = new MemoryStream(fileBytes);
                    IFormFile file = new FormFile(stream, 0, fileBytes.Length, filename!, filename!);
                    var fileDto = _fileService.SaveFile(file);
                    entityToUpdate.AdjuntoNombre = fileDto.NombreOriginal;
                    entityToUpdate.AdjuntoUrl = fileDto.Nombre;
                }
                _context.Programas.Update(entityToUpdate);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }

        public async Task DeletePrograma(long id, string userId)
        {
            try
            {
                var entity = await _context.Programas.FirstOrDefaultAsync(x => x.Id == id)
                    ?? throw new Exception("No se encuentra el recurso solcitado");
                entity.Active = false;
                entity.ModifiedBy = userId;
                entity.UpdatedAt = DateTime.Now;
                entity.Version = entity.Version + 1;
                _context.Programas.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
public async Task<PgaInfoDTO> GetPgaInfoAsync(long servicioId)
        {
            var servicio = await _context.Servicios.FindAsync(servicioId);

            if (servicio == null)
            {
                throw new KeyNotFoundException($"Servicio con ID {servicioId} no encontrado.");
            }

            var pgaInfo = new PgaInfoDTO
            {
                PgaRevisionRed = servicio.PgaRevisionRed,
                PgaObservacionRed = servicio.PgaObservacionRed,
                PgaRespuestaRed = servicio.PgaRespuestaRed
            };

            return pgaInfo;
        }

        // M�todos CRUD para Tarea
        public async Task<IEnumerable<TareaListDTO>> GetTareasByServicioIdAsync(long servicioId)
        {
            try
            {
                var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
                if (!exist)
                {
                    throw new Exception("El recurso solicitado no existe");
                }

                var tareas = await _context.Tareas
                    .Include(t => t.DimensionBrecha)
                    .Include(t => t.Accion)
                        .ThenInclude(a => a.Objetivo)
                            .ThenInclude(o => o.Brechas)
                    .Where(t => t.Active &&
                           t.Accion.Objetivo.Brechas.Any(b => b.ServicioId == servicioId && b.Active))
                    .ToListAsync();

                var response = _mapper.Map<List<TareaListDTO>>(tareas);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TareaListDTO> GetTareaByIdAsync(long id)
        {
            try
            {
                var tarea = await _context.Tareas
                    .Include(t => t.DimensionBrecha)
                    .Include(t => t.Accion)
                    .FirstOrDefaultAsync(t => t.Id == id && t.Active)
                    ?? throw new Exception("No se encuentra el recurso solicitado");

                var response = _mapper.Map<TareaListDTO>(tarea);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<long> SaveTareaAsync(TareaToSaveDTO tareaDto)
        {
            try
            {
                // Validar que existan la DimensionBrecha y Accion
                var dimensionExists = await _context.DimensionBrechas.AnyAsync(d => d.Id == tareaDto.DimensionBrechaId);
                if (!dimensionExists)
                {
                    throw new Exception("La dimensi�n especificada no existe");
                }

                var accionExists = await _context.Acciones.AnyAsync(a => a.Id == tareaDto.AccionId && a.Active);
                if (!accionExists)
                {
                    throw new Exception("La acci�n especificada no existe o no est� activa");
                }

                var entity = _mapper.Map<Tarea>(tareaDto);
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = tareaDto.UserId ?? "system";
                entity.Active = true;
                entity.Version = 1;

                _context.Tareas.Add(entity);
                await _context.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> EditTareaAsync(long id, TareaToEditDTO tareaDto)
        {
            try
            {
                var tareaFromDb = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == id && t.Active)
                    ?? throw new Exception("No se encuentra el recurso solicitado");

                // Validar que existan la DimensionBrecha y Accion
                var dimensionExists = await _context.DimensionBrechas.AnyAsync(d => d.Id == tareaDto.DimensionBrechaId);
                if (!dimensionExists)
                {
                    throw new Exception("La dimensi�n especificada no existe");
                }

                var accionExists = await _context.Acciones.AnyAsync(a => a.Id == tareaDto.AccionId && a.Active);
                if (!accionExists)
                {
                    throw new Exception("La acci�n especificada no existe o no est� activa");
                }

                var entityToUpdate = _mapper.Map(tareaDto, tareaFromDb);
                entityToUpdate.Id = id;
                entityToUpdate.ModifiedBy = tareaDto.UserId ?? "system";
                entityToUpdate.UpdatedAt = DateTime.Now;
                entityToUpdate.Version = tareaFromDb.Version + 1;

                _context.Tareas.Update(entityToUpdate);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteTareaAsync(long id, string userId)
        {
            try
            {
                var tareaFromDb = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == id && t.Active)
                    ?? throw new Exception("No se encuentra el recurso solicitado");

                tareaFromDb.Active = false;
                tareaFromDb.ModifiedBy = userId;
                tareaFromDb.UpdatedAt = DateTime.Now;
                tareaFromDb.Version = tareaFromDb.Version + 1;

                _context.Tareas.Update(tareaFromDb);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // M�todos para Implementaci�n y Seguimiento
        public async Task<List<BrechaListDTO>> GetBrechasByObjetivoId(long objetivoId)
        {
            try
            {
                var objetivo = await _context.Objetivos.AnyAsync(x => x.Id == objetivoId);
                if (!objetivo)
                {
                    throw new Exception("El objetivo solicitado no existe");
                }

                var brechas = await _context.Brechas
                    .Where(x => x.ObjetivoId == objetivoId && x.Active)
                    .ToListAsync();

                var response = _mapper.Map<List<BrechaListDTO>>(brechas);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<AccionListDTO>> GetAccionesConFiltros(long servicioId, long objetivoId, long? dimensionId = null, int? anio = null)
        {
            try
            {
                var exist = await _context.Servicios.AnyAsync(x => x.Id == servicioId);
                if (!exist)
                {
                    throw new Exception("El servicio solicitado no existe");
                }

                var objetivoExists = await _context.Objetivos.AnyAsync(x => x.Id == objetivoId);
                if (!objetivoExists)
                {
                    throw new Exception("El objetivo solicitado no existe");
                }

                var query = _context.Acciones
                    .Include(a => a.Medida)
                    .Include(a => a.Tareas)
                    .Include(a => a.Objetivo)
                    .ThenInclude(o => o!.DimensionBrecha)
                    .Include(a => a.AccionServicios)
                    .Where(a => a.AccionServicios!.Any(acs => acs.ServicioId == servicioId) &&
                               a.ObjetivoId == objetivoId &&
                               a.Active);

                // Filtrar por dimensi�n si se proporciona
                if (dimensionId.HasValue)
                {
                    query = query.Where(a => a.Objetivo!.DimensionBrechaId == dimensionId.Value);
                }

                // Filtrar por a�o si se proporciona
                if (anio.HasValue)
                {
                    query = query.Where(a => a.Tareas!.Any(t => t.FechaInicio.Year == anio.Value || t.FechaFin.Year == anio.Value));
                }

                var acciones = await query.ToListAsync();

                var response = _mapper.Map<List<AccionListDTO>>(acciones);
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<AccionListDTO>> GetAccionesByObjetivo(long objetivoId, int? anio = null, bool includeTareas = false)
        {
            try
            {
                var objetivoExists = await _context.Objetivos.AnyAsync(x => x.Id == objetivoId);
                if (!objetivoExists)
                {
                    throw new Exception("El objetivo solicitado no existe");
                }

                var query = _context.Acciones
                    .Include(a => a.Medida)
                    .Include(a => a.Objetivo)
                    .ThenInclude(o => o!.DimensionBrecha)
                    .Include(a => a.AccionServicios)
                    .Where(a => a.ObjetivoId == objetivoId && a.Active);

                // Incluir tareas solo si se solicita
                if (includeTareas)
                {
                    query = query.Include(a => a.Tareas.Where(t => t.Active))
                        .ThenInclude(t => t.DimensionBrecha);
                }               

                // Filtrar por año si se proporciona
                if (anio.HasValue)
                {
                    if (includeTareas)
                    {
                        query = query.Where(a => a.Tareas!.Any(t => t.FechaInicio.Year == anio.Value || t.FechaFin.Year == anio.Value));
                    }
                    else
                    {
                        // Si no incluimos tareas, necesitamos hacer la consulta de manera diferente
                        var accionesConTareas = _context.Acciones
                            .Include(a => a.Tareas)
                            .Where(a => a.ObjetivoId == objetivoId && a.Active &&
                                   a.Tareas!.Any(t => t.FechaInicio.Year == anio.Value || t.FechaFin.Year == anio.Value))
                            .Select(a => a.Id);
                        
                        query = query.Where(a => accionesConTareas.Contains(a.Id));
                    }
                }

                var acciones = await query.ToListAsync();

                if (includeTareas)
                {
                    foreach (var accion in acciones)
                    {
                        if (accion.Tareas != null)
                        {
                            accion.Tareas = accion.Tareas.Where(t => t.Active).ToList();
                        }
                    }
                }

var response = _mapper.Map<List<AccionListDTO>>(acciones, opt =>
    opt.Items["IncludeTareas"] = includeTareas);
return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
