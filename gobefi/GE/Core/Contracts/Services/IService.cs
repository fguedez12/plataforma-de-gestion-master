using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IService <TModel, TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TModel Get(TKey id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TModel> All();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        void Update(TModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        TKey Insert(TModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void Delete(TKey id);
    }
}
