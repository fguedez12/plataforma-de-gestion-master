using api_gestiona.DTOs.Medidor;

namespace api_gestiona.Services.Contracts
{
    public interface IMedidorService
    {
        Task<List<MedidorDTO>> GetAll(MedidorFilterDTO medidorFilterDTO);
    }
}
