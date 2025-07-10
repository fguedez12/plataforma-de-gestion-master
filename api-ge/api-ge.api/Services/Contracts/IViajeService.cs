using api_gestiona.DTOs.Viajes;

namespace api_gestiona.Services.Contracts
{
    public interface IViajeService
    {
        Task CreateViaje(string userId, long servicioId, ViajeCreateDTO model, int year);
        Task UpdateViaje(string userId, long viajeId, ViajeCreateDTO model,int year);
    }
}
