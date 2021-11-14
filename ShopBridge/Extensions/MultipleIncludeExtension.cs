using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopBridge.Extensions
{
    /// <summary>
    /// MultipleIncludeExtension
    /// </summary>
    public static class MultipleIncludeExtension
    {
        /// <summary>
        /// IncludeMultipleNavigateProperty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="query"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public static IQueryable<T> IncludeMultipleNavigateProperty<T, TProperty>(this IQueryable<T> query,
            IEnumerable<Expression<Func<T, TProperty>>> includes) where T : class
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
