using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.InstitucionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IComunaService : IService<ComunaModel, long>
    {
        IEnumerable<ComunaModel> GetByEmpresaDistribuidora(long idEmpresaDistribuidora);
        IEnumerable<ComunaModel> GetByEmpresaDistribuidoraNoAsociadas(long idEmpresaDistribuidora);
        Task<IEnumerable<ComunaModel>> GetByProvinciaId(long provinciaId);
        Task<IEnumerable<ComunaModel>> GetAllAsync();
        Task<IEnumerable<ComunaModel>> GetByEdificioId(long edificioId);
        Task<ComunaModel> GetAsync(long comunaId);
    }
}
