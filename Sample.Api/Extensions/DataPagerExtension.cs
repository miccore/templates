using System;
using System.Linq;
using System.Threading.Tasks;
using Miccore.Net.webapi_template.Sample.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataPagerExtension
    {
        //pagination extension 
        public static async Task<PaginationEntity<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query,
            int page,
            int limit)
            where TModel : class
        {
            
            // initialize pagination entity
            var paged = new PaginationEntity<TModel>();

            // update page
            page = (page < 0) ? 1 : page;

            //initialize current page
            paged.CurrentPage = page;
            // initialize page size, number of element
            paged.PageSize = limit;
            // update total items of elements
            paged.TotalItems = await query.CountAsync();
            // skip items and take another ones
            var startRow = (page - 1) * limit;
            paged.Items = await query.Skip(startRow).Take(limit).ToListAsync();
            // update total number of pages
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);
            // return the paginated
            return paged;
        }
    }
}
