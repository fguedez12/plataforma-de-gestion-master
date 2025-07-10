using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Services
{
    public interface IReporteService
    {
        Task<MemoryStream> ReporteVehiculos(long Id, bool IsAdmin);
    }
}
