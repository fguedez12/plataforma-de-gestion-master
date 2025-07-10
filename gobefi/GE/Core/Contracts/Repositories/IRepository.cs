using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<T, TKey> where T : class
    {
        /// <summary>
        /// Las consultas de AsNoTracking() son útiles cuando los resultados se usan en un escenario de solo lectura. Su ejecución es más rápida porque no es necesario configurar información de seguimiento de cambios.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(TKey id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="query"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PagedList<TDto> Paged<TDto>(IQueryable<TDto> query, int currentPage, int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
