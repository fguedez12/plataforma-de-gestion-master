using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.EnergeticoDivisionModels;
using System.Collections.Generic;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEnergeticoDivisionService : IService<EnergeticoDivisionModel, long>
    {
        IList<EnergeticoDivision> GetByDivisionId(long id);

        /// <summary>
        /// Desactiva la asociacion entre el numero de cliente y la relacion division-energetico 
        /// Valida si el numero de cliente desactivado se encuentra asociado a mas de una relacion division-energetico
        /// de no estar asociado a ninguna otra relacion, modifica el campo "Compartido" de los medidores asociados al numero de cliente
        /// </summary>
        /// <param name="NClienteId">Numero de cliente Id a desactivar</param>
        /// <param name="divisionId">Division Id actual</param>
        /// <param name="energeticoId">Energetico Id actual</param>
        void Delete(long NClienteId, long divisionId, long energeticoId);

        IList<EnergeticoDivisionModel> Get(long DivisionId, long EnergeticoId);
        bool ExisteRelacion(long divisionId, long energeticoId, long numeroClienteId);

        bool TieneNumCliente(long divisionId, long? energeticoId);

    }
}
