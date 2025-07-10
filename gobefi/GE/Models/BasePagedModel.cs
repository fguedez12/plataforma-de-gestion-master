using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BasePagedModel<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public BasePagedModel()
        {
            Sort = new SortModel();
        }

        /// <summary>
        /// 
        /// </summary>
        public T Titulos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SortModel Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
