using GobEfi.Web.Models.TipoNivelPisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface ITipoNivelPisoService : IService<TipoNivelPisoModel, string>
    {
        TipoNivelPisoModel Get(int id);
    }
}
