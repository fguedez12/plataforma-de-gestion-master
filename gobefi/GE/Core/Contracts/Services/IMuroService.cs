using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IMuroService
    {
        Task DisableMuros(MurosForSave murosForSave);
        Task DisableMurosByPiso(long pisoId);
        Task<List<PisoModel>> SaveMuros(MurosForSave murosForSave);
        Task<List<PisoModel>> SaveMurosInternos(MurosForSave murosForSave);
        Task UpdateMuro(long id, MuroModel muro);
    }
}
