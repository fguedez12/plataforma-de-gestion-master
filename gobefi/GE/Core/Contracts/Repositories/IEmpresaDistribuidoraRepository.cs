using GobEfi.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IEmpresaDistribuidoraRepository : IRepository<EmpresaDistribuidora, long>
    {
        IQueryable<EmpresaDistribuidora> GetByEnergetico(long energeticoId);

        void DeleteComunas(long empresaDistribuidoraId);
        void AgregarComunas(ICollection<long> comunas, long empresaDistribuidoraId);
    }
}
