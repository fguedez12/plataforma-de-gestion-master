using api_gestiona.DTOs.Divisiones;
using api_gestiona.DTOs.PlanGestion;
using api_gestiona.DTOs.Servicios;

namespace api_gestiona.Services.Contracts
{
    public interface IPlanGestionService
    {
        Task DeleteAccion(long id, string userId);
        Task DeleteBrecha(long id, string userId);
        Task DeleteIndicador(long id, string userId);
        Task DeleteObjetivo(long id, string userId);
        Task DeletePrograma(long id, string userId);
        Task EditBrecha(long id, BrechaToEditDTO brecha, string userId);
        Task EditIndicador(long id, IndicadorToEditDTO indicador);
        Task EditObjetivo(long id, ObjetivoToEditDTO objetivo, string userId);
        Task EditPrograma(long id, ProgramaToEditDTO model);
        Task<List<AccionListDTO>> GetAcciones(long servicioId, bool includeTareas = false);
        Task<AccionToEditDTO> GetAccionId(long id);
        Task<BrechaToEditDTO> GetBrechaById(long id);
        Task<List<BrechaListDTO>> GetBrechas(long servicioId);
        Task<List<DivisionListDTO>> GetDivisionesByObjetivId(long objetivoId);
        Task<IndicadorToEditDTO> GetIndicadorById(long id);
        Task<List<IndicadorListDTO>> GetIndicadores(long servicioId);
        Task<List<MedidaListDTO>> GetMedidas(long dimId);
        Task<ObjetivoToEditDTO> GetObjetivoById(long id);
        Task<List<ObjetivoListDTO>> GetObjetivos(long servicioId);
        Task<PlanGestionDTO> GetPlanGetion(long servicioId);
        Task<ProgramaToEditDTO> GetProgramaById(long id);
        Task<List<ProgramaListDTO>> GetProgramas(long servicioId);
        Task<List<ServicioListDTO>> GetServicios(long servicioId);
        Task<List<UserListDTO>> GetUserList(long servicioId);
        Task SaveAccion(AccionToSaveDTO model);
        Task SaveBrecha(long servicioId, BrechaToSaveDTO brecha, string userId);
        Task SaveDimensionServicio(DimensionDTO dimension, long servicioId);
        Task SaveIndicador(IndicadorToSaveDTO indicador);
        Task SaveObjetivo(ObjetivoToSaveDTO objetivo, string userId);
        Task SavePrograma(ProgramaToSaveDTO model);
        Task UpdateAccion(long id, AccionToSaveDTO model);
Task UpdatePgaInfo(long id, PgaInfoDTO model);
Task<PgaInfoDTO> GetPgaInfoAsync(long servicioId);

        // Métodos para Tarea
        Task<IEnumerable<TareaListDTO>> GetTareasByServicioIdAsync(long servicioId);
        Task<TareaListDTO> GetTareaByIdAsync(long id);
        Task<long> SaveTareaAsync(TareaToSaveDTO tareaDto);
        Task<bool> EditTareaAsync(long id, TareaToEditDTO tareaDto);
        Task<bool> DeleteTareaAsync(long id, string userId);

        // Métodos para Implementación y Seguimiento
        Task<List<BrechaListDTO>> GetBrechasByObjetivoId(long objetivoId);
        Task<List<AccionListDTO>> GetAccionesConFiltros(long servicioId, long objetivoId, long? dimensionId = null, int? anio = null);
        Task<List<AccionListDTO>> GetAccionesByObjetivo(long objetivoId, int? anio = null, bool includeTareas = false);
    }
}
