using GobEfi.Web.Models.RegionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IRegionService : IService<RegionModel, long>
    {
        Task<IEnumerable<RegionModel>> AllAsync();

        Task<IEnumerable<RegionModel>> AllRegionesWithComunas();
        Task<IEnumerable<RegionModel>> GetByServicioId(long servicioId);
    }
}
