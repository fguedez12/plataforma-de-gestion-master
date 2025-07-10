using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GobEfi.Web.Models.ReporteModels;
using GobEfi.Web.Core.Contracts.Services;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IReporteService : IService<ReporteModel, long>
    {
        ICollection<ReporteModel> GetByUser(string userId, long servicioId);
        Task<MemoryStream> ExportarExcel(long divisionId, long reporteId, string sFileName, bool isAdmin);
        Task InsertAsync(ReporteModel reporte);
        Task UpdateAsync(ReporteModel reporte);
        Task<MemoryStream> DisenioPasivoReporte(long servicioId);
    }
}