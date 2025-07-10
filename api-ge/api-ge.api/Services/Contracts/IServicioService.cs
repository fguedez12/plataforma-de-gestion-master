using api_gestiona.DTOs.Servicios;

namespace api_gestiona.Services.Contracts
{
    public interface IServicioService
    {
        Task<bool> Exist(long id);
        Task<List<ServicioListDTO>> GetAll();
        Task<ServicioListDTO> GetById(long id);
        Task<ServicioResponse> GetByUserId(string userId);
        Task<DiagnosticoDTO> GetDiagnostico(long id);
        Task<List<ServicioDTO>> GetServicesByUserId(string userId);
        Task SaveJustificacion(ServicioDTO model);
    }
}
