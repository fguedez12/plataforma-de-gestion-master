using GobEfi.Web.Models.TrazabilidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ITrazabilidadService : IService<TrazabilidadModel, long>
    {
        Task<long> Add<T>(T entity) where T : class;
    }
}
