using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Database
{
    /// <summary>
    /// OrderBySelectors
    /// </summary>
    public class OrderBySelectors
    {
        /// <summary>
        /// Get user order by selector based on order by string
        /// </summary>
        /// <param name="orderBy">string</param>
        /// <returns></returns>
        public static Func<Product, object> GetProductOrderBySelector(string orderBy)
        {
            switch (orderBy)
            {
                case "Name":
                    return x => x.ProductName;

                case "Supplier":
                    return x => x.Supplier.Name;

                case "Category":
                    return x => x.Category.CategoryName;

                default:
                    return x => x.ProductName;

            }
        }
    }
}
