using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Helper
{
    public static class HttpContextExtensions
    {
        public async static Task PaginationParams<T>(this HttpContext httpContext, IQueryable<T> queryable, int perPage)
        {
            double total = await queryable.CountAsync();
            double pages = Math.Ceiling(total / perPage);
            httpContext.Response.Headers.Add("Pages", pages.ToString());
            httpContext.Response.Headers.Add("TotalRecords", total.ToString());
        }
    }
}
