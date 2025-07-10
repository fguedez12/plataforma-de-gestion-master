
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParameterHeaders<T>(this HttpContext httpContent, IQueryable<T> queryable)
        {
            if (httpContent == null)
            {
                throw new ArgumentNullException(nameof(httpContent));
            }

            double total = await queryable.CountAsync();

            httpContent.Response.Headers.Add("totalRecords", total.ToString());
        }
    }
}
