
using GobEfi.Web.Models.EntornoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEntornoService: IService<EntornoModel, string>
    {
        Task<IEnumerable<EntornoModel>> GetAllAsync();
    }
}
