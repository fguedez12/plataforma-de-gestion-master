using GobEfi.Web.Models.RolModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IRolService : IService<RolModel, string>
    {
        Task<IEnumerable<RolListModel>> AllByUser(string id);
        IEnumerable<RolModel> GetByUserId(string id);
        IEnumerable<RolModel> GetNoAsociadasByUserId(string id);
        Task<IEnumerable<RolModel>> GetByCurrentUserRol();
    }
}
