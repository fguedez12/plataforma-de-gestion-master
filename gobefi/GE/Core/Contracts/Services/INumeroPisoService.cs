using GobEfi.Web.Models.NumeroPisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface INumeroPisoService : IService<NumeroPisoModel, string>
    {
        Task<IEnumerable<NumeroPisoModel>> GetAllAsync();
    }
}
