using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core
{
    public interface IPagedList
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
        int RowCount { get; set; }
        int TotalPages { set; get; }
    }
}
