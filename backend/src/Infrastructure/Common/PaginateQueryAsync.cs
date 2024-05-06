using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public static class PaginateQueryAsync
    {
        public static async Task<PaginationResponse<T>> PaginateAsync<T>(this IQueryable<T> query, int page, int size)
        {
            page = (page <= 0) ? 0 : --page;
            size = size <= 0 ? 10 : size;

            var totalData = await query.CountAsync();

            var data = await query
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return new PaginationResponse<T>(data, totalData, page +1, size );
        }
    }
}
