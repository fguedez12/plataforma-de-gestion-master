using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IEnergeticoRepository : IRepository<Energetico, long>
    {
        Energetico CheckFirstEnergetico(long numCliente, long energeticoIdActual);
        ICollection<Energetico> GetByDivisionId(long divisionId);
    }
}
