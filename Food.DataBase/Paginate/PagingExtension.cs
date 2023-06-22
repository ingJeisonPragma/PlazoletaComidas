using Food.Domain.Business.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Food.DataBase.Paginate
{
    public static class PagingExtension
    {
        public static async Task<PaginatedListDTO<T>> GetPagedAsync<T>(
            this IQueryable<T> query, int page, int take)
        {
            var originalPages = page;

            page--;
            if (page > 0) 
                page *= take;

            var result = new PaginatedListDTO<T>()
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }

            return result;
        }
    }
}
