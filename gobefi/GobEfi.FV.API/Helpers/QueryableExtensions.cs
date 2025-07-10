using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Pagin<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO) where T : class ,IId
        {
            return queryable
                    .Skip((paginationDTO.Page - 1) * paginationDTO.PerPage)
                    .Take(paginationDTO.PerPage)
                    .OrderBy(x=>x.Id);
        }
    }
}
