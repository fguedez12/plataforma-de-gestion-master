using api_gestiona.DTOs.TipoDocumentos;

namespace api_gestiona.Services.Contracts
{
    public interface ITipoDocumentoService
    {
        Task<TipoDocumentoResponse> GetAll();
    }
}
