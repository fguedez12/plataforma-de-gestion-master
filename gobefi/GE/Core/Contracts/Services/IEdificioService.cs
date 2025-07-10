using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Services.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEdificioService : IService<EdificioModel, long>
    {
        PagedList<EdificioListModel> Paged(EdificioRequest request);
        long GetComunaByDivision(long divisionId);
        Task<IEnumerable<EdificioSelectModel>> getByComunaId(long comunaId);
        IEnumerable<EdificioSelectModel> GetEdificiosForSelect();
        IEnumerable<EdificioModel> GetActivosByUser();
        Task<EdificioSelectModel> GetForSelect(long id);
        Task<IEnumerable<EdificioSelectModel>> getByRegionId(long regionId);
        long Insert(EdificioCreateModel model);
    }
}
