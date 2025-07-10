using System.Collections.Generic;
using System.Linq;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Core.Contracts.Repositories;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IReporteRepository : IRepository<Reporte, long>
    {
        object ObtenerData(long servicioId, bool isAdmin);
    }
}