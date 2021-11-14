using Microsoft.EntityFrameworkCore;
using ShopBridge.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Extensions
{
    /// <summary>
    /// PaginationExtension
    /// </summary>
    public static class PaginationExtension
    {
        /// <summary>
        /// PaginateDataAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="query"></param>
        /// <param name="pagedParametes"></param>
        /// <param name="orderbySelector"></param>
        /// <returns></returns>
        public static async Task<PagedModel<T>> PaginateDataAsync<T, Tkey>(this IQueryable<T> query, PagedParameters pagedParametes, Func<T, Tkey> orderbySelector) where T : class
        {
            int page = (pagedParametes.PageNumber < 0) ? 1 : pagedParametes.PageNumber;
            return new PagedModel<T>
            {
                CurrentPage = page,
                PageSize = pagedParametes.PageSize,
                Items = pagedParametes.OrderByACS ?
                query.OrderBy(orderbySelector).AsQueryable().Skip((page - 1) * pagedParametes.PageSize).Take(pagedParametes.PageSize).ToList() :
                query.OrderByDescending(orderbySelector).AsQueryable().Skip((page - 1) * pagedParametes.PageSize).Take(pagedParametes.PageSize).ToList(),
                TotalItems = await query.CountAsync()
            };
        }
    }
}
