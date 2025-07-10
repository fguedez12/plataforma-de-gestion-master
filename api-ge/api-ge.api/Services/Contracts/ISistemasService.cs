using api_gestiona.DTOs.Sistemas;

namespace api_gestiona.Services.Contracts
{
    public interface ISistemasService
    {
        Task<SistemasDataDTO> GetData(long divisionId);
        Task<List<EnergeticoEquipoListDTO>> GetEnergeticoEquipo(long equipoId);
        Task<List<TipoColectorListDTO>> GetTiposColectores(string tipo);
        Task SaveSistemaData(long divisionId, string userId, SistemasDataDTO model);
    }
}
