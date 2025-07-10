using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParams<T>(this HttpContext httpContext, IQueryable<T> queryable, int totalRecodsPerPage)
        {
            double total = await queryable.CountAsync();
            double totalPages = Math.Ceiling(total / totalRecodsPerPage);
            httpContext.Response.Headers.Add("totalPages", totalPages.ToString());
        }
    }
}
