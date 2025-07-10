using GobEfi.Web.Models.EstadoValidacionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEstadoValidacionService :IService<EstadoValidacionModel,string>
    {
        Task<IEnumerable<EstadoValidacionModel>> GetAllAsync();
    }
}
