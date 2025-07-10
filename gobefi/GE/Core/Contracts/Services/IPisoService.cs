using GobEfi.Web.Models.PisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IPisoService : IService<PisoModel, string>
    {
        Task<IEnumerable<PisoModel>> GetAllAsync();

        Task<IEnumerable<PisoModel>> GetByDivisionId(long id);

        Task<List<PisoPasoUnoModel>> Update(long id, PisoModel pisoFromBody);



        PisoModel Get(long id);
        //Task<long> SetFrontis(long id, int muroIndex);
        Task<List<PisoPasoUnoModel>> SavePiso(PisoForSaveModel model);
        Task AddUnidad(PisoUnidadRequest model);
        Task RemUnidad(PisoUnidadRequest model);
        Task<List<PisoPasoUnoModel>> DeleteAsync(long id);
        Task UpdateV2(long id, PisoModel pisoFromBody);
    }
}
