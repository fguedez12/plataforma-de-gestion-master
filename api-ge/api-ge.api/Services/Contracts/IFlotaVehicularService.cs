using api_gestiona.DTOs.Divisiones;

namespace api_gestiona.Services.Contracts
{
    public interface IFlotaVehicularService
    {
        Task<List<VehiculoDTO>> GetVehiculosServicioByDivisionId(long divisionId);
    }
}
