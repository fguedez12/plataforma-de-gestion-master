using api_gestiona.DTOs.MedicionInteligente;
using api_gestiona.DTOs.MedicionInteligente.ResponseDTO;

namespace api_gestiona.Services
{
    public interface IMedicionInteligenteService
    {
        Task<ApiResponseDTO> GetMedicionAvanzada(RequestConsultaAvanzadaDTO request);
        Task<ApiResponseDTO> GetMedicionAvanzadaPot(RequestConsultaAvanzadaDTO request);
        Task<ApiResponseDTO> GetMedicionesDefault(long id);
        Task<ApiResponseDTO> GetMedicionesDefaultPot(long id);
        Task<ApiResponseDTO> GetMedicionesDiaria(long id);
        Task<ApiResponseDTO> GetMedicionesDiariaPot(long id);
        Task<ApiResponseDTO> GetMedicionesMensual(long id);
        Task<ApiResponseDTO> GetMedicionesMensualPot(long id);
        Task<ApiResponseDTO> GetMedicionesSemanal(long id);
        Task<ApiResponseDTO> GetMedicionesSemanalPot(long id);
        Task<List<MedidoresDTO>> GetMedidores(long unidadId);
    }
}
