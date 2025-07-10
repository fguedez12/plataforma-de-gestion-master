using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RolModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IRolRepository : IRepository<Rol, string>
    {
        Task<IEnumerable<RolListModel>> AllByUser(string id);
        void UpdateRol(Rol entity);
    }
}
