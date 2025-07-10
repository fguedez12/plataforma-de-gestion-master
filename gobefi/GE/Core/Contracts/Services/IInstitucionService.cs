using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IInstitucionService : IService<InstitucionModel, long>
    {
        PagedList<InstitucionListModel> Paged(InstitucionRequest request);
        Task<IEnumerable<InstitucionListModel>> AllByUser(string id);
        Task<IEnumerable<InstitucionListModel>> GetNoAsociadosByUserId(string userId);
        //Task<IEnumerable<InstitucionListModel>> GetAsociadosByUserId(string userId);

        /// <summary>
        /// Obtiene las instituciones del usuario actual
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<InstitucionListModel>> GetAsociados();

        /// <summary>
        /// Obtiene las instituciones segun el usuario actual autentificado
        /// </summary>
        /// <returns>Lista de instituciones</returns>
        Task<IEnumerable<InstitucionModel>> GetInstituciones();
        Task<IEnumerable<InstitucionListModel>> GetAsociadosByUserId(string userId);
    }
}
