using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.UsuarioModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario, string>
    {
        Usuario GetWithRoles(string id);
        IEnumerable<UsuarioListExcelModel> ParaExcel(List<long> serviciosIds, bool isAdmin);
    }
}
