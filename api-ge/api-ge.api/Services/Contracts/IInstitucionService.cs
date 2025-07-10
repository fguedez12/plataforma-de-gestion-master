using api_gestiona.DTOs.Instituciones;

namespace api_gestiona.Services.Contracts
{
    public interface IInstitucionService
    {
        Task<List<InstitucionListDTO>> GetByUserId(string userId);
    }
}
