using api_gestiona.DTOs.PlanGestion;

namespace api_gestiona.Services.Contracts
{
    public interface IFiltrosService
    {
        Task<FiltrosDTO> GetFiltrosAsync();
        Task<List<DimensionFiltroDTO>> GetDimensionesAsync();
        Task<List<ObjetivoFiltroDTO>> GetObjetivosAsync(long? dimensionId = null);
        Task<List<ObjetivoFiltroDTO>> GetObjetivosByServicioDimensionAsync(long servicioId, long? dimensionId = null);
        Task<List<int>> GetAniosDisponiblesAsync();
    }
}