using GE.Models.NumeroClienteModels;
using GobEfi.Web.Models.NumeroClienteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface INumeroClienteService : IService<NumeroClienteModel, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="divisionId"></param>
        /// <param name="energeticoDivisionId"></param>
        /// <returns></returns>
        IEnumerable<NumeroClienteModel> Get(long divisionId, long energeticoDivisionId);

        NumeroClienteModel GetByNumero(string numeroCliente);
        bool NumClientExist(string numeroCliente, long divisionId, long energeticoDivisionId);
        Task<NumeroClienteModel> GetNumeroClientesByNum(string numCliente, long? empresaDistribuidoraId);
        Task<ICollection<NumClienteToDDL>> ByDivision(long divisionId, long energeticoId);
        Task<ICollection<NumClienteToDDL>> ByEdificioId(long edificioId, long energeticoId);
        bool validaNumeroByMedidor(long divisionId, long energeticoId, long numeroClienteId);
    }
}
