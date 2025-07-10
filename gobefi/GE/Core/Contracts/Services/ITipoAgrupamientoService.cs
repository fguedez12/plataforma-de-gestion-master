using GobEfi.Web.Models.TipoAgrupamientoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ITipoAgrupamientoService : IService<TipoAgrupamientoModel, string>
    {
        Task<IEnumerable<TipoAgrupamientoModel>> GetAllAsync();
    }
}
