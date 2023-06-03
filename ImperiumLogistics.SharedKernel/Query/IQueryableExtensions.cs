using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.Query
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> GetPagedData<T>(
            this IQueryable<T> queryable,
            PagedQueryRequest request)
        {
            return queryable.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }

        public static PagedQueryResult<T> ToPagedResult<T>(
           this IQueryable<T> source,
           int pageNumber,
           int pageSize)
        {
            var totalItemCount = source.Count();

            pageSize = pageSize < 1 ? totalItemCount : pageSize;

            var totalPageCount = totalItemCount == 0 ? 1 : (totalItemCount + pageSize - 1) / pageSize;
            pageNumber = Math.Max(1, Math.Min(pageNumber, totalPageCount));

            var startIndex = (pageNumber - 1) * pageSize;

            var items = source
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            return new PagedQueryResult<T>
            {
                Items = items,
                TotalPageCount = totalPageCount,
                CurrentPageNumber = pageNumber,
                CurrentPageSize = pageSize,
                TotalItemCount = totalItemCount,
                HasNext = totalItemCount - (pageNumber*pageSize) > 0 ? true : false,
                HasPrevious = pageNumber > 1 ? true : false
            };
        }

        public static bool RelatedEntityExists<T>(this IQueryable<T> set, Func<T, bool> expression)
            where T : class
        {
            return set.Any(expression);
        }
    }
}
