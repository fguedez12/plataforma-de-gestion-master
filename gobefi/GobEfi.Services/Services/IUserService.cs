using GobEfi.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Services.Services
{
    public interface IUserService
    {
        Task<bool> Acceso(AccesoInfo model);
        Task<EntidadesModel> GetEntidades(string userId);
    }
}
