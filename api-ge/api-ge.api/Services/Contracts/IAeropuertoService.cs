using api_gestiona.DTOs.Viajes;

namespace api_gestiona.Services.Contracts
{
    public interface IAeropuertoService
    {
        Task<List<AeropuertoListaDTO>> GetAeropuertosByPaisId(long paisId);
    }
}
