using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RegionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IRegionRepository : IRepository<Region, long>
    {
        Task<IEnumerable<RegionModel>> AllRegiones();

        Task<IEnumerable<RegionModel>> AllRegionesWithComunas();
    }
}
