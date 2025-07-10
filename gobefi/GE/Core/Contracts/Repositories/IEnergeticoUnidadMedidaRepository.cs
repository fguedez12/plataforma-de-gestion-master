using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IEnergeticoUnidadMedidaRepository : IRepository<EnergeticoUnidadMedida, long>
    {
        bool HasMoreOne(long energeticoId);
        List<EnergeticoUnidadMedida> GetByEnergeticoId(long enerId);
    }
}
