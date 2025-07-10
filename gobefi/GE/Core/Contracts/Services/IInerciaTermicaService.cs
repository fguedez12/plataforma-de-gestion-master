using GobEfi.Web.Models.InerciaTermicaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IInerciaTermicaService : IService<InerciaTermicaModel, string>
    {
        Task<IEnumerable<InerciaTermicaModel>> GetAllAsync();
    }
}
