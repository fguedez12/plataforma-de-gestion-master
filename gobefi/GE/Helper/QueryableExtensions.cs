using GobEfi.Web.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Helper
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pager<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.PerPage)
                .Take(paginationDTO.PerPage);
        }
    }
}
