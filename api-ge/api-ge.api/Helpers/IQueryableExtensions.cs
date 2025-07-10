using api_gestiona.DTOs.Pagination;

namespace api_gestiona.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Pagin<T>(this IQueryable<T> queruable, PaginationDTO paginationDTO)
        {
            return queruable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }
    }
}
