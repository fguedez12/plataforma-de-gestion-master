using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.EnergeticoUnidadMedidaModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IUnidadMedidaService : IService<UnidadMedida, long>
    {
        List<UnidadMedidaModel> Get(List<EnergeticoUnidadMedidaModel> unidadMedidaIds);
        Task<IEnumerable<UnidadMedidaModel>> GetAll();
        Task<EnergeticoForEditModel> GetForEdit(long? id);
        Task<IEnumerable<UnidadMedidaModel>> GetAsociadosByEnergeticoId(long energeticoId);
        Task<IEnumerable<UnidadMedidaModel>> GetNoAsociadosByEnergeticoId(long energeticoId);
    }
}
