using GobEfi.Web.Models.ProvinciaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IProvinciaService : IService<ProvinciaModel, long>
    {
        Task<IEnumerable<ProvinciaModel>> GetByRegionId(long regionId);
    }
}
