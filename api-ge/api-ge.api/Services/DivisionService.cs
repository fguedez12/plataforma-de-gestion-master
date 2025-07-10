using api_gestiona.DTOs.Divisiones;
using api_gestiona.DTOs.Servicios;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;

namespace api_gestiona.Services
{
    public class DivisionService : IDivisionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        private readonly ILogger logger;

        public DivisionService(ApplicationDbContext context, IMapper mapper,ILoggerFactory loggerFactory)
        {
            _context = context;
            _mapper = mapper;
            logger = loggerFactory.CreateLogger<DivisionService>();
        }
        public async Task<DivisionesResponse> GetByUserId(string id,bool isAdmin)
        {
            var response = new DivisionesResponse();
            if (isAdmin)
            {
                try
                {
                    var entities = await _context.Divisiones.Where(x => x.Active && x.GeVersion == 0).ToListAsync();
                    response.Ok = true;
                    response.Divisiones = _mapper.Map<List<DivisionDTO>>(entities);
                    return response;
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else 
            {
                var servicio = await _context.UsuariosServicios.Where(x => x.UsuarioId == id).Select(x => x.Servicio).FirstOrDefaultAsync();
                if(servicio == null)
                {
                    response.Ok = false;
                    return response;
                }

                var entities = await _context.Divisiones.Where(x => x.Active && x.GeVersion == 0 && x.ServicioId == servicio.Id).ToListAsync();
                response.Ok = true;
                response.Divisiones = _mapper.Map<List<DivisionDTO>>(entities);
                response.Servicio = _mapper.Map<ServicioDTO>(servicio);
                return response;

            }
            
        }

        public async Task<DivisionesResponse> GetByServicioId(long servicioId, string searchText)
        {
            var response = new DivisionesResponse();
            var servicio = await _context.Servicios.Where(x => x.Id == servicioId).FirstOrDefaultAsync();
            if (servicio == null)
            {
                response.Ok = false;
                return response;
            }
            
            var entities = await GetDivisionesByServicio(servicioId, searchText);
            response.Ok = true;
            response.Divisiones = _mapper.Map<List<DivisionDTO>>(entities);
            response.Servicio = _mapper.Map<ServicioDTO>(servicio);
            return response;
        }

        public async Task<List<DivisionListDTO>> GetListByServicioId(long servicioId, string? searchText)
        {
            var servicio = await _context.Servicios
                .Where(x => x.Id == servicioId)
                .FirstOrDefaultAsync() ?? throw new Exception("No se encuentra el recurso solicitado");
           
            var entities = await GetDivisionesByServicio(servicioId, searchText);
            var response = _mapper.Map<List<DivisionListDTO>>(entities);
            return response;
        }


        private async Task<List<Division>> GetDivisionesByServicio(long servicioId, string? searchText)
        {
            var query = _context.Divisiones.Where(x => x.Active && x.GeVersion == 0 && x.ServicioId == servicioId).AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(x => x.Direccion!.Contains(searchText));
            }

            var entities = await query.OrderBy(x=>x.Direccion).ToListAsync();
            return entities;
        }

        public async Task SetInicioGestion(DivisionDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (!string.IsNullOrEmpty(model.AnioInicioGestionEnergetica))
            {
                entity.AnioInicioGestionEnergetica = Convert.ToInt32(model.AnioInicioGestionEnergetica);
            }
            else 
            {
                entity.AnioInicioGestionEnergetica = null;
            }

            await AplicarReglasNegocio(entity, false);
            
            await _context.SaveChangesAsync();
        }

        public async Task SetRestoItems(DivisionDTO model)
        {
            var entity = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (!string.IsNullOrEmpty(model.AnioInicioRestoItems))
            {
                entity.AnioInicioRestoItems = Convert.ToInt32(model.AnioInicioRestoItems);
            }
            else
            {
                entity.AnioInicioRestoItems = null;
            }

            await AplicarReglasNegocio(entity, false);

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
            //division.ReportaPMG = (division.AccesoFactura == 1 && division.AnioInicioGestionEnergetica.HasValue && division.AnioInicioGestionEnergetica.Value <= anioActual);
            //Se quita el AccesoFactura porque no se usa en la regla 2025-06-02
            division.ReportaPMG = division.AnioInicioGestionEnergetica.HasValue && division.AnioInicioGestionEnergetica.Value!=0 && division.AnioInicioGestionEnergetica.Value <= anioActual;
            logger.LogDebug("Regla 1 - ReportaPMG calculado: {ReportaPMG} (AccesoFactura={AccesoFactura}, AnioInicioGestionEnergetica={AnioInicioGestionEnergetica})",
                division.ReportaPMG, division.AccesoFactura, division.AnioInicioGestionEnergetica);

            // Regla 2: IndicadorEE
            division.IndicadorEE = (division.ReportaPMG && !division.ComparteMedidorElectricidad && !division.ComparteMedidorGasCanieria);
            logger.LogDebug("Regla 2 - IndicadorEE calculado: {IndicadorEE} (ReportaPMG={ReportaPMG}, ComparteMedidorElectricidad={ComparteMedidorElectricidad}, ComparteMedidorGasCanieria={ComparteMedidorGasCanieria})",
                division.IndicadorEE, division.ReportaPMG, division.ComparteMedidorElectricidad, division.ComparteMedidorGasCanieria);

            // Regla 3: ReportaEV
            division.ReportaEV = (division.AnioInicioRestoItems.HasValue && division.AnioInicioRestoItems.Value != 0 && division.AnioInicioRestoItems.Value <= anioActual);
            logger.LogDebug("Regla 3 - ReportaEV calculado: {ReportaEV} (AnioInicioRestoItems={AnioInicioRestoItems})",
                division.ReportaEV, division.AnioInicioRestoItems);

            logger.LogInformation("Reglas de negocio aplicadas para División ID: {DivisionId}. Valores finales: ReportaPMG={ReportaPMG}, IndicadorEE={IndicadorEE}, ReportaEV={ReportaEV}",
                division.Id, division.ReportaPMG, division.IndicadorEE, division.ReportaEV);

            return Task.CompletedTask; // Devolvemos Task completado
        }
    }
}
