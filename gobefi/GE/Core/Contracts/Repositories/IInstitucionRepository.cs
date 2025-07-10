using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IInstitucionRepository : IRepository<Institucion, long>
    {
        Task<IEnumerable<InstitucionListModel>> AllByUser(string id);
    }
}
