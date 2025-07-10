using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDivisionRepository : IRepository<Division, long>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IQueryable<Division>> Query(DivisionRequest request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Division> GetDivision(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<DivisionListModel>> AllByUser(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">A string.</param>
        /// <param name="institucionId">A long integer.</param>
        /// <returns></returns>
        Task<IEnumerable<DivisionListModel>> AllByUserAndInstitucion(string userId, long institucionId);

        Task<IEnumerable<DivisionListModel>> AllByUserAndServicio(string userId, long servicioId);
        Task<IEnumerable<DivisionListModel>> GetByFilters(string userId, long servicioId, long comunaId);
        Task SaveChangesAsync();
    }
}
