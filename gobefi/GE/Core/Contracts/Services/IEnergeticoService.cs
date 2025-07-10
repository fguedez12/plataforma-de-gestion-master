using GobEfi.Web.Models.EnergeticoDivisionModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IEnergeticoService : IService<EnergeticoModel, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagedList<EnergeticoListModel> Paged(EnergeticoRequest request);
        
        /// <summary>
        /// Retorna un listado de energeticos para pintarlos en un Drop Down List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<EnergeticoModel>> GetAllAsync();
        Task<IEnumerable<EnergeticoDivisionModel>> GetByDivisionId(long divisionId);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<EnergeticoSelectModel> Select();

        /// <summary>
        /// obtiene el primer energetico del numero cliente ingresado
        /// </summary>
        /// <param name="numeroClienteId"></param>
        /// <returns></returns>
        EnergeticoModel CheckFirstEnergetico(long numeroClienteId, long energeticoIdActual);
        Task<EnergeticoForEditModel> GetForEdit(long? id);
        Task UpdateAsync(EnergeticoForEditModel energeticoModel);
        Task<IEnumerable<EnergeticoActivoModel>> GetActivosByDivision(long divisionId);
        Task InsertAsync(EnergeticoModel energeticoModel);
        Task<IEnumerable<EnergeticoActivoModel>> GetByEdificioId(long edificioId);
    }
}
