using api_gestiona.DTOs.PlanGestion;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class FiltrosService : IFiltrosService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FiltrosService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FiltrosDTO> GetFiltrosAsync()
        {
            try
            {
                var response = new FiltrosDTO();

                // Obtener dimensiones
                response.Dimensiones = await GetDimensionesAsync();

                // Obtener objetivos
                response.Objetivos = await GetObjetivosAsync();

                // Obtener años disponibles
                response.Anios = await GetAniosDisponiblesAsync();

                return response;
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener filtros: {e.Message}");
            }
        }

        public async Task<List<DimensionFiltroDTO>> GetDimensionesAsync()
        {
            try
            {
                var dimensiones = await _context.DimensionBrechas
                    .OrderBy(d => d.Nombre)
                    .Select(d => new DimensionFiltroDTO
                    {
                        Id = d.Id,
                        Nombre = d.Nombre,
                        NombreNormalizado = d.NombreNormalizado
                    })
                    .ToListAsync();

                return dimensiones;
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener dimensiones: {e.Message}");
            }
        }

        public async Task<List<ObjetivoFiltroDTO>> GetObjetivosAsync(long? dimensionId = null)
        {
            try
            {
                var query = _context.Objetivos.AsQueryable();

                if (dimensionId.HasValue)
                {
                    query = query.Where(o => o.DimensionBrechaId == dimensionId.Value);
                }

                var objetivos = await query
                    .OrderBy(o => o.Titulo)
                    .Select(o => new ObjetivoFiltroDTO
                    {
                        Id = o.Id,
                        DimensionBrechaId = o.DimensionBrechaId,
                        Titulo = o.Titulo,
                        Descripcion = o.Descripcion
                    })
                    .ToListAsync();

                return objetivos;
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener objetivos: {e.Message}");
            }
        }

        public async Task<List<ObjetivoFiltroDTO>> GetObjetivosByServicioDimensionAsync(long servicioId, long? dimensionId = null)
        {
            try
            {
                var query = _context.Objetivos
                    .Where(o => o.Acciones.Any(a => a.AccionServicios.Any(acs => acs.ServicioId == servicioId)));

                if (dimensionId.HasValue)
                {
                    query = query.Where(o => o.DimensionBrechaId == dimensionId.Value);
                }

                var objetivos = await query
                    .OrderBy(o => o.Titulo)
                    .Select(o => new ObjetivoFiltroDTO
                    {
                        Id = o.Id,
                        DimensionBrechaId = o.DimensionBrechaId,
                        Titulo = o.Titulo,
                        Descripcion = o.Descripcion
                    })
                    .Distinct()
                    .ToListAsync();

                return objetivos;
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener objetivos por servicio y dimensión: {e.Message}");
            }
        }

        public async Task<List<int>> GetAniosDisponiblesAsync()
        {
            try
            {
                // Obtener años desde las tareas creadas
                var aniosTareas = await _context.Tareas
                    .Select(t => t.CreatedAt.Year)
                    .Distinct()
                    .ToListAsync();

                // Obtener años desde los objetivos creados
                var aniosObjetivos = await _context.Objetivos
                    .Select(o => o.CreatedAt.Year)
                    .Distinct()
                    .ToListAsync();

                // Combinar y ordenar años únicos
                var aniosDisponibles = aniosTareas
                    .Union(aniosObjetivos)
                    .Where(year => year >= 2020) // Filtrar años relevantes
                    .OrderByDescending(year => year)
                    .ToList();

                // Si no hay años disponibles, incluir el año actual
                if (!aniosDisponibles.Any())
                {
                    aniosDisponibles.Add(DateTime.Now.Year);
                }

                return aniosDisponibles;
            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener años disponibles: {e.Message}");
            }
        }
    }
}