using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models
{
    public abstract class BaseIndexModel<T> where T : class
    {
        public T Titulos { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
