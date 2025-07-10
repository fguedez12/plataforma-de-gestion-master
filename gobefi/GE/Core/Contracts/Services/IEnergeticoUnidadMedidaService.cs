using System.Collections.Generic;
using GobEfi.Web.Models.EnergeticoUnidadMedidaModels;
using GobEfi.Web.Models.UnidadMedidaModels;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEnergeticoUnidadMedidaService : IService<EnergeticoUnidadMedidaModel,long>
    {
        bool hasMoreOne(long energeticoId);
        List<EnergeticoUnidadMedidaModel> getByEnergeticoId(long enerId);
    }
}
