using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Services.Request;
using System.Collections.Generic;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEmpresaDistribuidoraService : IService<EmpresaDistribuidoraModel, long>
    {
        PagedList<EmpresaDistribuidoraListModel> Paged(EmpresaDistribuidoraRequest request);
        IEnumerable<EmpresaDistribuidoraModel> GetByEnergetico(long energeticoId);
        IEnumerable<EmpresaDistribuidoraModel> GetByEnergeticoComuna(long energeticoId, long comunaId);
    }
}
