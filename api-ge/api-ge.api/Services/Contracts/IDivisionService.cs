using api_gestiona.DTOs.Divisiones;

namespace api_gestiona.Services.Contracts
{
    public interface IDivisionService
    {
        Task<DivisionesResponse> GetByServicioId(long servicioId, string searchText);
        Task<DivisionesResponse> GetByUserId(string id, bool isAdmin);
        Task<List<DivisionListDTO>> GetListByServicioId(long servicioId, string? searchText);
        Task SetInicioGestion(DivisionDTO model);
        Task SetRestoItems(DivisionDTO model);
    }
}
