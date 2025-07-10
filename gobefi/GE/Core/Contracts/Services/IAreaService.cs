using GobEfi.Web.Models.AreaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IAreaService
    {
        Task AddUnidad(AreaUnidadRequest model);
        Task RemUnidad(AreaUnidadRequest model);
        Task<List<AreaModel>> Save(AreaForSave model);
    }
}
