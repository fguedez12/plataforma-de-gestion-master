using GobEfi.Web.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IMedidorRepository : IRepository<Medidor, long>
    {
        //IQueryable<Medidor> Get(long divisionId, long EnergeticoDivisionId);
        IQueryable<Medidor> GetByNumClienteId(long id);
        IQueryable<Medidor> GetByNumMedidor(long numMedidor);
        //IQueryable<Medidor> GetByNumClientesIds(List<long> NumClienteIds);
        int CambiaEstadoCompartido(long nClienteId, bool swap);
    }
}
