using GobEfi.FV.APIV2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pagin<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                    .Skip((paginationDTO.Page - 1) * paginationDTO.PerPage)
                    .Take(paginationDTO.PerPage);
        }
    }
}
