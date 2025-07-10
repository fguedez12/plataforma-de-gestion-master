using GobEfi.ServiceN.Models.UserModels;

namespace GobEfi.ServiceN.Services
{
    public interface IUserService
    {
        Task<bool> Acceso(AccesoInfo model);
        Task<EntidadesModel> GetEntidades(string userId);
    }
}
