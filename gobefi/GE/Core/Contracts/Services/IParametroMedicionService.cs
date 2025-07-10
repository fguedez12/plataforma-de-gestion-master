using System.Collections.Generic;
using System.Threading.Tasks;
using GE.Models.ParametrosMedicionModels;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IParametroMedicionService : IService<ParametrosMedicionModel, long>
    {
        Task<IEnumerable<ParametrosMedicionModel>> AllAsync();
    }
}
