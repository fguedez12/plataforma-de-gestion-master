using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InmuebleModels;
using GobEfi.Web.Models.UnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IInmuebleService
    {
        Task AddUnidad(InmuebleUnidadRequest model);
        Task<InmuebleModel> Create(InmuebleToSaveRequest model);
        Task DeleteInmueble(long id);
        Task<List<InmuebleModel>> GetByAddress(InmuebleByAddressRequest request);
        Task<InmuebleModel> GetById(long id);
        Task<List<UnidadModel>> GetUnidades(long id);
        IQueryable<Division> Query();
        Task RemUnidad(InmuebleUnidadRequest model);
        Task Update(InmuebleToUpdateRequest model);
    }
}
