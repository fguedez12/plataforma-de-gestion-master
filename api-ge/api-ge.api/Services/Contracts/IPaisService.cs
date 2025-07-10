using api_gestiona.DTOs.Viajes;

namespace api_gestiona.Services.Contracts
{
    public interface IPaisService
    {
        Task<List<PaisListaDTO>> GetPaisList();
    }
}
