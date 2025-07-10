using GobEfi.Web.Data.Entities;
using System.Collections.Generic;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    public interface IEnergeticoDivisionRepository : IRepository<EnergeticoDivision, long>
    {

        IList<EnergeticoDivision> GetByDivisionId(long id);

        /// <summary>
        /// devuelve una lista de Ids de numeros de Cliente
        /// </summary>
        /// <param name="DivisionId"></param>
        /// <param name="EnergeticoId"></param>
        /// <returns></returns>
        List<long> Get(long DivisionId, long EnergeticoId);
        EnergeticoDivision Get(long nClienteId, long divisionId, long energeticoId);
        List<EnergeticoDivision> Get(long divisionId, long energeticoId, bool active = true);

        /// <summary>
        /// valida si el numero de cliente enviado por parametro esta asociado a mas relaciones entre division-energetico
        /// </summary>
        /// <param name="nClienteId"></param>
        /// <returns>Si esta asociado a mas de 1 relacion devuelve true</returns>
        bool TieneMasAsociado(long nClienteId);
    }
}
