using GobEfi.Web.Models.TipoUsoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ITipoUsoService : IService<TipoUsoModel, long>
    {
        IEnumerable<TipoUsoModel> getByEdificioId(long edificioId);
    }
}
